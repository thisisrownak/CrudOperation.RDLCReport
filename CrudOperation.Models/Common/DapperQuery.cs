using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperation.Models.Common
{
    public static class DapperQuery
    {
        public const string InsertStudentInfo = "INSERT INTO StudentInfo (Name, FatherName, DateOfBirth, Gender, Picture, District) VALUES (@Name, @FatherName, @DateOfBirth, @Gender, @Picture, @District)";
        public const string GetStudentList = "SELECT Name, FatherName, DateOfBirth, Gender, District FROM StudentInfo";
    }
}
