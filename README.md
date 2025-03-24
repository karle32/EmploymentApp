# EmploymentApp

This is the backend API for an Employment Application (the basic information), a demo employment application system. It provides an API for submitting job applications, generating PDFs, and sending confirmation emails.

## Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Resend API key (for sending emails)](https://resend.com/docs/send-with-dotnet) 
- [Entity Framework Core tools](https://learn.microsoft.com/en-us/ef/)
- [QuestPDF (for PDF generation)](https://www.questpdf.com/getting-started.html)

## Setup Instructions

1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-repo/employment-app.git
   cd employment-app
   ```

2. **Configure the database:**
   - Update the `appsettings.json` with your SQL Server connection string:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EmploymentAppDB;Trusted_Connection=True;"
     }
     ```
   - Run database migrations:
     ```sh
     dotnet ef database update
     ```

3. **Set up environment variables:**
   - Create a `.env` file (or set environment variables in your OS) with your Resend API key:
     ```sh
     RESEND_API_KEY=your-resend-api-key
     ```

4. **Run the application:**
   ```sh
   dotnet run
   ```

## API Endpoints

### Submit an Application
   ```http
   POST /api/candidate/submit
   ```
   **Request Body:**
   ```json
   {
     "firstName": "John",
     "lastName": "Doe",
     "email": "john.doe@example.com",
     "phone": "123-456-7890",
     "address": "123 Main St",
     "city": "Anytown",
     "stateProvince": "CA",
     "zipPostalCode": "12345"
   }
   ```

### Get All Applications
   ```http
   GET /api/candidate
   ```

## Troubleshooting

### Error: Cannot consume scoped service from singleton
**Fix:** Ensure the repository is registered as a `Scoped` service in `Program.cs`:
```csharp
services.AddScoped<ICandidateApplicationRepository, CandidateApplicationRepository>();
```

### Error: Missing Resend API Key
**Fix:** Ensure the `RESEND_API_KEY` environment variable is set.

## Deployment

To deploy the application:

1. **Build the application:**
   ```sh
   dotnet publish -c Release -o out
   ```

2. **Run the published application:**
   ```sh
   dotnet out/EmploymentApp.dll
   ```

## License
This project is for demonstration purposes only.

