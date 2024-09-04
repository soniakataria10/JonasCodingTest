using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeInfo> GetAllEmployee();
        EmployeeInfo GetEmployeeByCode(string employeeCode);
        bool SaveEmployee(EmployeeInfo employee);
        void DeleteEmployeeById(string id);
    }
}
