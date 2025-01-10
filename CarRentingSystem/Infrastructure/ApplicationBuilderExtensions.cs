namespace CarRentingSystem.Infrastructure
{
    using CarRentingSystem.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopedservices = app.ApplicationServices.CreateScope();

            var data= scopedservices.ServiceProvider.GetService<CarRentingDbContext>();

            data.Database.Migrate();

            return app;
        } 
    }
}
