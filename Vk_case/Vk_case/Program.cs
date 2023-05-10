using Microsoft.EntityFrameworkCore;
using VkDataBase.Repositories;
using VkDataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionStringDB = builder.Configuration.GetConnectionString("VkCaseDb");
builder.Services.AddDbContext<VkCaseDbContext>(options => options.UseNpgsql(connectionStringDB));
builder.Services.AddTransient<IUserRepo, UserRepo>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
