namespace Kicksharing.Console
{
    public abstract class Equipment : IInventoryAsset
    {
        public int AssetNumber { get; }
        public string Title { get; }
        
        protected Equipment(int assetNumber, string title)
        {
            AssetNumber = assetNumber;
            Title = title;
        }
    }

    public class SafetyHelmet : Equipment { public SafetyHelmet(int id) : base(id, "Шлем") {} }
    public class ChargingStation : Equipment { public ChargingStation(int id) : base(id, "Станция зарядки") {} }

    public abstract class TransportBase : IRideable
    {
        public int AssetNumber { get; }
        public int UsabilityLevel { get; }
        public string Title { get; }

        protected TransportBase(int id, int usability, string title)
        {
            AssetNumber = id;
            UsabilityLevel = usability;
            Title = title;
        }
    }

    public class ElectricScooter : TransportBase, IPowerConsumer
    {
        public double DailyPowerUsage { get; }

        public ElectricScooter(int id, int usability, double power) 
            : base(id, usability, "Электросамокат")
        {
            DailyPowerUsage = power;
        }
    }

    public class PedalBike : TransportBase
    {
        public PedalBike(int id, int usability) : base(id, usability, "Велосипед") {}
    }

    public class Monowheel : TransportBase, IPowerConsumer
    {
        public double DailyPowerUsage { get; }

        public Monowheel(int id, int usability, double power) 
            : base(id, usability, "Моноколесо")
        {
            DailyPowerUsage = power;
        }
    }
}
