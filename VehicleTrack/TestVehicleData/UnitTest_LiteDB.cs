using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleData;

namespace TestVehicleData
{
    [TestClass]
    public class UnitTest_LiteDB
    {
        private liteDBVehicleSupplier _liteDB = new liteDBVehicleSupplier();        

        [TestMethod]
        public void Test_LiteDB_SeedDB()
        {
            //Load sample records                        
            var v1 = new VehicleModel();
            v1.Type = "car";
            v1.Name = "Honda Civic";
            v1.FuelLevel = 50;
            v1.Mileage = 120;
            _liteDB.CreateRecord(v1);

            var car2 = new VehicleModel();
            car2.Type = "car";
            car2.Name = "Moza 3";
            car2.FuelLevel = 80;
            car2.Mileage = 240;
            _liteDB.CreateRecord(car2);

            int count = _liteDB.RecordCount();
            Assert.IsTrue(count > 1);
        }

        [TestMethod]
        public void Test_LiteDB_Create()
        {
            //Test Create record
            var v1 = new VehicleModel();
            v1.Type = "boat";
            v1.Name = "Stringray 6";
            v1.FuelLevel = 50;
            v1.Mileage = 120;
            v1.TireCount = "NA";
            bool result = _liteDB.CreateRecord(v1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ListDB_QueryAll()
        {
            //Test query filter
            var result = _liteDB.QueryRecordsAll();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Test_LiteDB_QueryName()
        {
            //Test query filter
            var result = _liteDB.QueryRecordsByName("Hon");

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Test_LiteDB_QueryId()
        {
            //Test query filter            
            var result = _liteDB.QueryRecordsById(1);

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Test_LiteDB_DeleteRecord()
        {
            //Test query filter            
            var result = _liteDB.RecordDelete(1);

            Assert.IsTrue(result);
        }        
    }
}
