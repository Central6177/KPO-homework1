using Xunit;
using Kicksharing.Console;
using System.Linq;

namespace Kicksharing.Tests
{
    public class FakeTechService : ITechnicalInspection
    {
        public bool WillPassInspection { get; set; } = true;
        
        public bool IsApproved(IRideable transport)
        {
            return WillPassInspection;
        }
    }

    public class ParkTests
    {
        [Fact]
        public void AddTransport_WhenInspectionRejected_DoesNotAddToFleet()
        {
            var fakeTech = new FakeTechService { WillPassInspection = false };
            var fleet = new FleetManager(fakeTech);
            
            var damagedBike = new PedalBike(555, 3); 
            
            bool result = fleet.RegisterTransport(damagedBike);
            
            Assert.False(result);
            Assert.Empty(fleet.GetEntireFleet());
        }

        [Fact]
        public void FilterForNovices_ReturnsOnlyItemsWithHighUsability()
        {
            var fakeTech = new FakeTechService { WillPassInspection = true };
            var fleet = new FleetManager(fakeTech);
            fleet.RegisterTransport(new ElectricScooter(10, 4, 2.5)); 
            fleet.RegisterTransport(new Monowheel(11, 6, 1.0));       
            fleet.RegisterTransport(new PedalBike(12, 9));            
            var safeTransports = fleet.GetBeginnerFriendlyTransports().ToList();
            
            Assert.Equal(2, safeTransports.Count);
            Assert.Contains(safeTransports, item => item.AssetNumber == 11);
            Assert.Contains(safeTransports, item => item.AssetNumber == 12);
            Assert.DoesNotContain(safeTransports, item => item.AssetNumber == 10);
        }
    }
}
