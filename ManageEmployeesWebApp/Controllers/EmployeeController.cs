using ManageEmployeesWebApp.DAO;
using ManageEmployeesWebApp.Models;
using ManageEmployeesWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployeesWebApp.Controllers {
    public class EmployeeController : Controller {
        private readonly AppDbContext _appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public  async Task<IActionResult> Index() {
            var employees =await _appDbContext.Employees.Select(s=>new EmployeeViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName= s.LastName,
                DateofBirth= s.DateofBirth,
            }).ToListAsync();
            return View(employees);
        }
        public async Task<JsonResult> List() {
            var employees = await _appDbContext.Employees.Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateofBirth = s.DateofBirth,
            }).ToListAsync();
            return Json(employees);
        }
        public IActionResult Entry() => View();

        [HttpPost]
        public async Task<IActionResult> Entry(EmployeeViewModel viewModel) {
            try {
                if (ModelState.IsValid) {
                    var employeeEntity = new EmployeeEntity()
                    {
                        Id = Guid.NewGuid().ToString(),//for new id when uer create the record 36 char GUID  , UUID 
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        DateofBirth = viewModel.DateofBirth,
                    };
                    _appDbContext.Employees.Add(employeeEntity);//adding the record to the products of db context
                    await _appDbContext.SaveChangesAsync();// actually save to the database 
                    TempData["Msg"] = "1 record is created successfully"; 
                }
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        public async Task< IActionResult> Delete(string Id) {
            try {
                var entity = _appDbContext.Employees.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (entity == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                _appDbContext.Employees.Remove(entity);// collect the data to remove
               await _appDbContext.SaveChangesAsync();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string Id) {
            var viewModel =await  _appDbContext.Employees.Where(x => x.Id.Equals(Id)).Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateofBirth = x.DateofBirth,
            }).SingleOrDefaultAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel viewModel) {
            try {
                //DTO >> Data Transfer Object 
                var employeeEntity = new EmployeeEntity(){
                    Id = viewModel.Id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DateofBirth = viewModel.DateofBirth,
                };
                _appDbContext.Entry(employeeEntity).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();// actually update to the database 
                TempData["Msg"] = "update process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is updated.";
            }
            return RedirectToAction("Index");
        }
    }
}
