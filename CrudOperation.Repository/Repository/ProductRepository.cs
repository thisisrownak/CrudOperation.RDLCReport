using CrudOperation.Models.Common;
using CrudOperation.Models.ViewModel;
using CrudOperation.Repository.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CrudOperation.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string? _connectionString;
        private readonly IConfiguration? _configuration;

        public ProductRepository(IConfiguration? configuration) 
        { 
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<CommonResponseModel> SubmitProduct(List<ProductViewModel> products)
        {
            CommonResponseModel commonResponseModel = new();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Product", typeof(string));
                dataTable.Columns.Add("Quantity", typeof(int));
                dataTable.Columns.Add("Rate", typeof(int));
                dataTable.Columns.Add("Amount", typeof(int));

                foreach (var product in products)
                {
                    dataTable.Rows.Add(product.Product, product.Quantity, product.Rate, product.Amount);
                }

                var result = await connection.ExecuteAsync("SP_SubmitProduct", new { Products = dataTable }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    commonResponseModel.Success = true;
                    commonResponseModel.Message = "Data updated successfully!!";
                }
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Message = ex.Message;
            }
            return commonResponseModel;
        }

        public async Task<CommonResponseModel<ProductViewModel>> GetProductList()
        {
            List<ProductViewModel> productList = [];
            CommonResponseModel<ProductViewModel> commonResponseModel = new();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = string.Format(DapperQuery.GetProductList);
                    var result = await connection.QueryAsync<ProductViewModel>(query);
                    if (result != null && result.Any())
                    {
                        productList = result.ToList();
                    }
                    else
                    {
                        productList = [];
                    }
                }
                commonResponseModel.Success = true;
                commonResponseModel.Resources = productList;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
            }
            return commonResponseModel;
        }
    }
}
