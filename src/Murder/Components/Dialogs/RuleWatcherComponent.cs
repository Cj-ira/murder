﻿using Bang.Components;
using Murder.Attributes;
using Murder.Core.Dialogs;
using Murder.Services;
using Murder.Utilities.Attributes;

namespace Murder.Components
{
    /// <summary>
    /// This will watch for rule changes based on the blackboard system.
    /// </summary>
    [Unique]
    [RuntimeOnly]
    [DoNotPersistEntityOnSave]
    public struct RuleWatcherComponent : IModifiableComponent
    {
        public void Subscribe(Action notification)
        {
            MurderSaveServices.CreateOrGetSave()?.BlackboardTracker.Watch(notification, BlackboardKind.Gameplay);
        }

        public void Unsubscribe(Action notification)
        {
            MurderSaveServices.TryGetSave()?.BlackboardTracker.ResetWatchers(BlackboardKind.Gameplay);
        }
    }
}
