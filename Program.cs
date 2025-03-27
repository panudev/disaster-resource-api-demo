using Microsoft.OpenApi.Models;
using DisasterResourceAllocationAPI.Interfaces;
using DisasterResourceAllocationAPI.Services;
using DisasterResourceAllocationAPI.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Force listen on port 80 (for Azure Container Apps)
builder.WebHost.ConfigureKestrel(serverOptions => { serverOptions.ListenAnyIP(80); });

//Add Redis Connection
var redisConfig = new RedisConfig();
builder.Configuration.GetSection("Redis").Bind(redisConfig);
var redisPasswordFromEnv = Environment.GetEnvironmentVariable("Redis__Password");
if (!string.IsNullOrEmpty(redisPasswordFromEnv))
{
    redisConfig.Password = redisPasswordFromEnv;
}

var redisConnection = $"{redisConfig.Host}:{redisConfig.Port},password={redisConfig.Password},ssl={redisConfig.UseSsl}";
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

//Add Services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Disaster Resource Allocation API",
        Version = "v1",
        Description = "API for Disaster Resource Allocation"
    });
});

//Register Services
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IRedisService, RedisService>();

var app = builder.Build();

//Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Disaster Resource Allocation API"));
// }
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Disaster Resource Allocation API"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();





