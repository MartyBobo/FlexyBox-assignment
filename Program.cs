using Application.Interfaces;
using FlexyBox.Components;
using FlexyBox.Services;
using Microsoft.EntityFrameworkCore;
using MyApp.Server.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add controllers for API endpoints
builder.Services.AddControllers();

// Add HttpClient for Blazor components to call API
builder.Services.AddScoped(sp =>
{
    // For Blazor Server, we need to create HttpClient differently
    var httpClient = new HttpClient();
    
    // In development, use the current host
    if (builder.Environment.IsDevelopment())
    {
        // This will be set correctly when the app runs
        httpClient.BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "https://localhost:7200/");
    }
    else
    {
        // In production, use the configuration value
        httpClient.BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "/");
    }
    
    return httpClient;
});

// Add API Explorer and Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "FlexyBox API",
        Version = "v1",
        Description = "API for FlexyBox restaurant review application"
    });
    
    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register MediatR (still needed for service layer)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Application.Resturants.Queries.GetResturantByIdQuery).Assembly);
});

// Register restaurant service
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

// Register current user service
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

// Register user service
builder.Services.AddScoped<IUserService, UserService>();

// Register ApplicationDbContext and IApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure")));

// Register IApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());

var app = builder.Build();


// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // This will apply any pending migrations or create the database if it doesn't exist
        context.Database.Migrate();
          // Optional: Seed data here or call a seed method
        await DataSeeder.SeedSampleDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexyBox API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map API Controllers
app.MapControllers();

app.Run();