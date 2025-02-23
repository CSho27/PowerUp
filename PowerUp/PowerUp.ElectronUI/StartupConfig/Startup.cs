using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PowerUp.Databases;
using PowerUp.ElectronUI.StartupConfig;
using PowerUp.Libraries;
using Serilog;

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

      var dataDirectory = Configuration["DataDirectory"] ?? "";
      Log.Information($"Data Directory: {Path.GetFullPath(dataDirectory)}");

      var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog());
      Logging.Initialize(loggerFactory.CreateLogger("Static"));
      DatabaseConfig.Initialize(loggerFactory.CreateLogger<EntityDatabase>(), dataDirectory);

      Log.Debug("RegisteringLibraries");
      services.RegisterLibraries(dataDirectory);
      Log.Debug("RegisteringDependencies");
      services.RegisterDependencies();
      Log.Debug("ConfigureServices finished successfully");
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      Log.Debug($"Entered {nameof(Configure)}");
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

      Log.Debug("HttpsRedirection beginning...");
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Electron}/{action=Index}"
        );
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=GameSaveImport}/{action=Import}"
        );
        endpoints.MapControllers();
      });

      DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml, ContractResolver = contractResolver };

      var serviceProvider = app.ApplicationServices
        .CreateScope()
        .ServiceProvider;

      serviceProvider.AddCommandsToRegistry();
    }
  }
}