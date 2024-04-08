using CRUDOperationProject.Entity;
using CRUDOperationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperationProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EntityDbContext _context;
        public EmployeeController(EntityDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
           ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";


            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "newest_first";
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            IQueryable<Employee> employeeQuery = _context.Employees;

            if (!String.IsNullOrEmpty(searchString))
            {
                employeeQuery = employeeQuery.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_asc":
                    employeeQuery = employeeQuery.OrderBy(s => s.Name);
                    break;
                case "Date":
                    employeeQuery = employeeQuery.OrderBy(s => s.DateOfBirth);
                    break;
                case "date_desc":
                    employeeQuery = employeeQuery.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "newest_first": 
                    employeeQuery = employeeQuery.OrderByDescending(s => s.Id); 
                    break;
                default:
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<Employee>.Create(employeeQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
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

        public IActionResult View(int? id)
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

        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);

                this.AddRange(items);
            }

            public bool HasPreviousPage
            {
                get
                {
                    return (PageIndex > 1);
                }
            }

            public bool HasNextPage
            {
                get
                {
                    return (PageIndex < TotalPages);
                }
            }

            public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = source.Count();
                var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
        }


    }
}
