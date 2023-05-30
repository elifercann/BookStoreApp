
using BookStoreApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Presentation.ActionFilters;
using Services.Abstract;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
//applicationpart ile reflection konuldu presentation katmanýný da assemnlyrefence sýnýfý sayesinde çözümleyebiliyor oradaki controllerda kullanýlabiliyor bu sayede
builder.Services.AddControllers(config =>
{
    //icerik pazarlýgý icin gerekli 
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddCustomCsvFormatter()
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.AssemblyRefence).Assembly).AddNewtonsoftJson();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigureSqlContext(builder.Configuration);
builder.Services.AddConfigureRepositoryManager();
builder.Services.AddConfigureServiceManager();
builder.Services.AddConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddConfigureActionFilters();

var app = builder.Build();
//ihtiyac duyulan servis bu sekilde alýnýyor
var logger=app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
