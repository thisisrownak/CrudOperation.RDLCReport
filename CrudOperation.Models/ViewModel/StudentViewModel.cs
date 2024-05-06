using Microsoft.AspNetCore.Http;

namespace CrudOperation.Models.ViewModel
{
    public class StudentViewModel
    {
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public IFormFile? Picture { get; set; }
        public string? District { get; set; }

    }
}
