using System;
using TrainingAppData.DB.SQLDatabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//AddTransient<SQLServerDatabase>(): This method registers the SQLServerDatabase class as a transient service.
//A transient service is created each time it is requested. It is one of the service lifetimes in ASP.NET Core, and it is used for lightweight, stateless services.

builder.Services.AddTransient<SQLServerDatabase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

//A new scope allows you to create a separate, isolated service provider
//using statement ensures that the scope is disposed of properly when the code block is exited.
using (var scope = app.Services.CreateScope())
{
  //The GetRequiredService<SQLServerDatabase>() method retrieves an instance of the SQLServerDatabase service
  //that was registered earlier. If the service cannot be found, it will throw an exception.
  var databaseService = scope.ServiceProvider.GetRequiredService<SQLServerDatabase>();

  //databaseService.CreateDatabase("TrainingAppDB");: This line calls the CreateDatabase method
  //on the SQLServerDatabase service, creating a new database with the name "TrainingAppDB".  
  databaseService.CreateDatabase("TrainingAppDB");
}

app.Run();
