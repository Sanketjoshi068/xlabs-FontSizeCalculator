
using Foundation;
using UIKit;
using XLabs.Platform.Device;

namespace XLabs.Samples.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {   SetIoc();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

           
              
            return base.FinishedLaunching(app, options);
        }

        private void SetIoc()
        {
            var resolverContainer = new global::XLabs.Ioc.SimpleContainer();


            //var app = new XFormsAppiOS();
           // app.Init(this,true);


          //  var documents = app.AppDataDirectory;
           // var pathToDatabase = Path.Combine(documents, "xforms.db");

            resolverContainer.Register<IDevice>(t => AppleDevice.CurrentDevice)
                .Register<IDisplay>(t => t.Resolve<IDevice>().Display)
                .Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()));
               

            XLabs.Ioc.Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}
