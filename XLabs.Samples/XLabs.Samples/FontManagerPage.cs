using System;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XLabs.Samples
{
    public class FontManagerPage : TabbedPage
    {
        private double FontSize = 0.25;

       

    
        public FontManagerPage()
        {

            IDisplay display = Resolver.Resolve<IDisplay>();
            IFontManager fontManager = Resolver.Resolve<IFontManager>();


            if (Device.Idiom == TargetIdiom.Phone)
            {
                FontSize = 0.30;
            }
            else if(Device.Idiom == TargetIdiom.Tablet)
            {
                FontSize = 0.50;
            }


            var stack = new StackLayout();

            foreach (var namedSize in Enum.GetValues(typeof(NamedSize)))
            {
                var font = Font.SystemFontOfSize((NamedSize)namedSize);

                var height = fontManager.GetHeight(font);

                var heightRequest = display.HeightRequestInInches(height);

                var label = new Label()
                {
                    Font = font,
                    HeightRequest = heightRequest + 10,
                    Text = string.Format("System font {0} is {1:0.000}in tall.", namedSize, height),
                    XAlign = TextAlignment.Center
                };

                stack.Children.Add(label);
            }

            var f = Font.SystemFontOfSize(24);

            var inchFont = fontManager.FindClosest(f.FontFamily, FontSize);

            stack.Children.Add(new Label()
            {
                Text = "The below text should be " + FontSize + "in height from its highest point to lowest.",
                XAlign = TextAlignment.Center
            });


            stack.Children.Add(new Label()
            {
                Text = "FfTtLlGgJjPp",
                TextColor = Color.Lime,
                FontSize = inchFont.FontSize,
                //                BackgroundColor = Color.Gray,
                //                FontFamily = inchFont.FontFamily,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Start
            });


            stack.Children.Add(new Label()
            {
                Text = FontSize + "in height = SystemFontOfSize(" + inchFont.FontSize + ")",
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End
            });

            this.Children.Add(new ContentPage() { Title = "Sizes", Content = stack });

            var listView = new ListView
            {
                ItemsSource = fontManager.AvailableFonts,
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label();
                    label.SetBinding(Label.TextProperty, ".");
                    label.SetBinding(Label.FontFamilyProperty, ".");
                    return new ViewCell { View = label };
                })
            };

            this.Children.Add(new ContentPage { Title = "Fonts", Content = listView });
        }
    }
}