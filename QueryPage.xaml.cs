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


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TechnoSphere_2k15
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
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
