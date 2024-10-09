using DemoPRN1.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<PJPRN221Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Books/index");
                await Task.CompletedTask;
            });
            app.MapRazorPages();

            app.Run();
        }
    }
}