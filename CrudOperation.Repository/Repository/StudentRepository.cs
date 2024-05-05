using CrudOperation.Models.Common;
using CrudOperation.Models.ViewModel;
using CrudOperation.Repository.IRepository;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Drawing;

namespace CrudOperation.Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IWebHostEnvironment _he;
        private readonly string? _connectionString;
        private readonly IConfiguration? _configuration;
        public StudentRepository(IConfiguration? configuration, IWebHostEnvironment he) 
        {
            _he = he;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CommonResponseModel> SubmitStudentInfo(StudentViewModel model)
        {
            CommonResponseModel commonResponseModel = new();
            string fileName = "", _ext = "", PhotoPath = "";
            try
            {
                if (model.Picture?.Length > 0)
                {
                    fileName = Path.GetFileName(model.Picture.FileName);
                    _ext = Path.GetExtension(model.Picture.FileName);

                    if (_ext == "")
                    {
                        _ext = ".jpg";
                    }
                    var _comPath = _he.WebRootPath + "\\Images\\";
                    PhotoPath = "Picture" + DateTime.Now.Second + "" + DateTime.Now.Minute + "" + DateTime.Now.Day + "" + DateTime.Now.Month + "" + DateTime.Now.Year + "_Resized" + _ext;

                    System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(model.Picture.OpenReadStream());

                    Bitmap? b = new Bitmap(sourceimage);
                    Image? i = b;

                    i.Save(_comPath + PhotoPath);
                    i.Dispose();
                    sourceimage.Dispose();
                    b.Dispose();
                }
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var result = await connection.ExecuteAsync("SP_InsertStudentInfo", new
                {
                    Name = model.Name,
                    FatherName = model.FatherName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Picture = PhotoPath,
                    District = model.District
                },
                commandType: CommandType.StoredProcedure, commandTimeout: 0);

                if (result > 0)
                {
                    commonResponseModel.Success = true;
                    commonResponseModel.Message = "Data saved successfully!!";
                }
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Message = ex.Message;
            }
            return commonResponseModel;
        }

        public async Task<CommonResponseModel<StudentViewModel>> GetStudentList()
        {
            List<StudentViewModel> studentList = [];
            CommonResponseModel<StudentViewModel> commonResponseModel = new();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = string.Format(DapperQuery.GetStudentList);
                    var result = await connection.QueryAsync<StudentViewModel>(query);
                    if (result != null && result.Any())
                    {
                        studentList = result.ToList();
                    }
                    else
                    {
                        studentList = [];
                    }
                }
                commonResponseModel.Success = true;
                commonResponseModel.Resources = studentList;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resources = studentList;
                return commonResponseModel;
            }
        }
    }
}
