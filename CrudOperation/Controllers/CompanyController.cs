using CrudOperation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IActionResult> CompanyList()
        {
            var result = await _companyRepository.GetCompanyList();
            return await Task.Run(() => View(result.Resources));
        }
    }
}
