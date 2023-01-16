using Microsoft.EntityFrameworkCore;
using News.Contracts;
using News.DAL;
using News.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContextFactory<NewsContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("News.Migrations")
            );
    });
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:4200");
                          });
    });
    builder.Services.AddControllers();
    builder.Services.AddScoped<IDataService, DataService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();
    app.UseCors(MyAllowSpecificOrigins);

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<NewsContext>();
        //if(DownMigartion)
        context.Database.Migrate();
    }

    app.Run();

}
catch(Exception e)
{

}