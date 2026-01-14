using Microsoft.OpenApi;
using Noser_Fitness_Application.Abstractions.Behaviors;
using Noser_Fitness.Courses;
using Noser_Fitness.Domain.Events;
using Noser_Fitness.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Noser-Fitness API", Version = "v1" });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediator(
    (options) =>
    {
        options.ServiceLifetime = ServiceLifetime.Scoped;
        options.Assemblies =
        [
            typeof(Noser_Fitness_Application.DependencyInjection).Assembly,
            typeof(CourseCreatedDomainEvent).Assembly,
        ];
        options.PipelineBehaviors = [typeof(TransactionBehavior<,>)];
    }
);

var app = builder.Build();

app.MapCourseEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseHttpsRedirection();

app.Run();
