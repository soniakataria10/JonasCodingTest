using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository EmployeeRepository, IMapper mapper)
        {
            _EmployeeRepository = EmployeeRepository;
            _mapper = mapper;
        }
        public IEnumerable<EmployeeInfo> GetAllEmployee()
        {
            var res = _EmployeeRepository.GetAll();

            // map data manually to get desired output specially company name
            var empList = res.Select(x => new EmployeeInfo 
            { 
                EmployeeCode = x.EmployeeCode,
                EmployeeName = x.EmployeeName,
                CompanyName = x.Company.CompanyName,
                OccupationName = x.Occupation,
                EmployeeStatus = x.EmployeeStatus,
                EmailAddress = x.EmailAddress,
                PhoneNumber = x.Phone,
                LastModifiedDateTime = x.LastModified.ToString()
            });
            return empList;
        }

        public EmployeeInfo GetEmployeeByCode(string EmployeeCode)
        {
            EmployeeInfo employee = new EmployeeInfo();
            var result = _EmployeeRepository.GetByCode(EmployeeCode);

            // map data manually because according to the requirements Employee model props names are different than EmployeeInfo model
            if (result != null)
            {
            employee.EmployeeCode = result.EmployeeCode;
            employee.EmployeeName = result.EmployeeName;
            employee.CompanyName = result.Company.CompanyName;
            employee.OccupationName = result.Occupation;
            employee.EmployeeStatus = result.EmployeeStatus;
            employee.EmailAddress = result.EmailAddress;
            employee.PhoneNumber = result.Phone;
            employee.LastModifiedDateTime = result.LastModified.ToString();
            }
            return employee;
        }
        public bool SaveEmployee(EmployeeInfo EmployeeInfo)
        {
            // map data manually because according to the requirements Employee model props names are different than EmployeeInfo model

            Employee Employee = new Employee
            {
                EmployeeCode = EmployeeInfo.EmployeeCode,
                EmployeeName = EmployeeInfo.EmployeeName,
                CompanyCode = EmployeeInfo.CompanyCode,
                Occupation = EmployeeInfo.OccupationName,
                EmployeeStatus = EmployeeInfo.EmployeeStatus,
                EmailAddress = EmployeeInfo.EmailAddress,
                Phone = EmployeeInfo.PhoneNumber,
                LastModified = Convert.ToDateTime(EmployeeInfo.LastModifiedDateTime)
        };
            return _EmployeeRepository.SaveEmployee(Employee);
        }
        public void DeleteEmployeeById(string id)
        {
            _EmployeeRepository.Delete(id);
        }
    }
}
