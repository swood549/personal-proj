using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace VehicleLibrary
{
    //LiteDB requires a concrete class to work on the database data
    public class VehicleData : Vehicle  {}

    public class DBLayer
    {
        const string DBFILE = @"vehicle.db";        

        public DBLayer()
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(DBFILE))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Vehicle>("vehicles");

                // Ensure indexes are in place for querying
                col.EnsureIndex(x => x.Name);
                col.EnsureIndex(x => x.Type);
            }
        }

        public Vehicle QueryRecordsById(int id)
        {
            VehicleData vehicle;
            using (var db = new LiteDatabase(DBFILE))
            {
                var col = db.GetCollection<VehicleData>("vehicles");
                vehicle = (VehicleData)col.FindById(new BsonValue(id));
            }
            return vehicle;
        }

        public List<Vehicle> QueryRecordsByName(string nameFilter)
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            using (var db = new LiteDatabase(DBFILE))
            {               
                var col = db.GetCollection<Vehicle>("vehicles");
                vehicleList  = (col.Find(x => x.Name.Contains(nameFilter))).ToList();            
            }
            return vehicleList;
        }

     

        public bool CreateRecord(Vehicle vehicle)
        {
            bool result = true;
            try
            {
                using (var db = new LiteDatabase(DBFILE))
                {
                    var col = db.GetCollection<Vehicle>("vehicles");
                    col.Insert(vehicle);
                }
            }
            catch 
            {
                result = false;
            }
            return result;
        }

        public bool UpdateRecords(Vehicle vehicle)
        {
            bool result = true;
            try
            {
                using (var db = new LiteDatabase(DBFILE))
                {
                    var col = db.GetCollection<Vehicle>("vehicles");
                    col.Update(vehicle);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        //// Insert new customer document (Id will be auto-incremented)
        //col.Insert(vehicle);

        //// Update a document inside a collection
        //vehicle.Name = "Mazda 3 Tan";

        //col.Update(vehicle);



        // Use LINQ to query documents


        // Let's create an index in phone numbers (using expression). It's a multikey index
        //col.EnsureIndex(x => x.Phones, "$.Phones[*]");

        // and now we can query phones
        //var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
        //    }
        //}
    }
}
