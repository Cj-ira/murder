﻿using Microsoft.Xna.Framework;
using Murder.Assets;
using System.Diagnostics.CodeAnalysis;

namespace Murder.Core
{
    public class SceneLoader
    {
        protected readonly GraphicsDeviceManager _graphics;
        protected readonly GameProfile _settings;

        private Scene? _activeScene;

        public Scene ActiveScene => _activeScene!;

        private readonly Dictionary<Guid, GameScene> _gameScenes = new();

        private readonly Dictionary<Type, Scene> _genericScenes = new();

        public SceneLoader(GraphicsDeviceManager graphics, GameProfile settings, Scene scene)
        {
            _graphics = graphics;
            _settings = settings;

            SetScene(scene);
        }

        public void SwitchScene(Guid worldGuid)
        {
            if (_activeScene is GameScene gameScene && 
                gameScene.WorldGuid == worldGuid)
            {
                // Reload the active scene.
                _activeScene.Reload();
                return;
            }

            if (_gameScenes.TryGetValue(worldGuid, out GameScene? scene))
            {
                // Scene was already loaded, just change the active scene.
                scene.Reload();

                SetScene(scene);
                return;
            }

            CacheAndSetScene(new GameScene(worldGuid));
        }

        /// <summary>
        /// Load a scene of type <see cref="T"/>.
        /// </summary>
        public void SwitchScene<T>() where T : Scene, new()
        {
            if (_genericScenes.TryGetValue(typeof(T), out Scene? scene))
            {
                // Scene was already loaded, just change the active scene.
                scene.Reload();

                SetScene(scene);
                return;
            }

            CacheAndSetScene(new T());
        }

        /// <summary>
        /// Load a scene of type <see cref="T"/>.
        /// </summary>
        public void SwitchScene(Scene scene) => SetScene(scene);

        /// <summary>
        /// Load the content of the current active scene.
        /// </summary>
        public ValueTask LoadContentAsync()
        {
            if (_activeScene is null)
            {
                return default;
            }

            return _activeScene.LoadContentAsync(_graphics.GraphicsDevice, _settings);
        }

        private void CacheAndSetScene(Scene scene)
        {
            if (scene is GameScene gameScene)
            {
                _gameScenes.Add(gameScene.WorldGuid, gameScene);
            }
            else
            {
                _genericScenes.Add(scene.GetType(), scene);
            }

            SetScene(scene);
        }
            
        [MemberNotNull(nameof(_activeScene))]
        private void SetScene(Scene scene)
        {
            _activeScene?.Unload();
            _activeScene = scene;
        }
    }
}
