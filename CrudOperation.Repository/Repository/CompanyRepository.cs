using CrudOperation.Models.Common;
using CrudOperation.Models.ViewModel;
using CrudOperation.Repository.IRepository;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using System.Data.OleDb;

namespace CrudOperation.Repository.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IWebHostEnvironment _he;
        private readonly string? _connectionString;

        public CompanyRepository(IWebHostEnvironment he) 
        {
            _he = he;
            var _comPath = _he.WebRootPath + "\\AccessDB\\CompanyDB.accdb";
            _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _comPath + ";";
        }

        public async Task<CommonResponseModel<CompanyListViewModel>> GetCompanyList()
        {
            List<CompanyListViewModel> companyList = [];
            CommonResponseModel<CompanyListViewModel> commonResponseModel = new();
            try
            {
                using (var connection = new OleDbConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = string.Format(DapperQuery.GetCompanyList);
                    var result = await connection.QueryAsync<CompanyListViewModel>(query);
                    if (result != null && result.Any())
                    {
                        companyList = result.ToList();
                    }
                    else
                    {
                        companyList = [];
                    }
                }
                commonResponseModel.Success = true;
                commonResponseModel.Resources = companyList;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Message = ex.Message;
            }
            return commonResponseModel;
        }
    }
}
