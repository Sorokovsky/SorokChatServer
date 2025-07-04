using SorokChatServer.Database;
using SorokChatServer.Database.Repositories;
using SorokChatServer.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();