using Content.Server.Administration;
using Content.Server.Power.Components;
using Content.Server.Items;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;

namespace Content.Server.Power
{
    [AdminCommand(AdminFlags.Admin)]
    public class SetBatteryPercentCommand : IConsoleCommand
    {
        public string Command => "setbatterypercent";
        public string Description => "Drains or recharges a battery by entity uid and percentage, i.e.: forall with Battery do setbatterypercent $ID 0";
        public string Help => $"{Command} <id> <percent>";

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            if (args.Length != 2)
            {
                shell.WriteLine($"Invalid amount of arguments.\n{Help}");
                return;
            }

            if (!EntityUid.TryParse(args[0], out var id))
            {
                shell.WriteLine($"{args[0]} is not a valid entity id.");
                return;
            }

            if (!float.TryParse(args[1], out var percent))
            {
                shell.WriteLine($"{args[1]} is not a valid float (percentage).");
                return;
            }

            var entityManager = IoCManager.Resolve<IEntityManager>();

            if (!entityManager.TryGetComponent<BatteryComponent>(id, out var battery))
            {
                shell.WriteLine($"No battery found with id {id}.");
                return;
            }
            battery.CurrentCharge = (battery.MaxCharge * percent) / 100;
            // Don't acknowledge b/c people WILL forall this
        }
    }
}
