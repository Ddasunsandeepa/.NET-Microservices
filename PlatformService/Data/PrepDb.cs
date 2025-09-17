namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder app, bool isProd)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope()){
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        if (context != null)
        {
          SeedData(context, isProd);
        }
        else
        {
          throw new InvalidOperationException("AppDbContext service is not registered.");
        }
      }
    }

    private static void SeedData(AppDbContext context, bool isProd)
    {

    }
  }
}