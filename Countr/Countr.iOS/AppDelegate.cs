using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;

namespace Countr.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if DEBUG
            Xamarin.Calabash.Start();
#endif

            UINavigationBar.Appearance.BarTintColor = UIColor.Orange;
            UINavigationBar.Appearance.TintColor = UIColor.DarkGray;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes(new NSDictionary(UIStringAttributeKey.ForegroundColor,
                                                                                                     UIColor.DarkGray));
            
            AppCenter.Start("<your app secret>",
                            typeof(Analytics),
                            typeof(Crashes));
            
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
