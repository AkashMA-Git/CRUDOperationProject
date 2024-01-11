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
       
        [HttpPost]
        public IActionResult Create(Employee obj)
        {   
            if(ModelState.IsValid)
            {
                _context.Employees.Add(obj);
                _context.SaveChanges();
                TempData["Success"] = "New Employee Added Successfully";
                return RedirectToAction("Index");
            }
               return View();
           
        }
       

        
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {   
                return NotFound();
            }
            Employee? employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            if(employee == null)
            {
                return NotFound();

            }

            return View(employee);

        }
        [HttpPost]
        public IActionResult Update(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(obj);
                _context.SaveChanges();
                TempData["Success"] = " Employee details updated Successfully";

                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Employee? employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();

            }

            return View(employee);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {   
            Employee? obj = _context.Employees.FirstOrDefault(x =>x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(obj);
            _context.SaveChanges();
            TempData["Success"] = "  Employee details deleted Successfully";

            return RedirectToAction("Index");
            
            
        }


    }
}
