using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiteDBWrapper;

namespace TestVehicleTrack
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_DB_QueryID_Success()
        {
            //Test query filter
            DBLayer db = new DBLayer();
            var result = db.QueryRecordsById(1);

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Test_DB_QueryName_Success()
        {
            //Test query filter
            DBLayer db = new DBLayer();
            var result = db.QueryRecordsByName("Hon");

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Test_DB_QueryName_Failure()
        {
            //Test query filter
            DBLayer db = new DBLayer();
            var result = db.QueryRecordsByName("Mazada");

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Test_DB_Create_Success()
        {
            //Test Create record
            var car = new Car();
            car.Name = "Mazda 3";
            car.FuelLevel = 80;
            car.Mileage = 99850.5;
            
            DBLayer db = new DBLayer();
            var result = db.CreateRecord(car);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_DB_Update_Success()
        {
            //Test Update record       
            int fuelTestNumber = 100;
            DBLayer db = new DBLayer();

            //First get the fuellevel of the first record
            var vehicle = db.QueryRecordsById(1);
            if(vehicle == null)
            {
                Assert.Fail("Query records failed");
            }

            //Then determine if the next will write 100 or 50
            if(vehicle.FuelLevel == 100)
            {
                fuelTestNumber = 50;
            }
            vehicle.FuelLevel = fuelTestNumber;

            //Update the record with the new fuel number
            db.UpdateRecords(vehicle);

            //Requery the vehicle and check if the update was successful
            var vehicle2 = db.QueryRecordsById(1);
            if (vehicle2 == null)
            {
                Assert.Fail("Query records failed on repull of data.");
            }

            Assert.AreEqual(vehicle2.FuelLevel, fuelTestNumber);
        }
    }
}
