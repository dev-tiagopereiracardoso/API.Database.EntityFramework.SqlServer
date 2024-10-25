using API.Database.SqlServer.Db;
using API.Database.SqlServer.Db.Repository;
using API.Database.SqlServer.Domain.Implementation.Interfaces;
using API.Database.SqlServer.Domain.Implementation.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["SqlConnectionString"];

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseSqlServer(connectionString);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddTransient<UsersRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    if (scope != null)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();