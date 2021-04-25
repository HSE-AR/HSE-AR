using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.CompanyService;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.Infrastructure;
using HseAr.Scanner.Api.Helpers;
using HseAr.Scanner.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.Scanner.Api.Controllers
{
    [Authorize]
    [Route("sapi/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение списка доступных команд
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CompanyModel>>> Get()
        {
            var companies = await _companyService.GetCompaniesByUserId(this.GetUserIdFromToken());

            return companies.Select(company => _mapper.Map<CompanyContext, CompanyModel>(company)).ToList();
        }
    }
}