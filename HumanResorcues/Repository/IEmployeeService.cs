using HumanResorcues.Dtos;

namespace HumanResorcues.Repository
{
    public interface IEmployeeService
    {
        public Task CreateEmployee(EmployeeCreateDto employeeCreateDto);
        public Task<List<EmployeeResultDto>> GetAllEmploye();
        public Task DeleteEmployee(string id);
        public  Task<EmployeeGetByIdResultDto> GetByIdEmployee(string id);
        public Task<List<EmployeeSearchDto>> SearchEmployeeExactFirstName(EmployeeSearchDto employeeSearchDto);
        public Task<List<EmployeeSearchDto>> SearchEmployeeWildcardFirstName(EmployeeSearchDto employeeSearchDto);
        public  Task<List<EmployeeSearchDto>> SearchEmployeePrefixFirstName(EmployeeSearchDto employeeSearchDto);
        public  Task<List<EmployeeSearchDto>> SearchEmployeeFuzzyFirstName(EmployeeSearchDto employeeSearchDto);

    }
}
