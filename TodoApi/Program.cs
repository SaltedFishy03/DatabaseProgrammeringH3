using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("TodoItems_WebApiDBConnectionString");
builder.Services.AddDbContext<TodoContext>(o => o.UseSqlServer(connectionString));
// builder.Services.AddDbContext<TodoContext>(opt =>
    // opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Automatically open Swagger in the default browser
    var url = "http://localhost:5213/swagger"; // Update port if needed
    Task.Run(() => {
        // Delay for a moment to ensure the app is fully started
        Thread.Sleep(1000); 
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true // This opens the URL in the default browser
        });
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();