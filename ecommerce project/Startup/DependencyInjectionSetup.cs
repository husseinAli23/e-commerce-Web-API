namespace ecommerce_project.Startup;

using ecommerce_project.Interface;
using ecommerce_project.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

public static class DependencyInjectionSetup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(setupAction =>
        {
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setupAction.IncludeXmlComments(xmlCommentsFullPath);

            //setupAction.AddSecurityDefinition("ecommrceAuth", new OpenApiSecurityScheme()
            //{
            //    Type = SecuritySchemeType.Http,
            //    Scheme = "Bearer",
            //    Description = "Input a valid token to access this api"
            //});

            //setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //    new OpenApiSecurityScheme
            //    {
            //        Reference = new OpenApiReference
            //        {
            //            Type = ReferenceType.SecurityScheme,
            //            Id = "ecommrceAuth"}
            //    },new List<string>() }
            //});
        });
        return services;
    }
    public static IServiceCollection RegisterScoped(this IServiceCollection services)
    {

        services.AddScoped<IAccount, AccountRepository>();
        services.AddScoped<IProduct, ProductRepository>();
        services.AddScoped<ICategory, CategoryRepository>();
        services.AddScoped<IDiscount, DiscountRepository>();

        return services;
    }

    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }

    public static IServiceCollection AddControllersConfig(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers()
    .AddXmlDataContractSerializerFormatters()
    // To castumaize the ModelState and add some info
    .ConfigureApiBehaviorOptions(setupAction =>
    {
        // It will execute when the ModelState invalid
        setupAction.InvalidModelStateResponseFactory = context =>
        {
            //create a valisation problem details object
            ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

            var VaildationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState);

            // add additional info not added by default
            VaildationProblemDetails.Detail = "See the errors field for details.";
            VaildationProblemDetails.Instance = context.HttpContext.Request.Path;

            // report invalid model state responses as vaildation issues
            VaildationProblemDetails.Type = "VaildationProblemDetails";
            VaildationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            VaildationProblemDetails.Title = "One or more validation errors occurred";

            return new UnprocessableEntityObjectResult(
                VaildationProblemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

        return services;
    }
}
