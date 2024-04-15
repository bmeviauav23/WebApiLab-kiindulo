using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;


using WebApiLab.Bll.Dtos;
using WebApiLab.Bll.Exceptions;
using WebApiLab.Bll.Interfaces;
using WebApiLab.Bll.Services;
using WebApiLab.Dal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = context =>
        {
            if (context.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is EntityNotFoundException ex)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.ProblemDetails.Title = "Invalid ID";
                context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                context.ProblemDetails.Detail = $"No product with ID {ex.Id}";
            }
        }
    );

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(WebApiProfile));

builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
