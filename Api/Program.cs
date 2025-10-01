using minimal_api.extensions;
using minimal_api.Extensions;
using minimal_api.Extensions.Configs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationSetup(builder.Configuration);
var app = builder.Build();
app.MapHomeApi()
    .MapAdministradorApi()
    .MapVeiculosApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
public partial class Program { }
