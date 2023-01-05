using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ServicesRegistrationDelegate
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILandlordService, LandlordService>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<IApartmentFeatureService, ApartmentFeatureService>();
        }
    }
}