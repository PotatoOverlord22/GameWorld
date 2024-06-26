﻿using System.Windows;
using System.Windows.Controls;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;
namespace GameWorld.Views
{
    public partial class CommentScreen : Page
    {
        private VisitedFarm visitedFarm;
        private User user;
        private readonly IUserService userService;

        public CommentScreen(VisitedFarm visitedFarm, User user, IUserService userService)
        {
            this.visitedFarm = visitedFarm;
            this.user = user;
            this.userService = userService;

            InitializeComponent();
        }

        private void BackToVisitedFarm()
        {
            NavigationService.Navigate(visitedFarm);
        }

        private async void Button_Click_Send(object sender, RoutedEventArgs e)
        {
            try
            {
                await userService.AddCommentForAnotherUser(user, CommentTextBox.Text);
                BackToVisitedFarm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            BackToVisitedFarm();
        }
    }
}
