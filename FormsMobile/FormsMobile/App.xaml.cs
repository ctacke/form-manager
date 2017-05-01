using FormsMobile.Services;
using FormsMobile.ViewModels;
using FormsMobile.Views;
using OpenNETCF;
using OpenNETCF.IoC;
using OpenNETCF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsMobile
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                InitializeServices();
                RegisterViews();

                // register the Home and Login views and show the Login
                // if we add a "remember me" feature, this logic will change slightly
                NavigationService.SetHomeView<HomeView>(true, false);
                NavigationService.SetLoginView<LoginView>(true);
            }
            catch (Exception ex)
            {
                // this is here to allow us to set a breakpoint to analyze problems
                throw ex;
            }
        }

        private void InitializeServices()
        {
            RootWorkItem.Services.AddNew<FormService, IFormService>();
        }

        private void RegisterViews()
        {
            NavigationService.Register<LoginView, LoginViewModel>();
            NavigationService.Register<HomeView, HomeViewModel>();
            NavigationService.Register<FormEntryView, FormEntryViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
