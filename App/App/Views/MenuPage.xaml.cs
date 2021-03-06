﻿using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            string loggedUserRole = (string) Application.Current.Properties["userRole"];

            if(loggedUserRole == "CLIENT" || loggedUserRole == "EMPLOYEE") {
                menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Suplements, Title = "Suplementi"},
                    new HomeMenuItem {Id = MenuItemType.Workouts, Title = "Treninzi"},
                    new HomeMenuItem {Id = MenuItemType.Memberships, Title = "Clanarine"},
                    new HomeMenuItem {Id = MenuItemType.WorkoutAdvice, Title = "Savjeti"},
                    new HomeMenuItem {Id = MenuItemType.LogOut, Title = "Logout"}
                };
            }

            if(loggedUserRole == "TRAINER")
            {
                menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Suplements, Title = "Suplementi"},
                    new HomeMenuItem {Id = MenuItemType.Workouts, Title = "Treninzi"},
                    new HomeMenuItem {Id = MenuItemType.WorkoutAdviceTrainer, Title = "Savjeti"},
                    new HomeMenuItem {Id = MenuItemType.LogOut, Title = "Logout"}
                };
            }

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}