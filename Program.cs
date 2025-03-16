using ISMIEEmploymentApp.Repository;
using ISMIEEmploymentApp.Services;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ICandidateApplicationRepository, CandidateApplicationRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load configuration from User Secrets
builder.Configuration.AddUserSecrets<Program>();

// Register services
builder.Services.AddSingleton<PdfService>();
builder.Services.AddSingleton<EmailService>();

// Configure Resend API
builder.Services.AddOptions();
// Configure Resend API Token from Secrets
builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken = builder.Configuration["Resend:ApiToken"];
});
builder.Services.AddHttpClient<ResendClient>();
builder.Services.AddTransient<IResend, ResendClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
