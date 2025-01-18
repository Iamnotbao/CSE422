using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceCategoryManagement.Data;
using DeviceCategoryManagement.Models;
using System.Xml.Linq;

namespace DeviceCategoryManagement.Controllers
{
    public class DevicesController : Controller
    {
        private readonly DeviceCategoryManagementContext _context;
        public static List<Device> list = new List<Device>
{
    new Device(1, "Asus", "2", "computer", "In use", DateTime.Parse("1989-2-12")),
    new Device(2, "Printer", "5", "printer", "Broken", DateTime.Parse("1959-5-10")),
};
        public DevicesController(DeviceCategoryManagementContext context)
        {
            _context = context;

        }

        // GET: Devices
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StatusSortParam = (sortOrder == "Status" )? "status_desc" : "Status";
            IEnumerable<Device> sortBasedOnStatus = list;
            switch (sortOrder)
            {
                case "status_desc":
                     sortBasedOnStatus = list.OrderByDescending(d => d.Status);
                    break;
                case "Status":
                     sortBasedOnStatus = list.OrderBy(d => d.Status);
                    break;
                default:
                    break;
            }
            ViewBag.Search = new Device();
            return View(sortBasedOnStatus.ToList());

        }

        [HttpPost]
        public IActionResult Search(string name)
        {
            var filteredDevices = string.IsNullOrWhiteSpace(name) ? list : list.Where(d => d.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            ViewBag.Device = filteredDevices;
            return View("Index",filteredDevices);
        }
        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailDevice = list.FirstOrDefault(c => c.Id == id);
            if (detailDevice != null)
            {
                ViewBag.Detail = detailDevice;
                return View();
            }

            return View();
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Category,Status,DateOfEntry")] Device device)
        {
            if (ModelState.IsValid)
            {
                list.Add(device);
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editDevice = list.FirstOrDefault(c => c.Id == id);
            if (editDevice == null)
            {
                return NotFound();
            }
            return View(editDevice);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,Category,Status,DateOfEntry")] Device device)

           
        {
            var editDevice = list.FirstOrDefault(c => c.Id == id);
            if (editDevice == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    editDevice.Name = device.Name;
                    editDevice.Code = device.Code;
                    editDevice.Category = device.Category;
                    editDevice.Status = device.Status;
                    editDevice.DateOfEntry = device.DateOfEntry;
                }
                catch
                {
                    Console.WriteLine("Not ok!!!");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editDevice);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteDevice = list.FirstOrDefault(c => c.Id == id);
            if (deleteDevice == null)
            {
                return NotFound();
            }
            return View(deleteDevice);


        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteDevice = list.FirstOrDefault(c => c.Id == id);
            if (deleteDevice == null)
            {
                return Problem("Entity set 'DeviceCategoryManagementContext.Device'  is null.");
            }
            if (deleteDevice != null)
            {
                list.Remove(deleteDevice);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return (list?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
