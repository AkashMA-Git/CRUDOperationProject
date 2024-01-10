using CRUDOperationProject.Entity;
using CRUDOperationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperationProject.Controllers
{
    public class EmployeeController : Controller
    {   
        private readonly EntityDbContext _context;
        public EmployeeController(EntityDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Employee> employeeList = _context.Employees.ToList();
            return View(employeeList);
        }
        public IActionResult Create() { 
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }


    }
}
