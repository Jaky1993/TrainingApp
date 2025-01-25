using TrainingApp.UTILITY;
using TrainingAppData.DB.DBCONTROLLER.USER.Sql;
using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

//When you register a service as transient, a new instance of the service is created each time it is requested
//Singleton: The same instance of the service is used for every request throughout the application's lifetime.
//This is useful for state that should be shared across the application.

/*
Transient: Ogni volta che un servizio viene richiesto, viene creata una nuova istanza del servizio. 
Ad esempio, se il servizio è richiesto cinque volte nel corso di una singola richiesta HTTP,
verranno create cinque istanze distinte.

Scoped: Una nuova istanza del servizio viene creata per ogni richiesta. Tuttavia, durante quella singola richiesta,
la stessa istanza del servizio verrà utilizzata ovunque sia richiesta.
Ad esempio, se il servizio è richiesto cinque volte nel corso di una singola richiesta HTTP,
verrà utilizzata la stessa istanza per tutte e cinque le richieste.
*/

builder.Services.AddScoped(typeof(ICreate<User>), typeof(CreateSqlUser));
builder.Services.AddScoped(typeof(IUpdate<User>), typeof(UpdateSqlUser));
builder.Services.AddScoped(typeof(IDelete<User>), typeof(DeleteSqlUser));
builder.Services.AddScoped(typeof(ISelect<User>), typeof(SelectSqlUser));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "user",
    pattern: "{controller=User}/{action=List}")
    .WithStaticAssets();


app.Run();
