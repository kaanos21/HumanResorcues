using HumanResorcues.Dtos;
using HumanResorcues.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace HumanResorcues.Controllers
{
    public class HumanResourceController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public HumanResourceController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto dto)
        {
            await _employeeService.CreateEmployee(dto);
            return View("AFERİNBAŞARDINBAŞARDIN");
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var employees = await _employeeService.GetAllEmploye();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _employeeService.DeleteEmployee(id);
            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(string id)
        {
            var vv= await _employeeService.GetByIdEmployee(id);
            return View(vv);
        }
        [HttpGet]
        public IActionResult SearchEmployeePage(EmployeeSearchDto searchDto)
        {
            // Eğer searchDto null ise boş bir liste döndür
            if (searchDto == null)
            {
                searchDto = new EmployeeSearchDto();
            }
            return View(new List<EmployeeSearchDto>());
        }
        [HttpPost]
        public async Task<IActionResult> SearchEmployee(EmployeeSearchDto searchDto)
        {
            // Arama sonuçlarını alın
            var employees = await _employeeService.SearchEmployeeExactFirstName(searchDto);
            // Sonuçları aynı türde bir liste ile görünümü döndür
            return View("SearchEmployeePage", employees); // Burada employees EmployeeSearchDto türünde olmalı
        }
        [HttpPost] 
        public async Task<IActionResult> SearchEmployeeWildCardFirstName(EmployeeSearchDto searchDto)
        {
            var employees = await _employeeService.SearchEmployeeWildcardFirstName(searchDto);
            // Sonuçları aynı türde bir liste ile görünümü döndür
            return View("SearchEmployeePage", employees);
        }
        [HttpPost]
        public async Task<IActionResult> SearchEmployeePrefixFirstName(EmployeeSearchDto searchDto)
        {
            var searchResponse = await _employeeService.SearchEmployeePrefixFirstName(searchDto);
            return View("SearchEmployeePage", searchResponse);
        }
        [HttpPost]
        public async Task<IActionResult> SearchEmployeeFuzzyFirstName(EmployeeSearchDto searchDto)
        {
            // Arama sonuçlarını alın
            var employees = await _employeeService.SearchEmployeeFuzzyFirstName(searchDto);
            // Sonuçları aynı türde bir liste ile görünümü döndür
            return View("SearchEmployeePage", employees); // Burada employees EmployeeSearchDto türünde olmalı
        }

    }
}
