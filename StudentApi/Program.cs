using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentApi.Data;
using StudentApi.Services;
using StudentApi.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using StudentApi.Middleware;
using AutoMapper;
using StudentApi.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use Custom Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// === Minimal APIs (Map Style) for Departments ===

app.MapGet("/api/departments", async (IDepartmentService service) => 
    Results.Ok(await service.GetAllAsync()));

app.MapGet("/api/departments/{id}", async (int id, IDepartmentService service) => 
{
    var item = await service.GetByIdAsync(id);
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

app.MapPost("/api/departments", async (DepartmentCreateDto dto, IDepartmentService service, IValidator<DepartmentCreateDto> validator) => 
{
    var validationResult = await validator.ValidateAsync(dto);
    if (!validationResult.IsValid) return Results.ValidationProblem(validationResult.ToDictionary());

    var created = await service.CreateAsync(dto);
    return Results.Created($"/api/departments/{created.Id}", created);
});

app.MapPut("/api/departments/{id}", async (int id, DepartmentCreateDto dto, IDepartmentService service, IValidator<DepartmentCreateDto> validator) => 
{
    var validationResult = await validator.ValidateAsync(dto);
    if (!validationResult.IsValid) return Results.ValidationProblem(validationResult.ToDictionary());

    var success = await service.UpdateAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/api/departments/{id}", async (int id, IDepartmentService service) => 
{
    var success = await service.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    context.Database.Migrate();
    DbSeeder.Seed(context);
}

app.Run();