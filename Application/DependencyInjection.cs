using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Application.Products.Validators;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>() );
            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
        }
    }
}
