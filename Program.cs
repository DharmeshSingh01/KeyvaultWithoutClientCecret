global using DotNetAPIDS.Models;
global using Microsoft.EntityFrameworkCore;
global using DotNetAPIDS.Interface;
using System.Text.Json.Serialization;
using DotNetAPIDS;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Configuration;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    /*
     // This is oldway get details from appsettings

    var keyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultUrl");
    var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    var keyVaultDirectoryId = builder.Configuration.GetSection("KeyVault:DirectoryId");
    var credential = new ClientSecretCredential(keyVaultDirectoryId.Value!.ToString(), keyVaultClientId.Value!.ToString(),
        keyVaultClientSecret.Value!.ToString());

    builder.Configuration.AddAzureKeyVault(keyVaultUrl.Value!.ToString(), keyVaultClientId.Value!.ToString(),
        keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());
    var client = new SecretClient(new Uri(keyVaultUrl.Value!.ToString()), credential);
    */

    var keyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultUrl");
    var keyVaultClient= new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
    builder.Configuration.AddAzureKeyVault(keyVaultUrl.Value!.ToString(), new DefaultKeyVaultSecretManager());
    var client = new SecretClient(new Uri(keyVaultUrl.Value!.ToString()), new DefaultAzureCredential());


    builder.Services.AddDbContext<SampleDatabaseContext>(options =>
    {
        options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString());
    });

}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SampleDatabaseContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
