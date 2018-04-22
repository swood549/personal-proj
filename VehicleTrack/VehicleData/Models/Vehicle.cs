using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleData
{

    //Vehicle Model
    public class VehicleModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double FuelLevel { get; set; }
        public string TireCount { get; set; }
        public double Mileage { get; set; }
    }

    //A producer to create vehicles based on Factory Methods. 
    //This ensure that each vehicle is created with its specific traits
    public class VehicleProducer : VehicleModel
    {
        public VehicleProducer()
        {

        }

        public VehicleProducer(VehicleModel vehicleData, VehiclFactory factory)
        {
            this.Name = vehicleData.Name;
            this.Mileage = vehicleData.Mileage;
            this.FuelLevel = vehicleData.FuelLevel;
            this.Type = factory.Type;
            this.TireCount = factory.TireCount;
        }
    }

    public abstract class VehiclFactory
    {
        public abstract string Type { get; }
        public abstract string TireCount { get; }
    }

    //Vehicle Factories to load through a producer
    public class CarFactory : VehiclFactory
    {
        public override string Type { get { return "Car"; } }
        public override string TireCount { get { return "4"; } }
    }
    public class TruckFactory : VehiclFactory
    {
        public override string Type { get { return "Truck"; } }
        public override string TireCount { get { return "4"; }}
    }
    public class MotorcycleFactory : VehiclFactory
    {
        public override string Type { get { return "Motorcycle"; }}
        public override string TireCount { get { return "2"; } }
    }
    public class BoatFactory : VehiclFactory
    {
        public override string Type { get { return "Boat"; } }
        public override string TireCount { get { return "NA"; } }
    }



    //Interface for any Vehicle database supplier
    public interface IVehicleDataSupplier
    {
        void Initialize();
        List<VehicleModel> QueryRecordsAll();
        VehicleModel QueryRecordsById(int id);
        List<VehicleModel> QueryRecordsByName(string nameFilter);
        bool CreateRecord(VehicleModel vehicle);
        bool RecordUpdate(VehicleModel vehicle);
        bool RecordDelete(int id);
        int RecordCount();
    }

    //Implementation of the Vehicle Database Context which will take a database supplier
    //and encapsulate all the abstrated methods for CRUD
    public class VehicleDBContext
    {

        private IVehicleDataSupplier _dbSupplier;

        public VehicleDBContext(IVehicleDataSupplier dbSupplier)
        {
            _dbSupplier = dbSupplier;
            Initialize();
        }


        private void Initialize()
        {
            //Seed the database if this our first use
            if (_dbSupplier.QueryRecordsById(1) == null)
            {
                //Load sample records
                var car = new VehicleModel();
                car.Type = "car";
                car.Name = "Honda Civic";
                car.FuelLevel = 50;
                car.Mileage = 120;
                CreateVehicle(car);

                var truck = new VehicleModel();
                truck.Type = "truck";
                truck.Name = "Ford F150";
                truck.FuelLevel = 20;
                truck.Mileage = 240;
                CreateVehicle(truck);

                var motorcycle = new VehicleModel();
                motorcycle.Type = "motorcycle";
                motorcycle.Name = "Harley Davidson";
                truck.FuelLevel = 92;
                motorcycle.Mileage = 25;
                CreateVehicle(motorcycle);

                var boat = new VehicleModel();
                boat.Type = "boat";
                boat.Name = "Bertram";
                boat.FuelLevel = 10;
                boat.Mileage = 35;
                CreateVehicle(boat);

            }
        }

        public bool CreateVehicle(VehicleModel vehicleData)
        {
            bool result = true;

            switch (vehicleData.Type)
            {
                case "car":
                    var car = new VehicleProducer(vehicleData, new CarFactory());
                    result = _dbSupplier.CreateRecord(car);
                    break;
                case "truck":
                    var truck = new VehicleProducer(vehicleData, new TruckFactory());
                    result = _dbSupplier.CreateRecord(truck);
                    break;
                case "motorcycle":
                    var motorcycle = new VehicleProducer(vehicleData, new MotorcycleFactory());
                    result = _dbSupplier.CreateRecord(motorcycle);
                    break;
                case "boat":
                    var boat = new VehicleProducer(vehicleData, new BoatFactory());
                    result = _dbSupplier.CreateRecord(boat);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public bool UpdateVehicle(VehicleModel vehicle)
        {
            bool result;
            result = _dbSupplier.RecordUpdate(vehicle);
            return result;
        }

        public bool DeleteVehicle(int id)
        {
            bool result;
            result = _dbSupplier.RecordDelete(id);
            return result;
        }

        public IEnumerable<VehicleModel> GetList()
        {
            List<VehicleModel> result;
            result = _dbSupplier.QueryRecordsAll();

            IEnumerable<VehicleModel> vehicleData = result;
            return vehicleData;
        }

        public IEnumerable<VehicleModel> GetList(string filter)
        {
            List<VehicleModel> result;
            result = _dbSupplier.QueryRecordsByName(filter);

            IEnumerable<VehicleModel> vehicleData = result;
            return vehicleData;
        }

        public VehicleModel GetVehicleDetails(int id)
        {
            VehicleModel result;
            result = _dbSupplier.QueryRecordsById(id);
            return result;
        }

    }    
}
