using OrderManager.Extensions;
using OrderManager.Infrastructure.EntityFramework.Extensions;
using OrderManager.Web.Extensions;

namespace OrderManager.Web
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddEntityFramework();
            builder.Services.AddFluentValidators();
            builder.Services.AddDomainServices();
            builder.Services.AddMediatR();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                      policy =>
                                      {
                                          policy.AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowAnyOrigin();
                                      });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.MapControllers();
            app.AddData();

            app.Run();
        }
    }
}