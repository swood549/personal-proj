using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleData;

namespace TestVehicleData
{
    [TestClass]
    public class UnitTest1
    {        
        private VehicleDBContext _vehicleDBContext = new VehicleDBContext(new liteDBVehicleSupplier());

        [TestMethod]
        public void Test_VehicleModel_Create()
        {
            //Test query filter            
            var v1 = new VehicleModel();
            v1.Type = "boat";
            v1.Name = "Stringray 6";
            v1.FuelLevel = 50;
            v1.Mileage = 120;
            bool result = _vehicleDBContext.CreateVehicle(v1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_VehicleModel_QueryName()
        {
            //Test query filter            
            var result = _vehicleDBContext.GetList("Maz");

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Test_VehicleModel_QueryAll()
        {
            //Test query filter            
            var result = _vehicleDBContext.GetList();

            Assert.IsTrue(result != null);
        }

    }
}
