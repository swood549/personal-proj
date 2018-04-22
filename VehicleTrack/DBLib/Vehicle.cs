using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLibrary
{
    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double FuelLevel { get; set; }
        public int TireCount { get; set; }
        public double Mileage { get; set; }
    }

    public class Car : Vehicle
    {
        public Car()
        {
            this.Type = "Car";
            this.TireCount = 4;
        }
    }

    public class Truck : Vehicle
    {
        public Truck()
        {
            this.Type = "Truck";
            this.TireCount = 4;
        }
    }

    public class Motorcycle : Vehicle
    {
        public Motorcycle()
        {
            this.Type = "Motorcycle";
            this.TireCount = 2;
        }
    }

    public class Boat : Vehicle
    {
        public Boat()
        {
            this.Type = "Boat";
            this.TireCount = 0;
        }
    }

}
