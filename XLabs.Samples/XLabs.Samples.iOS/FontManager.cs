using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Platform.Device;

namespace XLabs.Samples.iOS
{
    public partial class FontManager : IFontManager
    {
        /// <summary>
        /// The _display
        /// </summary>
        private readonly IDisplay display;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontManager"/> class.
        /// </summary>
        /// <param name="display">The display.</param>
        public FontManager(IDisplay display)
        {
            this.display = display;
        }

        #region IFontManager Members

        /// <summary>
        /// Gets all available system fonts.
        /// </summary>
        /// <value>The available fonts.</value>
        public IEnumerable<string> AvailableFonts
        {
            get
            {
                return UIFont.FamilyNames;
            }
        }

        private const short InitialSize = 24;

        /// <summary>
        /// Finds the closest.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="desiredHeight">Height of the desired.</param>
        /// <returns>Font.</returns>
        public Font FindClosest(string name, double desiredHeight)
        {
            var height = this.GetHeight(Font.OfSize(name, InitialSize));

            var multiply = (int)((desiredHeight / height) * InitialSize);


            var f1 = Font.OfSize(name, multiply);
            var f2 = Font.OfSize(name, multiply + 1);

            var h1 = this.GetHeight(f1);
            var h2 = this.GetHeight(f2);

            var d1 = Math.Abs(h1 - desiredHeight);
            var d2 = Math.Abs(h2 - desiredHeight);

            return d1 < d2 ? f1 : f2;
        }

        /// <summary>
        /// Gets height for the font.
        /// </summary>
        /// <param name="font">Font whose height is calculated.</param>
        /// <returns>Height of the font in inches.</returns>
        public double GetHeight(Font font)
        {
            return font.ToUIFont().Ascender
                * UIScreen.MainScreen.Scale
                * this.display.ScreenHeightInches()
                / this.display.Height;
        }

        #endregion
    }
}