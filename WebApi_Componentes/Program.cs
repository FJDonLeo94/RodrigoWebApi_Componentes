using WebApi_Componentes.Services;

namespace WebApi_Componentes;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("connectionString");

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IComponenteRepository>(provider => new AdoComponenteRepository(connectionString!));
        builder.Services.AddScoped<IOrdenadorRepository>(provider => new AdoOrdenadorRepository(connectionString!));
        builder.Services.AddScoped<IPedidoRepository>(provider => new AdoPedidoRepository(connectionString!));

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
