using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;
using log4net;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CompanyController));
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                var items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while retrieving companies list.", ex);
                return new List<CompanyDto>();
            }
        }

        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCode(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while retrieving employee detail.", ex);
                return new CompanyDto();
            }
        }

        public async Task Post([FromBody]CompanyDto value)
        {
            try
            {
                await _companyService.SaveCompany(_mapper.Map<CompanyInfo>(value));
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while ading new company.", ex);
            }
        }

        public void Put(int id, [FromBody] CompanyDto value)
        {
            try
            {
                value.CompanyCode = id.ToString();
                _companyService.SaveCompany(_mapper.Map<CompanyInfo>(value));
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while updating company detail.", ex);
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var record = await _companyService.GetCompanyByCode(id);
                if (record != null)
                {
                    await _companyService.DeleteCompanyById(id);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while deleting company.", ex);
            }
        }
    }
}