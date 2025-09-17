var builder = WebApplication.CreateBuilder(args);
var profilesUrl = builder.Configuration["Services:Profiles"] ?? "http://profiles_service:5209";
var searchUrl   = builder.Configuration["Services:Search"]   ?? "http://search_service:8005";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobsService, JobsService.Service.JobsService>();

builder.Services.AddHttpClient<ISearchClient, SearchClient>(c =>
{
    c.BaseAddress = new Uri(searchUrl);
});

builder.Services.AddHttpClient<IProfilesClient, ProfilesClient>(c =>
{
    c.BaseAddress = new Uri(profilesUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// слушаем на всех интерфейсах
app.Urls.Add("http://*:5000");

app.Run();
