
using Module34WebAPI.Configuration;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using Module34WebAPI.Contracts.Validation;
using Microsoft.EntityFrameworkCore;
using Module34WebAPI.Data;
using Module34WebAPI.Data.Repos;


namespace Module34WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = Assembly.GetAssembly(typeof(MappingProfile));
        builder.Services.AddAutoMapper(assembly);

        string connection = builder.Configuration.GetSection("ConnectionStrings").GetValue(typeof(string), "DefaultConnection").ToString();
        builder.Services.AddDbContext<Module34WebAPIContext>(options => options.UseSqlServer(connection));

        builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();

        //builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>()); // устарело
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();

        builder.Services.Configure<HomeOptions>(new ConfigurationBuilder().AddJsonFile("HomeOptions.json").Build());
        
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

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
