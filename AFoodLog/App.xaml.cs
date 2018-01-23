﻿using System;

using Xamarin.Forms;

namespace AFoodLog
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();

            /*if (Device.RuntimePlatform == Device.iOS)
                MainPage = new AFoodLog.iOS.Views
            else
                MainPage = new NavigationPage(new MainPage());*/
        }
    }
}