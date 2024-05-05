using CrudOperation.Models.Common;
using CrudOperation.Models.ViewModel;

namespace CrudOperation.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<CommonResponseModel> SubmitStudentInfo(StudentViewModel model);
        Task<CommonResponseModel<StudentViewModel>> GetStudentList();
    }
}
