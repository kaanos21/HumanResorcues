using HumanResorcues.Dtos;
using HumanResorcues.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HumanResorcues.ViewComponents
{
    public class _EmployeeSearchArea:ViewComponent
    {
        private readonly IEmployeeService _employeeService;
        public _EmployeeSearchArea(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new EmployeeSearchDto(); // Boş bir model oluşturun
            return await Task.FromResult(View(model));
        }
    }
}
