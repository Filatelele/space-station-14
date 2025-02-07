using System;
using System.Linq;
using Content.Server.Ghost;
using Content.Shared.Actions.Behaviors;
using Content.Shared.Actions.Components;
using Content.Shared.Cooldown;
using JetBrains.Annotations;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Server.GameObjects;
using Content.Shared.Instruments;

namespace Content.Server.Actions.Actions
{
    /// <summary>
    ///     Pull up MIDI instrument interface for PAIs to "play themselves"
    /// </summary>
    [UsedImplicitly]
    [DataDefinition]
    public class PAIMidi : IInstantAction
    {

        public void DoInstantAction(InstantActionEventArgs args)
        {
            if (!args.Performer.TryGetComponent<ServerUserInterfaceComponent>(out var serverUi)) return;
            if (!args.Performer.TryGetComponent<ActorComponent>(out var actor)) return;
            if (!serverUi.TryGetBoundUserInterface(InstrumentUiKey.Key,out var bui)) return;

            bui.Toggle(actor.PlayerSession);

        }
    }
}
