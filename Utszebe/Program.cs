using API.Extensions;
using API.Middleware;
using Utszebe.Infrastracture.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:4200") // Add the correct origin of your client app
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("Content-Disposition")
    .AllowCredentials() // Allow credentials (cookies, authorization headers, etc.) IMPORTANTE
);

app.UseAuthorization();

app.MapHub<ResultHub>("/resultHub");
app.MapControllers();


app.Run();
