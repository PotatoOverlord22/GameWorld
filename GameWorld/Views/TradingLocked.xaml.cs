﻿using System.Windows;
using GameWorld;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public partial class TradingLocked : Window
    {
        private Farm farm;
        private readonly IUserService userService;

        public TradingLocked(Farm farm, IUserService userService)
        {
            this.farm = farm;
            this.userService = userService;
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            farm.Top = this.Top;
            farm.Left = this.Left;

            farm.Show();
            this.Close();
        }

        private async void Unlock_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await userService.UnlockTradeHall();

                TradingUnlocked tradingScreen = new TradingUnlocked(farm, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<ITradeService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IResourceService>());

                tradingScreen.Top = this.Top;
                tradingScreen.Left = this.Left;

                tradingScreen.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}