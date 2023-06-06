using ecommerce_project.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersConfig()
                 .RegisterScoped()
                 .RegisterMapper()
                 .RegisterServices();

builder.Services.AddDBContext(builder.Configuration);

var app = builder.Build();

app.ConfigurSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
