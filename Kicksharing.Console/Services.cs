using System.Collections.Generic;
using System.Linq;

namespace Kicksharing.Console
{
    public interface ITechnicalInspection
    {
        bool IsApproved(IRideable transport);
    }

    public class TechInspectionService : ITechnicalInspection
    {
        public bool IsApproved(IRideable transport) => true; 
    }

    public class FleetManager
    {
        private readonly ITechnicalInspection _inspection;
        private readonly List<IInventoryAsset> _assets = new();

        public FleetManager(ITechnicalInspection inspection)
        {
            _inspection = inspection;
        }

        public bool RegisterTransport(IRideable transport)
        {
            if (_inspection.IsApproved(transport))
            {
                _assets.Add(transport);
                return true;
            }
            return false;
        }

        public void RegisterEquipment(Equipment item) => _assets.Add(item);

        public double CalculateTotalPowerUsage() => 
            _assets.OfType<IPowerConsumer>().Sum(p => p.DailyPowerUsage);

        public IEnumerable<IRideable> GetBeginnerFriendlyTransports() => 
            _assets.OfType<IRideable>().Where(t => t.UsabilityLevel >= 6);

        public IEnumerable<IInventoryAsset> GetEntireFleet() => _assets;
    }
}
