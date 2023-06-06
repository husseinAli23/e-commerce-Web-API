namespace ecommerce_project.Startup;

public static class SwaggerConfiguration
{
    public static WebApplication ConfigurSwagger(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
