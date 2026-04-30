using System;
using System.Collections.Generic;
using System.Text;

namespace yhendamisemang.Models
{
    public class Theme
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public string FontFamily { get; set; }

        public Theme(string name, Color bg, Color text, string font = "OpenSansRegular")
        {
            Name = name;
            BackgroundColor = bg;
            TextColor = text;
            FontFamily = font;
        }

        public void Apply(ContentPage page)
        {
            page.BackgroundColor = BackgroundColor;
            // Siia saab lisada loogika, mis muudab globaalseid ressursse või konkreetseid elemente
        }
    }
}
