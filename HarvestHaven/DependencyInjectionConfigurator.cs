﻿using HarvestHaven.Repositories;
using HarvestHaven.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public static class DependencyInjectionConfigurator
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceProvider Init()
        {
            var serviceProvider = new ServiceCollection()
                .ConfigureServices()
                .ConfigureCodeBehinds()
                .BuildServiceProvider();
            ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }

    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IAchievementService, AchievementService>();
            services.AddSingleton<IMarketService, MarketService>();
            services.AddSingleton<IFarmService, FarmService>();
            services.AddSingleton<ITradeService, TradeService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IResourceService, ResourceService>();
            services.AddSingleton<IMainMenuService, MainMenuService>();
            services.AddSingleton<IInventoryService, InventoryService>();

            services.AddTransient<IAchievementRepository, AchievementRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IFarmCellRepository, FarmCellRepository>();
            services.AddTransient<IInventoryResourceRepository, InventoryResourceRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IMarketBuyItemRepository, MarketBuyItemRepository>();
            services.AddTransient<IMarketSellResourceRepository, MarketSellResourceRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<ITradeRepository, TradeRepository>();
            services.AddTransient<IUserAchievementRepository, UserAchievementRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection ConfigureCodeBehinds(this IServiceCollection services)
        {
            return services;
        }
    }
}
