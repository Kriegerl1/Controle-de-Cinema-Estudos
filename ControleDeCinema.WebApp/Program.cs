using ControleDeCinema.Aplica��o;
using ControleDeCinema.Dom�nio;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.M�dulo_Categoria;
using ControleDeCinema.Infra.M�dulo_Filme;
using ControleDeCinema.Infra.M�dulo_Sala;
using ControleDeCinema.WebApp.Mapping.Resolvers;
using System.Reflection;

namespace ControleDeCinema.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Inje�ao de Depend�ncias
        builder.Services.AddDbContext<CinemaDbContext>();

        builder.Services.AddScoped<IRepositorioSala, RepositorioDeSalaOrm>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        builder.Services.AddScoped<IRepositorioFilme, RepositorioFilmeEmOrm>();

        builder.Services.AddScoped<SalaResolver>();
        builder.Services.AddScoped<CategoriaResolver>();
        builder.Services.AddScoped<FilmeResolver>();

        builder.Services.AddScoped<SalaService>();
        builder.Services.AddScoped<CategoriaService>();
        builder.Services.AddScoped<FilmeService>();

        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(Assembly.GetExecutingAssembly());
        });

        #endregion

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
