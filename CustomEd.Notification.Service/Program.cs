using System.Text;
using CustomEd.Notification.Service.Hubs;
using CustomEd.Notification.Service.Models;
using CustomEd.Shared.Data;
using CustomEd.Shared.JWT;
using CustomEd.Shared.JWT.Interfaces;
using CustomEd.Shared.RabbitMQ;
using CustomEd.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddMongo();
builder.Services.AddMassTransitWithRabbitMq();
builder.Services.AddPersistence<Notification>("Notification");
builder.Services.AddPersistence<StudentNotification>("StudentNotification");
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IdentityProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddAuth();







var app = builder.Build();

app.UseRouting();
app.UseAuthentication(); 


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/getNotification");
});

await app.RunAsync();
