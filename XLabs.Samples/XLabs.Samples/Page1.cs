using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XLabs.Samples
{
    public class Page1 : ContentPage
    {
        Entry test, txtinc;
        Label resultlbl;
        Label resultlblipad;
        Button button;
        Label resulTextiphone;
        Label resulTextiPad;
        public Page1()
        {

            test = new Entry()
            { WidthRequest = 150, BackgroundColor = Color.FromHex("dddddd"),Keyboard = Keyboard.Numeric };
            txtinc = new Entry()
            { WidthRequest = 150, BackgroundColor = Color.FromHex("dddddd"), Keyboard = Keyboard.Numeric };
            button = new Button()
            {
                Text = "Calculate",
                TextColor =  Color.Black,
                BackgroundColor = Color.FromHex("4286f4"),
               WidthRequest=  150,HeightRequest=200
            };
            button.Clicked += Button_Clicked;
            resultlbl = new Label();
            resultlblipad = new Label();
            resulTextiphone = new Label()
            {
                Text = "Font Size iPhone. ABCD abcd",
                Margin = new Thickness(20)
            };
            resulTextiPad = new Label()
            {
                Text = "Font Size iPad. ABCD abcd",
                Margin = new Thickness(20)
            };
            BoxView SeperatorCmnt = new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 1,
                BackgroundColor = Color.Black,
                Margin = new Thickness(20)
            };
            StackLayout stacklbl = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
            };
            stacklbl.Children.Add(test);
            stacklbl.Children.Add(txtinc);

            StackLayout stackbtn = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 20,
                Padding = new Thickness(20),
            };
            stackbtn.Children.Add(button);
            stackbtn.Children.Add(resultlbl);
            stackbtn.Children.Add(resultlblipad);

            StackLayout stackres = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
            };
            stackres.Children.Add(resulTextiphone);
            stackres.Children.Add(SeperatorCmnt);
            stackres.Children.Add(resulTextiPad);

            StackLayout mainstack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { stacklbl, stackbtn, stackres }
            };


            Content = mainstack;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Double data = Convert.ToDouble(test.Text);
            resultlbl.Text = Convert.ToString(font_size(data));
            resulTextiphone.FontSize = font_size(data);

            if (!string.IsNullOrEmpty(txtinc.Text))
            {
                Double inc = Convert.ToDouble(txtinc.Text);
                Double incsiz = data + inc;
                resultlblipad.Text = Convert.ToString(font_size(incsiz));

                resulTextiPad.FontSize = font_size(incsiz);
            }


        }

        public static Double font_size(double FontSize)
        {
            // Double inc = FontSize * 0.010416667;
            //if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            //{
            //    FontSize = FontSize + IWMSEnums.FontSize_Tablet.FONTSIZE_TABLET;
            //}
            var f = Font.SystemFontOfSize(24);

            var inchFont = App.fontManager.FindClosest(f.FontFamily, FontSize);
            return inchFont.FontSize;
        }

    }
}
