using Microsoft.EntityFrameworkCore;
using RoomChatApp.Database;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;
using RoomChatApp.Services;

namespace RoomChatApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        // Add services to the container.
        builder.Services.AddScoped<IChatService, ChatService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IChatDbOperations, ChatDbOperations>();
        builder.Services.AddScoped<IUserDbOperations, UserDbOperations>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddDbContext<RoomChatDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true));

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}