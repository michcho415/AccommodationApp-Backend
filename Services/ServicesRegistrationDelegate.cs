using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace Services
{
    public static class ServicesRegistrationDelegate
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBookingService, BookingService>();
        }
    }
}