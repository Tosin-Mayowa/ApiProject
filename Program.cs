
using ApiProject.Container;
using ApiProject.Data;
using ApiProject.Helper;
using ApiProject.Service;
using AutoMapper;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace ApiProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<ICategories,CategoryList>();
            builder.Services.AddDbContext<ApiContext>(options => options.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection")
               ));
            var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutomapperHandler()));
            IMapper mapper= autoMapper.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddCors(p => p.AddDefaultPolicy(build =>
            {
                build.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
            }));
            builder.Services.AddRateLimiter(p => p.AddFixedWindowLimiter(policyName: "fixedwindow", options =>
            {
             options.Window=TimeSpan.FromSeconds(10);
                options.PermitLimit = 1;
                options.QueueLimit = 0;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            }
                ));
            string logPath = builder.Configuration.GetSection("Logging:LogPath").Value;
            var logger=new LoggerConfiguration()
                .MinimumLevel.Error()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(logPath)
                .CreateLogger();
            builder.Logging.AddSerilog(logger);
            var app = builder.Build();


            app.UseRateLimiter();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}