using Sarf.Database;
using Sarf.Logic;
using Sarf.Services;
using Sarf.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddSingleton<AuthLogic>();
builder.Services.AddSingleton<JwtUtils>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.MapGrpcService<SarfAuthService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();