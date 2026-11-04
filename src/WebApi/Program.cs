using Asp.Versioning;

using HomeBudgetManager.Bff.WebUI.WebApi.Setup;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new QueryStringApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddMvc()
    .AddOpenApi();

builder.AddCors();
builder.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().WithDocumentPerVersion();
    app.MapScalarApiReference(options =>
    {
        options.AddDocument("v1", "v1", "/openapi/v1.json", true);
    });
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
