using HackChallengeApi.AudioHandler;
using HackChallengeApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Minio;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(connectionString));

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseCors(bld =>
{
    bld.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AudioHub>("/audiohub");
});

app.MapIdentityApi<AppUser>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


var endpoint = "31.129.105.161:9000";
var accessKey = "KB5Rirh2nCMIOJ9LuXEs";
var secretKey = "Nq8z9Lgx5RWS4BnTUBza5yOJvUqCftEpDwye3rVT";
var secure = false;
// Initialize the client with access credentials.
var minio = new MinioClient()
    .WithEndpoint(endpoint)
    .WithCredentials(accessKey, secretKey)
    .WithSSL(secure)
    .Build();

// Create an async task for listing buckets.
var getListBucketsTask = await minio.ListBucketsAsync().ConfigureAwait(false);

// Iterate over the list of buckets.
foreach (var bucket in getListBucketsTask.Buckets)
{
    Console.WriteLine(bucket.Name + " " + bucket.CreationDateDateTime);
}

app.Run();