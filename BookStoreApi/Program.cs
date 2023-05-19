
using BookStoreApi.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
//applicationpart ile reflection konuldu presentation katmanýný da assemnlyrefence sýnýfý sayesinde çözümleyebiliyor oradaki controllerda kullanýlabiliyor bu sayede
builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyRefence).Assembly).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigureSqlContext(builder.Configuration);
builder.Services.AddConfigureRepositoryManager();
builder.Services.AddConfigureServiceManager();
builder.Services.AddConfigureLoggerService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
