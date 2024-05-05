using Azure;

namespace CrudOperation.Models.Common
{
    public class CommonResponseModel<T>
    {
        public T? Resource { get; set; }
        public List<T?> Resources { get; set; }
        public Response Response { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
    }
    public class CommonResponseModel
    {
        public Response Response { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
    }
}
