using Parse;
using VetMapp.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VetMapp
{
    sealed partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = "VHGWqcnFaJtSbzYEU0RoZd7vrksPKB3NQQNQlfrU",
                WindowsKey = "smzwxPjGFImASW5hYhkZKup4mfsPaNNrPrQXQA3U",
                Server = "https://pg-app-j8e1p7yakmanwjkdb8d7svie9iv7x3.scalabl.cloud/1/"
            });

            ParseFacebookUtils.Initialize("518811151618797");
        }


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {

                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if(ParseUser.CurrentUser == null)
                    {
                        rootFrame.Navigate(typeof(StartView));
                    }

                    else
                    {
                        rootFrame.Navigate(typeof(MainPage));
                    }
                }

                Window.Current.Activate();
            }
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}