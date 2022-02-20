using ElectronNET.API;
using ElectronNET.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PowerUp.Databases;
using PowerUp.ElectronUI.StartupConfig;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI
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
      services.AddControllersWithViews();
      services.RegisterCommandsForDI();

      var dataDirectory = Configuration["DataDirectory"];
      DatabaseConfig.Initialize(dataDirectory);
      services.RegisterLibraries(dataDirectory);
      services.RegisterDependencies();
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
        // TODO: Create Better Error Page
        app.UseDeveloperExceptionPage();
        //app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Electron}/{action=Index}");
      });

      DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml, ContractResolver = contractResolver };


      if (HybridSupport.IsElectronActive)
        ElectronBootstrap();

      app.ApplicationServices
        .CreateScope()
        .ServiceProvider
        .AddCommandsToRegistry();
    }

    public async void ElectronBootstrap()
    {
      var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
      {
        Width = 1152,
        Height = 940,
        Show = false,
      });

      browserWindow.Maximize();

      await browserWindow.WebContents.Session.ClearCacheAsync();

      browserWindow.OnReadyToShow += () => browserWindow.Show();
      browserWindow.SetTitle("PowerUp");
    }
  }
}