using ecommerce_project.Data;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_project.Startup;

public static class DbConfiguration
{

    public static IServiceCollection AddDBContext(this IServiceCollection app,IConfiguration config)
    {
        app.AddDbContext<DatabaseContext>(optoins =>
        {
            optoins.UseSqlServer(config.GetConnectionString("DefaultConnction"));
        });

        return app;
    }
}
