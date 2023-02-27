using CustomersApi.CasosDeUso;
using CustomersApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var urlAceptadas = builder.Configuration.GetSection("AllowedHosts").Value.Split(",");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing=>routing.LowercaseUrls = true);

builder.Services.AddDbContext<System_customers>(mysqlbuilder =>
{
    mysqlbuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});

builder.Services.AddScoped<IUpdateCustomerUserCase, UpdateCustomerUserCase>();

var app = builder.Build();

app.UseCors(builder => builder.WithOrigins(urlAceptadas)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                            );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapGet("/customer/{id}", (long id) =>
//{
//    return "net 6";
//});

app.MapControllers();

app.Run();
