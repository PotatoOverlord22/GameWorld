﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using GameWorld.Views;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using GameWorldClassLibrary.Services;

namespace GameWorld
{
    public partial class App : Application
    {
        private readonly IUserService userService;
        public App()
        {
            DependencyInjectionConfigurator.Init();
            userService = DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>();
            SetCurrentUser().Wait();
            /*MainMenu mainMenu = new MainMenu();
            mainMenu.Show();*/
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private async Task SetCurrentUser()
        {
            User user = new User(new Guid(), "test");
            try
            {
                user = await userService.GetUserByUsername("TestUser");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            GameStateManager.SetCurrentUser(user);
        }
    }
}
