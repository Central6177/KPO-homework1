namespace Kicksharing.Console
{
    public interface IInventoryAsset
    {
        int AssetNumber { get; }
        string Title { get; }
    }

    public interface IPowerConsumer
    {
        double DailyPowerUsage { get; }
    }

    public interface IRideable : IInventoryAsset
    {
        int UsabilityLevel { get; }
    }
}
