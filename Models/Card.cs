using System;
using System.Collections.Generic;
using System.Text;

namespace yhendamisemang.Models;

public class Card : Button
{
    public string Value { get; set; }
    public bool IsMatched { get; set; } = false;
    public bool IsFaceUp { get; set; } = false;

    public Card(string value)
    {
        Value = value;
        Text = "?"; // Alguses on väärtus peidus
        FontSize = 24;
        HeightRequest = 80;
        WidthRequest = 80;
        Margin = 5;
    }

    public void Reveal()
    {
        Text = Value;
        IsFaceUp = true;
    }

    public void Hide()
    {
        Text = "?";
        IsFaceUp = false;
    }
}
