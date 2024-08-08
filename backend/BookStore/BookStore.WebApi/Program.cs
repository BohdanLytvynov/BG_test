using BookStore.BLL.Mapping.Authors;
using BookStore.BLL.MediatR.Author.GetAll;
using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using BookStore.DAL.Repositories.Realizations.RepositoryWrapper;
using BookStore.WebApi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DB Context
builder.Services.AddDbContext<BookStoreDbContext>(conf =>
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    if (env.Equals("Development"))
    {
        conf.EnableDetailedErrors();
    }

    var conStr = builder.Configuration.GetSection(env).GetConnectionString("BookStoreDb");

    Debug.WriteLine(conStr);

    conf.UseNpgsql(conStr);

});
// Add Identity System
builder.Services.AddIdentity<User, IdentityRole<Guid>>(conf =>
{ 
    conf.User.RequireUniqueEmail = true;
    conf.Password.RequiredLength = 7;
}).AddEntityFrameworkStores<BookStoreDbContext>()
.AddDefaultTokenProviders()
.AddApiEndpoints();

//Add Repository Wrapper as a Service

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

//Add MediatR
var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddMediatR(config =>
{ 
    config.RegisterServicesFromAssemblyContaining(typeof(GetAllAuthorsHandler));
});

//Add Mapper
builder.Services.AddAutoMapper(typeof(AuthorProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.SeedDatabaseAsync();

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
