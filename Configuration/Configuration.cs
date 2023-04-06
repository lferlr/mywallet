using MyWallet.Services;

namespace MyWallet.Configuration;

public static class Configuration
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IExpenseService, ExpenseService>();

        return services;
    }
}