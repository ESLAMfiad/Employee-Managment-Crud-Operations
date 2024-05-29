using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork) //ask clr to crate object from class interface
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = new DepartmentRepository();
        }
        //baseurl/department/index "3shan ywsl ll index
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        //m7tag 2 Create action el awl 3shan yro7 3la elform weltany 3shan ysubmit elform 
        [HttpGet] //elmwq3 msh byfhm 8er el httpGet
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost] //elpost hwa eldefault method llform
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid)//server side validation
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                int result = await _unitOfWork.CompleteAsync();
                //3.temp data = dictionary object
                //transfer data from action to action
                if(result > 0)
                    TempData["Message"] = "department is created";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        //base url/Department/details/id
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if(id is null)
            {
                return BadRequest(); //status code 400
            }
            var department =await _unitOfWork.DepartmentRepository.GetbyIdAsync(id.Value);
            if (department is null)
                return NotFound ();
            return View(ViewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if(id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
            return await Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // tmn3 7d ywsl ll action
        public async Task<IActionResult> Edit(Department department,[FromRoute] int id)
        {
            if(id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    await  _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    //1, log exception
                    //2.form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View (department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
         
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // tmn3 7d ywsl ll action
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();         
                try
                {
                _unitOfWork.DepartmentRepository.Delete(department);
                await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {                   
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(department);
                }                
        }
    }

}
