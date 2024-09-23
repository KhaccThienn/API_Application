using API_Application.Core.Database;
using API_Application.Core.Database.InMemory;
using API_Application.Core.IRepositories;
using API_Application.Core.IServices;
using API_Application.Extensions;
using API_Application.Repositories;
using API_Application.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddDbContext<DbComicAppContext>(opts =>
{
    opts.UseSqlServer(configuration.GetConnectionString("ConnStr"));
});
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(
        options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
    );

app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.LoadDataToMemory<UserMemory, DbComicAppContext>((userInMem, dbContext) =>
{
    new UserMemorySeedAsync().SeedAsync(userInMem, dbContext).Wait();
});

app.LoadDataToMemory<GenreMemory, DbComicAppContext>((genImmem, dbContext) =>
{
    new GenreMemorySeedAsync().SeedAsync(genImmem, dbContext).Wait();
});

app.LoadDataToMemory<ActorMemory, DbComicAppContext>((actorInMem, dbContext) =>
{
    new ActorMemorySeedAsync().SeedAsync(actorInMem, dbContext).Wait();
});


app.LoadDataToMemory<DirectorMemory, DbComicAppContext>((directorInMem, dbContext) =>
{
    new DirectorMemorySeedAsync().SeedAsync(directorInMem, dbContext).Wait();
});


app.Run();
