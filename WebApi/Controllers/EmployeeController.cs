using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using log4net;

namespace WebApi.Controllers
{
    [RoutePrefix("Employee/{action}")]
    public class EmployeeController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper) 
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<EmployeeDto> GetAll()
        {
            try
            {
                var items = _employeeService.GetAllEmployee();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while retrieving employees list.", ex);
                return new List<EmployeeDto>();
            }
        }

        [HttpGet]
        public EmployeeDto Get(string employeeCode)
        {
            try
            {
                var item = _employeeService.GetEmployeeByCode(employeeCode);
                return _mapper.Map<EmployeeDto>(item);
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while retrieving employee detail.", ex);
                return new EmployeeDto();
            }
        }

        [HttpPost]
        public void Post([FromBody] EmployeeDto value)
        {
            try
            {
                _employeeService.SaveEmployee(_mapper.Map<EmployeeInfo>(value));
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while ading new employee.", ex);
            }
        }

        [HttpPut]
        public void Put(int id, [FromBody] EmployeeDto value)
        {
            try
            {
                value.EmployeeCode = id.ToString();
                _employeeService.SaveEmployee(_mapper.Map<EmployeeInfo>(value));
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while updating employee detail.", ex);
            }
        }

        [HttpDelete]
        public void Delete(string id)
        {
            try
            {
                var record = _employeeService.GetEmployeeByCode(id);
                if (record != null)
                {
                    _employeeService.DeleteEmployeeById(id);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while deleting employee.", ex);
            }
        }
    }
}