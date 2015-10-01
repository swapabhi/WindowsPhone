using TechnoSphere_2k15.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Net.NetworkInformation;
using Windows.UI;

namespace TechnoSphere_2k15
{
    public sealed partial class QueryPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public QueryPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Colors.Black;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        private void CommandInvokedHandler1(IUICommand command)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                // e.Handled = true;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            email.Text = "";
            college.Text = "";
            message.Text = "";
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            string actionPath = "https://docs.google.com/forms/d/1Ec06DCgcY2MdDQGdnb2VgGUgz15qsnY01sgVQhhEwfM/formResponse?entry.1855539786="+name.Text+"&entry.1353202612="+college.Text+"&entry.600594034="+email.Text+"&entry.1470053554="+message.Text+"&submit=Submit";
            if (name.Text != "" && email.Text != "" && college.Text != "" && message.Text != "")
            {
                if(NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    webview.Navigate(new Uri(actionPath));
                    progressring.IsActive = true;
                    progressring.Visibility = Visibility.Visible;
                    name.IsEnabled = email.IsEnabled = college.IsEnabled = message.IsEnabled = accept.IsEnabled = reset.IsEnabled = false;
                }
                else
                {
                    var messageDialogue = new Windows.UI.Popups.MessageDialog("No network Available. Please check your network connectivity and try again.");
                    messageDialogue.Commands.Add(new UICommand("close", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                    messageDialogue.DefaultCommandIndex = 0;
                    messageDialogue.CancelCommandIndex = 1;
                    await messageDialogue.ShowAsync();
                }

            }

            else 
            {
                var messageDialogue = new Windows.UI.Popups.MessageDialog("Oops! All fields are mandatory. Please fill all the fields.");
                messageDialogue.Commands.Add(new UICommand("Close", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialogue.DefaultCommandIndex = 0;
                messageDialogue.CancelCommandIndex = 1;
                await messageDialogue.ShowAsync();
            }

        }

        private void CommandInvokedHandler(IUICommand command)
        {
            new Exception();
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
               return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
               // e.Handled = true;
            }
        }

        private async void webview_LoadCompleted(object sender, NavigationEventArgs e)
        {
            progressring.IsActive = false;
            progressring.Visibility = Visibility.Collapsed;
            var messageDialogue = new Windows.UI.Popups.MessageDialog("Thank you. Your response has been recorded successfully.");
            messageDialogue.Commands.Add(new UICommand("Close", new UICommandInvokedHandler(this.CommandInvokedHandler1)));
            messageDialogue.DefaultCommandIndex = 0;
            messageDialogue.CancelCommandIndex = 1;
            await messageDialogue.ShowAsync();
            name.IsEnabled = email.IsEnabled = college.IsEnabled = message.IsEnabled = accept.IsEnabled = reset.IsEnabled = true;
            name.Text = "";
            email.Text = "";
            college.Text = "";
            message.Text = "";
        }
    }
}
