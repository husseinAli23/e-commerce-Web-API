using ecommerce_project.Data;
using ecommerce_project.Interface;
using ecommerce_project.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
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
builder.Services.AddScoped<IAccount, AccountRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<IDiscount, DiscountRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
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

builder.Services.AddDbContext<DatabaseContext>(optoins =>
{
    optoins.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnction"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
