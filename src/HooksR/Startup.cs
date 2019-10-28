using System;
using AutoMapper;
using HooksR.Hubs;
using HooksR.Options;
using HooksR.Service;
using HooksR.Service.Contracts;
using HooksR.Session.Concrete;
using HooksR.Session.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HooksR.Options.Utils;

namespace HooksR
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      IConfigurationSection redisSection = Configuration.GetSection("Redis");
      var redisOptions = redisSection.Get<Redis>();

      services.AddDistributedRedisCache(options => {
        options.Configuration = redisOptions.Configuration;
        options.InstanceName = redisOptions.InstanceName;
      });

      services.AddSession(options => {
        options.Cookie.Name = "HooksR";
        options.IdleTimeout = TimeSpan.FromDays(30);
        options.Cookie.Expiration = TimeSpan.FromDays(30);
      });

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddScoped<IHooksRSession, HooksRSession>();

      services.AddScoped<IHubService, HooksRHubService>();

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

      services.AddSignalR();
      services.AddControllersWithViews();

      services.AddMvc()
        .AddJsonOptions(options => {
          options.JsonSerializerOptions.Converters.Add(new HttpHeaderConverter());
        });

      services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(),
                               AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.Use(async (context, next) =>
      {
        context.Items.Add(Consts.RequestStartedOn, DateTime.UtcNow);
        await next();
      });

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      var cookiePolicyOptions = new CookiePolicyOptions
      {
        MinimumSameSitePolicy = SameSiteMode.Strict,
      };
      app.UseCookiePolicy(cookiePolicyOptions);

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapHub<HookRHub>("/hookhub");
      });
    }
  }
}
