using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Connection DataBase
            var connetionString = builder.Configuration.GetConnectionString("DefaultConntion");
            builder.Services.AddDbContext<AppDBcontext>(options=>
            options.UseSqlServer(connetionString));
            #endregion

            #region services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            builder.Services.AddSwaggerGen();
            #endregion
            var app = builder.Build();
            #region  Configure the HTTP request 
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #endregion

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}