using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperation.Models.Common
{
    public static class DapperQuery
    {
        public const string GetStudentList = "SELECT Name, FatherName, FORMAT (DateOfBirth, 'MMM dd-yyyy') DateOfBirthString, Gender, District FROM StudentInfo";
        public const string GetProductList = "SELECT Product, Quantity, Rate, Amount FROM ProductInfo";
        public const string GetCompanyList = "SELECT CompanyName, Address, Country FROM CompanyName";
    }
}
