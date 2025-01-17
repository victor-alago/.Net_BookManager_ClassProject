using ClassProject.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassProject;

public class Program
{
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AuthContext>();
        
        var connectionString = builder.Configuration.GetConnectionString("AuthContextConnection");
        
        builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
        
        
    }
}

