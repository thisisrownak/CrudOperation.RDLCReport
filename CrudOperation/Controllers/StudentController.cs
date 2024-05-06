using AspNetCore.Reporting;
using CrudOperation.Models.ViewModel;
using CrudOperation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace CrudOperation.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitStudentInfo(StudentViewModel model)
        {
            var result = await _studentRepository.SubmitStudentInfo(model);

            if (result.Success == true)
            {
                return Json(result.Message);
            }
            else
            {
                return Json(result.Message);
            }
        }

        public async Task<IActionResult> StudentList()
        {
            var result = await _studentRepository.GetStudentList();
            return await Task.Run(() => View(result.Resources));
        }

        public async Task<IActionResult> RdlcReport(string fileType)
        {
            string mimetype = "";
            string fileExtension = "";
            int extension = 1;
            RenderType renderType = RenderType.Pdf;

            if (fileType == "pdf")
            {
                mimetype = "application/pdf";
                fileExtension = "StudentList.pdf";
                renderType = RenderType.Pdf;
            }
            else if (fileType == "word")
            {
                mimetype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                fileExtension = "StudentList.doc";
                renderType = RenderType.Word;
            }
            else if (fileType == "excel")
            {
                mimetype = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                fileExtension = "StudentList.xls";
                renderType = RenderType.Excel;
            }

            // Combine the root path with the relative path to RDLC Report
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "RDLCReport", "StudentList.rdlc");

            Dictionary<string, string> parameters = [];

            var studentList = await _studentRepository.GetStudentList();
            var dataTable = ListToDataTable(studentList.Resources);

            LocalReport localReport = new(reportPath);
            localReport.AddDataSource("Student", dataTable);
            var result = localReport.Execute(renderType, extension, parameters, mimetype);
            return File(result.MainStream, mimetype, fileExtension);
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
