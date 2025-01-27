using TrainingApp_WebAPI.SERVICE;
using TrainingApp_WebAPI.SERVICE.INTERFACE;
using TrainingApp_WebAPI.UTILITY;
using TrainingApp_WebAPI.VIEWMODEL;
using TrainingAppData.MODEL;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IApiService, ApiService>();
builder.Services.AddScoped(typeof(IUserApiService), typeof(UserApiService));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
