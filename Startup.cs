using Biblioteca.Controllers;
using Biblioteca.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public IConfiguration Configuration { get; }

    // Construtor que injeta o IConfiguration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configurar a conexão com o banco de dados
        services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));        
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"String de Conexão: {connectionString}");

        // Adicionar suporte ao MVC
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Livro}/{action=Index}/{id?}");
        });
    }
}


