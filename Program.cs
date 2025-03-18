using ISMIEEmploymentApp.Data;
using ISMIEEmploymentApp.Repository;
using ISMIEEmploymentApp.Services;
using Microsoft.EntityFrameworkCore;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from User Secrets
builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddControllers();

// Register Entity Framework Core with SQL Server (or Oracle)
var connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); // Change to UseOracle(connectionString) if using Oracle

// Register repositories and services
builder.Services.AddScoped<ICandidateApplicationRepository, CandidateApplicationRepository>();
builder.Services.AddSingleton<PdfService>();
builder.Services.AddSingleton<EmailService>();

// Configure Resend API Token from Secrets
builder.Services.AddOptions();
builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken = builder.Configuration["Resend:ApiToken"];
});
builder.Services.AddHttpClient<ResendClient>();
builder.Services.AddTransient<IResend, ResendClient>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsProduction())
//{
    
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
