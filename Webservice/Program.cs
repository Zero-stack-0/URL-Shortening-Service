using Data;
using Data.Repository;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UrlShortneningDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

//second db
//regsiter services and repository
builder.Services.AddScoped<IUrlShortService, UrlShortService>();
builder.Services.AddScoped<IUrlRecordRepository, UrlRecordRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();