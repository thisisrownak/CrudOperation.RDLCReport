using CrudOperation.Repository.IRepository;
using CrudOperation.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CrudOperation.Configuration.Scope
{
    public static class ScopeExtensionService
    {
        public static void ConfigureScopeExtension(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}
