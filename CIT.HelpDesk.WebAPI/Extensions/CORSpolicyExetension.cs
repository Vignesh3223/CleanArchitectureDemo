namespace CIT.HelpDesk.WebAPI.Extensions
{
    public static class CORSpolicyExetension
    {
        public static void AddCORSPolicy(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAccess",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "").AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }
    }
}
