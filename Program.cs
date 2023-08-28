using roko_test;
using roko_test.Data;
using roko_test.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;

    builder.Services.AddControllersWithViews();


    services.AddDbContext<DataContext>();


    services.AddControllersWithViews()
        .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Default database seeding
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     DataContext context = services.GetRequiredService<DataContext>();

//     await DefaultSeeds.SeedAsync(context);

//     await context.DisposeAsync();
// }


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
