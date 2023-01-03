using EshopBackend.Core.Di;
using EshopBackend.Data.DI;
using EshopBackend.WebApi.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddDataService(builder.Configuration.GetConnectionString("EshopConnectionString"));
builder.Services.AddCoreServices();

var app = builder.Build();
app.UseStaticFiles();
app.UseCors("any");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
