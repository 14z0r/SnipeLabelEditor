using Blazored.Modal;
using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using SnipeLabelEditor.Data;

namespace SnipeLabelEditor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLocalization();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredModal();

            builder.Services.AddDbContextFactory<LabelsDBContext>(opt =>
                    opt.UseSqlite($"Data Source={Path.Combine(Environment.CurrentDirectory, "labels.db")}"));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            
            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<LabelsDBContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var localizeOptions = new RequestLocalizationOptions()
                .SetDefaultCulture("en-EN")
                .AddSupportedCultures("en-EN", "de-DE")
                .AddSupportedUICultures("en-EN", "de-DE");

            app.UseRequestLocalization(localizeOptions);

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}