﻿using InstallWizard;
using InstallWizard.Util;
using ImGuiNET;
using System.Numerics;

namespace Editor.Gui
{
    public class RectangleBox : IDisposable
    {
        private readonly Vector2 _p0;
        private readonly Vector2 _padding;

        private readonly Vector4 _color;

        public RectangleBox(int paddingX = 15, int paddingY = 15, Vector4? color = default)
        {
            _padding = new(paddingX, paddingY);
            _p0 = ImGui.GetCursorScreenPos();
            _color = color ?? Game.Profile.Theme.Faded;

            ImGui.Dummy(new Vector2(0, _padding.Y));

            ImGui.Dummy(new Vector2(_padding.X, 0));
            ImGui.SameLine();
            ImGui.BeginGroup();
        }

        public void Dispose()
        {
            ImGui.Dummy(new Vector2(ImGui.GetContentRegionAvail().X + _padding.X, 0));
            ImGui.EndGroup();

            // TODO: Figure out right X value for this.
            Vector2 p1 = ImGui.GetItemRectMax() + _padding;

            ImGui.Dummy(new Vector2(0, _padding.Y));
            ImGui.GetWindowDrawList().AddRect(_p0, p1, ImGuiExtended.MakeColor32(_color), 16f);
        }
    }
}
