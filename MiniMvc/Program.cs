using System;
using Microsoft.AspNetCore.Builder;
using MiniMvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var startup = new Startup();
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app, app.Environment);
Console.WriteLine($"WebRoot:    {app.Environment.WebRootPath}");
Console.WriteLine($"ContentRoot {app.Environment.ContentRootPath}");

app.Run();