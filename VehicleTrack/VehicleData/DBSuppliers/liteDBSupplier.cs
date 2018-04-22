using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace VehicleData
{

    //LiteDB requires a concrete class to work on the database data
    //public class VehicleData : Vehicle  {}

    public class liteDBVehicleSupplier : IVehicleDataSupplier
    {
        const string DBFILE = @"vehicle.db";
        const string DBTABLE = "vehicles";

        public liteDBVehicleSupplier()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(DBFILE))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<VehicleModel>(DBTABLE);

                // Ensure indexes are in place for querying
                col.EnsureIndex(x => x.Name);
                col.EnsureIndex(x => x.Type);
            }
        }

        public List<VehicleModel> QueryRecordsAll()
        {
            List<VehicleModel> vehicleList = new List<VehicleModel>();
            using (var db = new LiteDatabase(DBFILE))
            {
                var col = db.GetCollection<VehicleModel>(DBTABLE);
                vehicleList = (col.FindAll().ToList());
            }
            return vehicleList;
        }

        public VehicleModel QueryRecordsById(int id)
        {
            VehicleModel vehicle;
            using (var db = new LiteDatabase(DBFILE))
            {
                var col = db.GetCollection<VehicleModel>(DBTABLE);
                vehicle = (VehicleModel)col.FindById(new BsonValue(id));
            }
            return vehicle;
        }

        public List<VehicleModel> QueryRecordsByName(string nameFilter)
        {
            List<VehicleModel> vehicleList = new List<VehicleModel>();
            using (var db = new LiteDatabase(DBFILE))
            {               
                var col = db.GetCollection<VehicleModel>(DBTABLE);
                vehicleList  = (col.Find(x => x.Name.Contains(nameFilter))).ToList();            
            }
            return vehicleList;
        }
     
        public bool CreateRecord(VehicleModel vehicle)
        {
            bool result = true;
            try
            {
                using (var db = new LiteDatabase(DBFILE))
                {
                    var col = db.GetCollection<VehicleModel>(DBTABLE);
                    col.Insert(vehicle);
                }
            }
            catch 
            {
                result = false;
            }
            return result;
        }

        public bool RecordUpdate(VehicleModel vehicle)
        {
            bool result = true;
            try
            {
                using (var db = new LiteDatabase(DBFILE))
                {
                    var col = db.GetCollection<VehicleModel>(DBTABLE);
                    col.Update(vehicle);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool RecordDelete(int id)
        {
            bool result = true;
            try
            {

                using (var db = new LiteDatabase(DBFILE))
                {
                    var col = db.GetCollection<VehicleModel>(DBTABLE);
                    VehicleModel vehicle = (VehicleModel)col.FindById(new BsonValue(id));

                    if (vehicle != null)
                    {
                        col.Delete(id);
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public int RecordCount()
        {
            int count = 0;
            using (var db = new LiteDatabase(DBFILE))
            {
                var col = db.GetCollection<VehicleModel>(DBTABLE);
                count = col.Count();
            }
            return count;
        }        
    }
}
