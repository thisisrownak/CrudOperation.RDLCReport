using CrudOperation.Models.Common;
using CrudOperation.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperation.Repository.IRepository
{
    public interface ICompanyRepository
    {
        Task<CommonResponseModel<CompanyListViewModel>> GetCompanyList();
    }
}
