using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleTrack.Models;
using VehicleData;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleTrack.Controllers
{
    public class HomeController : Controller
    {
        private VehicleDBContext _vehicleDBContext = new VehicleDBContext(new liteDBVehicleSupplier());

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Davis County programming assigment";
            ViewData["Author"] = "By Scott Wood";

            return View();
        }

        public IActionResult VehicleList()
        {
            var vehicleList = _vehicleDBContext.GetList();
            return View(vehicleList);
        }

        [HttpGet]
        public IActionResult VehicleCreate()
        {       
            return View();
        }

        [HttpPost]
        public IActionResult VehicleCreate(VehicleModel vehicle)
        {
            bool result = _vehicleDBContext.CreateVehicle(vehicle);
            if (result)
            {
                return RedirectToAction("VehicleList");
            }
            return View();
        }

        [HttpGet]
        public IActionResult VehicleDetail(int id)
        {
            var vehicle = _vehicleDBContext.GetVehicleDetails(id);
            ViewBag.Message = "Make changes and click Save";
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult VehicleDetail(VehicleModel vehicle)
        {
            bool result = _vehicleDBContext.UpdateVehicle(vehicle);
            if(result)
            {
                ViewBag.Message = "Vehicle updated";
            }
            return View(vehicle);
        }

        public IActionResult VehicleDelete(int id)
        {
            var vehicle = _vehicleDBContext.DeleteVehicle(id);
            return RedirectToAction("VehicleList");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
