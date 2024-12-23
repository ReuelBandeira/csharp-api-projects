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

builder.Services.AddScoped<PaginationParams>();
builder.Services.AddScoped<PaginationHeaderFilter>();

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
