using Microsoft.EntityFrameworkCore;
using News.Contracts;
using News.Crawler;
using News.DAL;
using News.Services;
using System.Timers;
using Timer = System.Timers.Timer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<NewsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddSingleton<CrawlerService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var crawlerService = app.Services.GetRequiredService<CrawlerService>();

Timer timer = new Timer();
timer.Interval = 3600000;        
timer.Elapsed += OnTimerTick;
timer.Start();
 void OnTimerTick(Object source, ElapsedEventArgs e)
 {
   crawlerService.LoadAndPersistItems();
 }

   

app.Run();
