using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Demo.PL.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using Demo.PL.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper) //ask clr for creating object from class implement Iemprepos
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository; bqet bt3aml m3ahom btreqa indirect 3n treq unitofwork
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees =await _unitOfWork.EmployeeRepository.GetAllAsync();

            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);

            }
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployees);
            ///1. ViewData =>keyvaluepair[dictionary object]
            ///transfer data from controller[action] to its view
            ///net.fw 3.5
            ///ViewData["message"] = "hello from view data"; //e3tbrha 7aga bttb3t m3 elview aw elmodel zyada
            ///2.view bag = dynamic propeqrty [based on dynamic keyword]
            ///transfer data from controller[action] to its view.
            /// .net frameowrk 4.0
            ///ViewBag.Message = "hello from view bage";

        }
        public IActionResult Create()
        {
            //ViewBag.Departments= _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)//server side validation
            {
                ///MANUAL MAPPING
                ///var MappedEmployee = new Employee()
                ///{
                ///    Name = EmployeeVM.Name,
                ///    Age = EmployeeVM.Age,
                ///    Address = EmployeeVM.Address,
                ///    PhoneNumber = EmployeeVM.PhoneNumber,
                ///    DepartmentId = EmployeeVM.DepartmentId,
                ///};
                ///Employee employee = (Employee)employeeVM;
                employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image,"Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                //1.update
                //2.delete
                //3.update
                //_dbcontext.set<t>().add(); "dbcont hwa el unitofwork welset hya emp repo
                //_dbcont .savechanges
                await _unitOfWork.CompleteAsync(); //bdel el dbcontext save changes
                //lazm n3ml dispose 3shan el addscoped bybqa unreachable object b3d elrequest
                //hy3ml kda b idisposable gwa unitofwork
                
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        //base url/employee/details/id
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); //status code 400
            }
            var employee = await _unitOfWork.EmployeeRepository.GetbyIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image, "Images");
                    var MappedEmployee = _mapper.Map<Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //1, log exception
                    //2.form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {

            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                var result= await _unitOfWork.CompleteAsync();
                if (result > 0 && employeeVM.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }

    }
}
