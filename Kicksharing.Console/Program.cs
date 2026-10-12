using System;
using Microsoft.Extensions.DependencyInjection;

namespace Kicksharing.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ITechnicalInspection, TechInspectionService>()
                .AddSingleton<FleetManager>()
                .BuildServiceProvider();

            var activeFleet = serviceProvider.GetRequiredService<FleetManager>();

            var helmet = new SafetyHelmet(id: 1);
            var bicycle = new PedalBike(id: 2, usability: 6);
            var premiumScooter = new ElectricScooter(id: 3, usability: 8, power: 1.7);
            var fastScooter = new ElectricScooter(id: 4, usability: 2, power: 3.6);
            var monowheel = new Monowheel(id: 5, usability: 1, power: 0.9);

            System.Console.WriteLine("----- Парк ВышКат -----");
            activeFleet.RegisterEquipment(helmet);
            System.Console.WriteLine($"[ИНВЕНТАРЬ] {helmet.Title} (№{helmet.AssetNumber}) добавлен на баланс без техосмотра.");
            
            TryRegister(activeFleet, bicycle);
            TryRegister(activeFleet, premiumScooter);
            TryRegister(activeFleet, fastScooter);
            TryRegister(activeFleet, monowheel);

            System.Console.WriteLine("\nОборудование поставлено на баланс.");

            System.Console.WriteLine("\n----- Доступный инвентарь парка -----");
            foreach (var asset in activeFleet.GetEntireFleet())
            {
                System.Console.WriteLine($"Оборудование. №{asset.AssetNumber} | {asset.Title}");
            }

            System.Console.WriteLine($"\nРасход энергии парка: {activeFleet.CalculateTotalPowerUsage()} кВт⋅ч");
            System.Console.WriteLine("\n----- Транспорт, которым могут пользоваться новички -----");
            foreach (var t in activeFleet.GetBeginnerFriendlyTransports())
            {
                System.Console.WriteLine($"Оборудование. №{t.AssetNumber} | {t.Title} (Рейтинг простоты для новичка: {t.UsabilityLevel}/10)");
            }
        }

        static void TryRegister(FleetManager fleet, IRideable transport)
        {
            bool success = fleet.RegisterTransport(transport);
            if (success)
            {
                System.Console.WriteLine($"[ТЕХОСМОТР УСПЕШЕН] {transport.Title} (№{transport.AssetNumber}) прошел проверку и добавлен в парк.");
            }
            else
            {
                System.Console.WriteLine($"[ТЕХОСМОТР ПРОВАЛЕН] {transport.Title} (№{transport.AssetNumber}) отклонен сервисом проверки.");
            }
        }
    }
}
