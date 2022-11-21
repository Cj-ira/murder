﻿using ImGuiNET;
using Microsoft.Xna.Framework.Input;
using System.Numerics;
using Murder.Assets;
using Murder.ImGuiExtended;
using Murder.Diagnostics;
using Murder.Editor.Utilities;
using Murder.Components;

namespace Murder.Editor
{
    public partial class EditorScene
    {
        private void DrawCreateAssetModal(Type type)
        {
            if (ImGui.BeginPopup(CreatePopupAssetForType(type)))
            {
                var assetTypes = new List<Type>();
                var searchForType = type;
                var parent = ReflectionHelper.TryFindFirstAbstractOf(type);
                if (parent != typeof(GameAsset))
                    searchForType = parent; 

                foreach (var t in ReflectionHelper.GetAllImplementationsOf<GameAsset>())
                {
                    if ((searchForType is null || searchForType.IsAssignableFrom(t)) && !t.IsAbstract)
                    {
                        assetTypes.Add(t);
                    }
                }

                if (assetTypes.Count > 0)
                {
                    ImGui.Text("What's the asset type?");
                    if (ImGui.BeginCombo("", assetTypes[_selectedAssetToCreate].Name))
                    {
                        for (int i = 0; i < assetTypes.Count; i++)
                        {
                            if (ImGui.MenuItem(assetTypes[i].Name))
                            {
                                _selectedAssetToCreate = i;
                                _newAssetName = String.Format(Architect.EditorSettings.NewAssetDefaultName, type.Name);
                            }
                        }

                        ImGui.EndCombo();
                    }

                    Type createAssetOfType = assetTypes[_selectedAssetToCreate];
                    ImGui.PushID("NewNameField");
                    ImGui.InputText("", ref _newAssetName, 128, ImGuiInputTextFlags.AutoSelectAll);
                    ImGui.PopID();

                    if (!string.IsNullOrWhiteSpace(_newAssetName))
                    {
                        if (createAssetOfType.GetConstructor(Type.EmptyTypes) != null)
                        {
                            if (ImGui.Button("Create") || Architect.Input.Pressed(Keys.Enter))
                            {
                                string name = AssetsFilter.GetValidName(createAssetOfType, name: _newAssetName.Trim());

                                _selectedAsset = Architect.EditorData.CreateNewAsset(createAssetOfType, name);
                                GameLogger.Verify(_selectedAsset is not null);

                                OpenAssetEditor(_selectedAsset);

                                _selectedAsset.Name = name;
                                _selectedAsset.FileChanged = true;
                                ImGui.CloseCurrentPopup();
                            }
                        }
                        else
                        {
                            ImGuiHelpers.DisabledButton("Create");
                            ImGuiHelpers.HelpTooltip("No generic constructor found for this asset");
                        }
                        ImGui.SameLine();
                    }
                }
                else
                {
                    ImGui.Text("No asset type found!\n(You should create one on the C# project)");
                }

                if (ImGui.Button("Cancel") || Architect.Input.Pressed(Keys.Escape))
                {
                    ImGui.CloseCurrentPopup();
                }

                ImGui.EndPopup();
            }
        }

        private void DrawAssetFolder(string folderName, Vector4 color, Type? createType, IEnumerable<GameAsset> assets, int depth)
        {
            string printName = GetFolderPrettyName(folderName, out char? icon);

            Dictionary<string, (Vector4 color, Type? createType, List<GameAsset> assets)> foldersToDraw = new();
            foreach (GameAsset asset in assets)
            {
                var folders = Path.Combine(asset.EditorFolder, asset.Name).Split('\\', '/');
                if (folders.Length > depth + 1)
                {
                    string currentFolder = folders[depth];
                    if (!foldersToDraw.ContainsKey(currentFolder))
                    {
                        // Add create asset button to the folder if necessary
                        var t = !string.IsNullOrWhiteSpace(asset.EditorFolder) && depth == 0 ? asset.GetType() : null;

                        foldersToDraw[currentFolder] = (asset.EditorColor, t, new List<GameAsset>());
                    }

                    foldersToDraw[currentFolder].assets.Add(asset);
                }
            }

            IEnumerable<(string folder, Vector4 color, Type? createType, List<GameAsset> assets)> orderedDirectories =
                foldersToDraw.OrderBy(kv => GetFolderPrettyName(kv.Key, out _)).Select(kv => (kv.Key, kv.Value.color, kv.Value.createType, kv.Value.assets));

            if (icon.HasValue && depth > 0)
            {
                ImGuiHelpers.ColorIcon(icon.Value, color);
                ImGui.SameLine();
            }

            if (depth <= 1) ImGui.PushStyleColor(ImGuiCol.Text, color);

            bool isFolderOpened = string.IsNullOrWhiteSpace(printName) || ImGui.TreeNodeEx(printName);
            if (createType is not null && printName != "Generated")
            {
                DrawAssetContextMenu(createType);
            }

            if (isFolderOpened)
            {
                if (depth <= 1) ImGui.PopStyleColor();

                // TODO: Draw folders in alphabetical order
                foreach ((string folder, Vector4 folderColor, Type? folderCreateType, List<GameAsset> folderAssets) in orderedDirectories)
                {
                    if (folder.StartsWith(GameAsset.SkipDirectoryIconCharacter))
                    {
                        DrawAssetFolder(folder, folderColor, folderCreateType, folderAssets, depth + 1);
                    }
                    else
                    {
                        DrawAssetFolder($"#\uf07b{folder}", folderColor, folderCreateType, folderAssets, depth + 1);
                    }
                }

                foreach (GameAsset asset in assets)
                {
                    var folders = Path.Combine(asset.EditorFolder, asset.Name).Split('\\', '/');
                    if (folders.Length > depth + 1)
                    {
                        continue;
                    }

                    DrawAssetInList(asset, color, folders[^1]);
                }

                if (!string.IsNullOrWhiteSpace(printName))
                {
                    ImGui.TreePop();
                }
            }
            else
            {
                if (depth <= 1)
                {
                    ImGui.PopStyleColor();
                }
            }
        }

        private static string CreatePopupAssetForType(Type t) => $"Create {t.Name}##Create {t.FullName}";

        private void DrawAssetContextMenu(Type type)
        {
            string name = type == typeof(GameAsset) ? 
                "asset (pick one!)" : 
                Prettify.FormatAssetName(type.Name);

            ImGui.PushID($"context_create_{type.Name}");
            ImGui.PushStyleColor(ImGuiCol.Text, Game.Profile.Theme.White);

            if (ImGui.BeginPopupContextItem())
            {
                if (ImGui.Selectable($"Create new {name}"))
                {
                    ImGui.OpenPopup(CreatePopupAssetForType(type));
                }

                ImGui.EndPopup();
            }

            ImGui.PopStyleColor();

            ImGui.PopID();

            DrawCreateAssetModal(type);
        }

        private void DrawAssetInList(GameAsset asset, Vector4 color, string name)
        {
            ImGui.PushID($"TabIconList {asset.Guid}");

            var selectedColor = _selectedAsset == asset ? Game.Profile.Theme.Faded : Game.Profile.Theme.BgFaded;
            ImGui.PushStyleColor(ImGuiCol.Header, selectedColor);

            if (ImGuiHelpers.SelectableWithIconColor($"{name}{(asset.FileChanged ? "*" : "")}", asset.Icon, color, color * 0.6f, _selectedAssets.Contains(asset)))
            {
                OpenAssetEditor(asset);
            }

            ImGui.PopStyleColor();

            if (ImGui.BeginPopupContextItem())
            {
                ImGui.TextColored(Game.Profile.Theme.Faded, asset.Name);
                if (ImGui.MenuItem("Save"))
                {
                    Architect.EditorData.SaveAsset(asset);
                    ImGui.CloseCurrentPopup();
                }
                if (asset is PrefabAsset prefab && ImGui.MenuItem("Create instance"))
                {
                    string instanceName = Architect.EditorData.GetNextName($"{prefab.Name} Instance", Architect.EditorSettings.AssetNamePattern);

                    GameAsset instance = prefab.ToInstanceAsAsset(instanceName);
                    Architect.Data.AddAsset(instance);

                    ImGui.CloseCurrentPopup();
                }
                if (ImGui.MenuItem("Duplicate"))
                {
                    string duplicateName = Architect.EditorData.GetNextName(asset.Name, Architect.EditorSettings.AssetNamePattern);

                    GameAsset instance = asset.Duplicate(duplicateName);
                    Architect.Data.AddAsset(instance);

                    ImGui.CloseCurrentPopup();
                }
                if (asset.CanBeRenamed && ImGui.Selectable("Rename", false, ImGuiSelectableFlags.DontClosePopups))
                {
                    _newAssetName = asset.Name;
                    ImGui.OpenPopup("Asset Name");
                }
                if (asset.CanBeDeleted && ImGui.Selectable("Delete", false, ImGuiSelectableFlags.DontClosePopups))
                {
                    ImGui.OpenPopup("Delete?");
                }

                if (DrawRenameModal(asset))
                {
                    ImGui.CloseCurrentPopup();
                }

                if (DrawDeleteModal(asset))
                {
                    ImGui.CloseCurrentPopup();
                }

                ImGui.EndPopup();
            }

            ImGui.PopID();
        }

        private void CreateAssetButton(Type type)
        {
            ImGui.PushStyleColor(ImGuiCol.Text, Game.Profile.Theme.White);

            if (ImGuiHelpers.SelectableWithIcon($"", '\uf0fe', false))
            {
                _selectedAssetToCreate = 0;
                _newAssetName = String.Format(Architect.EditorSettings.NewAssetDefaultName, type.Name);
                ImGui.OpenPopup(CreatePopupAssetForType(type));
            }

            ImGui.PopStyleColor();

            DrawCreateAssetModal(type);
        }

        private string GetFolderPrettyName(string name, out char? icon)
        {
            if (name.StartsWith(GameAsset.SkipDirectoryIconCharacter))
            {
                if (name.Length >= 2)
                {
                    icon = name[1];
                    return name[2..];
                }
                else
                {
                    GameLogger.Error("Expected an icon and name for the directory name.");
                }
            }

            icon = null;
            return name;
        }
    }
}
