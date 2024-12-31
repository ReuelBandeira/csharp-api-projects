using Api.Domain.UseCases.BusinessUnits.Repositories;
using Api.Domain.UseCases.BusinessUnits.Repositories.Interfaces;
using Api.Domain.UseCases.BusinessUnits.Services;
using Api.Domain.UseCases.BusinessUnits.Services.Interfaces;
using Api.Domain.UseCases.EquipmentFamilys.Repositories;
using Api.Domain.UseCases.EquipmentFamilys.Repositories.Interfaces;
using Api.Domain.UseCases.EquipmentFamilys.Services;
using Api.Domain.UseCases.EquipmentFamilys.Services.Interfaces;
using Api.Domain.UseCases.TypesEquipments.Repositories;
using Api.Domain.UseCases.TypesEquipments.Repositories.Interfaces;
using Api.Domain.UseCases.TypesEquipments.Services;
using Api.Domain.UseCases.TypesEquipments.Services.Interfaces;
using Api.Domain.UseCases.TypesChecklists.Services;
using Api.Domain.UseCases.TypesChecklists.Services.Interfaces;
using Api.Domain.UseCases.TypesChecklists.Repositories;
using Api.Domain.UseCases.TypesChecklists.Repositories.Interfaces;
using Api.Infra.Database;
using Api.Shared.Helpers;
using Minio;
using Api.Domain.UseCases.FilesMinios.Repositories.Interfaces;
using Api.Domain.UseCases.FilesMinios.Repositories;
using Api.Domain.UseCases.FilesMinios.Services.Interfaces;
using Api.Domain.UseCases.FilesMinios.Services;





var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services
    .AddDbContext<AppDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

builder.Services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
builder.Services.AddScoped<IBusinessUnitService, BusinessUnitService>();

builder.Services.AddScoped<IEquipmentFamilyRepository, EquipmentFamilyRepository>();
builder.Services.AddScoped<IEquipmentFamilyService, EquipmentFamilyService>();

builder.Services.AddScoped<ITypesEquipmentRepository, TypesEquipmentRepository>();
builder.Services.AddScoped<ITypesEquipmentService, TypesEquipmentService>();

builder.Services.AddScoped<ITypesChecklistRepository, TypesChecklistRepository>();
builder.Services.AddScoped<ITypesChecklistService, TypesChecklistService>();

builder.Services.AddScoped<IFileMinioRepository, FileMinioRepository>();
builder.Services.AddScoped<IFileMinioService, FileMinioService>();

builder.Services.AddScoped<IMinioTestService, MinioTestService>();










builder.Services.AddScoped<PaginationParams>();
builder.Services.AddScoped<PaginationHeaderFilter>();

// Configuração do MinIO
// Configurar o MinioClient usando as variáveis do appsettings.json
builder.Services.AddSingleton<MinioClient>(provider =>
{
    var configuration = builder.Configuration;
    var endpoint = configuration["MinIOConnection:Endpoint"];
    var accessKey = configuration["MinIOConnection:AccessKey"];
    var secretKey = configuration["MinIOConnection:SecretKey"];
    var useSSL = configuration.GetValue<bool>("MinIOConnection:UseSSL");

    // Verificar se as configurações estão presentes
    if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
    {
        throw new ArgumentNullException("MinIO connection parameters cannot be null or empty.");
    }

    // Criar e configurar o MinioClient
    var minioClient = new MinioClient()
        .WithEndpoint(endpoint)
        .WithCredentials(accessKey, secretKey)
        .WithSSL(useSSL);

    return (MinioClient)minioClient;
});


builder.Services.AddControllers();

var swaggerConfig = builder.Configuration.GetSection("Swagger");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(swaggerConfig["Version"], new OpenApiInfo
    {
        Title = swaggerConfig["Title"],
        Description = swaggerConfig["Description"],
        Version = swaggerConfig["Version"]
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(swaggerConfig["Endpoint"], swaggerConfig["Title"]);
    });

    // Create scope for execute migrations on database.
    // app.Services.CreateScope()
    //     .ServiceProvider.GetRequiredService<AppDbContext>()
    //     .Database.Migrate();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
