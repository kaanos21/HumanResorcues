using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using HumanResorcues.Dtos;
using HumanResorcues.Entities;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResorcues.Repository
{
    public class EmployeManager : IEmployeeService
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "employees";
        public EmployeManager(ElasticsearchClient client)
        {
            _client = client;
        }
        public async Task CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employee = new Employee
            {
                FirstName = employeeCreateDto.FirstName,
                LastName = employeeCreateDto.LastName,
                Age = employeeCreateDto.Age,
                Gender = employeeCreateDto.Gender,
                Salary = employeeCreateDto.Salary,
                Department = employeeCreateDto.Department,
                Position = employeeCreateDto.Position,
                HireDate = DateTime.Now,
                PhoneNumber = employeeCreateDto.PhoneNumber,
                Email = employeeCreateDto.Email,
                IsActive = true,
                Education = new Education
                {
                    UniversityName = employeeCreateDto.UniversityName,
                    Department = employeeCreateDto.EducationDepartment
                }
            };

            var response = await _client.IndexAsync(employee, idx => idx
                .Index(indexName)
                .Id(employee.Id) 
            );
        }
        public async Task<List<EmployeeResultDto>> GetAllEmploye()
        {
            var value = await _client.SearchAsync<Employee>(s => s
                .Index(indexName)
                .Query(q => q.MatchAll(_ => { })) 
            );
            var result = value.Hits.Select(hit => new EmployeeResultDto
            {
                Id = hit.Id, // `Id` değerini al
                FirstName = hit.Source.FirstName,
                LastName = hit.Source.LastName,
                Age = hit.Source.Age,
                Gender = hit.Source.Gender,
                Salary = hit.Source.Salary,
                Department = hit.Source.Department,
                Position = hit.Source.Position,
                HireDate = hit.Source.HireDate,
                PhoneNumber = hit.Source.PhoneNumber,
                Email = hit.Source.Email,
                IsActive = hit.Source.IsActive,
                UniversityName = hit.Source.Education?.UniversityName ?? string.Empty,
                EducationDepartment = hit.Source.Education?.Department ?? string.Empty
            }).ToList();

            return result;
        }
        public async Task DeleteEmployee(string id)
        {
            await _client.DeleteAsync<Employee>(id, x => x.Index(indexName));
        }
        public async Task<EmployeeGetByIdResultDto> GetByIdEmployee(string id)
        {
            var value = await _client.GetAsync<Employee>(id, x => x.Index(indexName));
            EmployeeGetByIdResultDto resultDto = new EmployeeGetByIdResultDto()
            {
                Id = value.Id, 
                FirstName = value.Source.FirstName,
                LastName = value.Source.LastName,
                Age = value.Source.Age,
                Gender = value.Source.Gender,
                Salary = value.Source.Salary,
                Department = value.Source.Department,
                Position = value.Source.Position,
                HireDate = value.Source.HireDate,
                PhoneNumber = value.Source.PhoneNumber,
                Email = value.Source.Email,
                IsActive = value.Source.IsActive,
                UniversityName = value.Source.Education.UniversityName,
                EducationDepartment = value.Source.Education.Department
            };
            return resultDto;
        }
        public async Task<List<EmployeeSearchDto>> SearchEmployeeExactFirstName(EmployeeSearchDto employeeSearchDto)
        {
            var searchResponse = await _client.SearchAsync<Employee>(s => s
                      .Index(indexName)
                      .Query(q => q
                      .Match(m => m
                      .Field(f => f.FirstName)
                      .Query(employeeSearchDto.FirstName))));
            var result=searchResponse.Hits.ToList();
            var value=result.Select(hit=> new EmployeeSearchDto
            {
                Id = hit.Id,
                FirstName = hit.Source.FirstName,
                LastName = hit.Source.LastName,
                Age = hit.Source.Age,
                Gender = hit.Source.Gender,
                Salary = hit.Source.Salary,
                Department = hit.Source.Department,
                Position = hit.Source.Position,
                HireDate = hit.Source.HireDate,
                PhoneNumber = hit.Source.PhoneNumber,
                Email = hit.Source.Email,
                IsActive = hit.Source.IsActive,
                UniversityName = hit.Source.Education?.UniversityName ?? string.Empty,
                EducationDepartment = hit.Source.Education?.Department ?? string.Empty
            }).ToList();
            return value;
        }
        public async Task<List<EmployeeSearchDto>> SearchEmployeeWildcardFirstName(EmployeeSearchDto employeeSearchDto)
        {
            if (string.IsNullOrEmpty(employeeSearchDto.FirstName))
            {
                return new List<EmployeeSearchDto>();
            }

            var searchResponse = await _client.SearchAsync<Employee>(s => s
                .Index(indexName)
                .Query(q => q
                    .Wildcard(w => w
                        .Field(f => f.FirstName)
                        .Value($"{employeeSearchDto.FirstName.ToLower()}*") 
                    )
                )
            );

            var result = searchResponse.Hits.ToList();
            var employeeDtos = result.Select(hit => new EmployeeSearchDto
            {
                Id = hit.Id,
                FirstName = hit.Source.FirstName,
                LastName = hit.Source.LastName,
                Age = hit.Source.Age,
                Gender = hit.Source.Gender,
                Salary = hit.Source.Salary,
                Department = hit.Source.Department,
                Position = hit.Source.Position,
                HireDate = hit.Source.HireDate,
                PhoneNumber = hit.Source.PhoneNumber,
                Email = hit.Source.Email,
                IsActive = hit.Source.IsActive,
                UniversityName = hit.Source.Education?.UniversityName ?? string.Empty,
                EducationDepartment = hit.Source.Education?.Department ?? string.Empty
            }).ToList();

            return employeeDtos;
        }
        public async Task<List<EmployeeSearchDto>> SearchEmployeePrefixFirstName(EmployeeSearchDto employeeSearchDto)
        {
            if (string.IsNullOrEmpty(employeeSearchDto.FirstName))
            {
                return new List<EmployeeSearchDto>();
            }

            var searchResponse = await _client.SearchAsync<Employee>(s => s
                .Index(indexName)
                .Query(q => q
                    .Prefix(p => p
                        .Field(f => f.FirstName.Suffix("keyword"))
                        .Value(employeeSearchDto.FirstName.ToLower()) 
                    )
                )
            );

            var result = searchResponse.Hits.ToList();
            var employeeDtos = result.Select(hit => new EmployeeSearchDto
            {
                Id = hit.Id,
                FirstName = hit.Source.FirstName,
                LastName = hit.Source.LastName,
                Age = hit.Source.Age,
                Gender = hit.Source.Gender,
                Salary = hit.Source.Salary,
                Department = hit.Source.Department,
                Position = hit.Source.Position,
                HireDate = hit.Source.HireDate,
                PhoneNumber = hit.Source.PhoneNumber,
                Email = hit.Source.Email,
                IsActive = hit.Source.IsActive,
                UniversityName = hit.Source.Education?.UniversityName ?? string.Empty,
                EducationDepartment = hit.Source.Education?.Department ?? string.Empty
            }).ToList();

            return employeeDtos;
        }
        public async Task<List<EmployeeSearchDto>> SearchEmployeeFuzzyFirstName(EmployeeSearchDto employeeSearchDto)
        {
            if (string.IsNullOrEmpty(employeeSearchDto.FirstName))
            {
                return new  List<EmployeeSearchDto>();
            }

            var searchResponse = await _client.SearchAsync<Employee>(s =>
                s.Index(indexName)
                 .Query(q => q.Fuzzy(fu => fu
                    .Field(f => f.FirstName.Suffix("keyword")) 
                    .Value(employeeSearchDto.FirstName.ToLower())
                    .Fuzziness(new Fuzziness(1)) 
                 ))
            );

            var employeeDtos = searchResponse.Hits.Select(hit => new EmployeeSearchDto
            {
                Id = hit.Id,
                FirstName = hit.Source.FirstName,
                LastName = hit.Source.LastName,
                Age = hit.Source.Age,
                Gender = hit.Source.Gender,
                Salary = hit.Source.Salary,
                Department = hit.Source.Department,
                Position = hit.Source.Position,
                HireDate = hit.Source.HireDate,
                PhoneNumber = hit.Source.PhoneNumber,
                Email = hit.Source.Email,
                IsActive = hit.Source.IsActive,
                UniversityName = hit.Source.Education?.UniversityName ?? string.Empty,
                EducationDepartment = hit.Source.Education?.Department ?? string.Empty
            }).ToList();

            return employeeDtos;
        }




    }
}