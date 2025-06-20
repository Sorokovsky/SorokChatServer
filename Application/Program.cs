using Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DatabaseContext>();
var app = builder.Build();
using var scoped = app.Services.CreateScope();
var context = scoped.ServiceProvider.GetRequiredService<DatabaseContext>();
var ensured = context.Database.EnsureCreated();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();