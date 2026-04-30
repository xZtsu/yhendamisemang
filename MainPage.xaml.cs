using yhendamisemang.Models;

namespace yhendamisemang;

public partial class MainPage : ContentPage
{
    private Game _game;
    private List<Theme> _themes;
    private int _currentPairs = 6; 

    public MainPage()
    {
        InitializeComponent();
        _themes = new List<Theme>
        {
            new Theme("Hele", Colors.White, Colors.Black),
            new Theme("Tume", Colors.Black, Colors.White),
            new Theme("Värviline", Colors.LightPink, Colors.Purple)
        };

        _game = new Game(new Player("Mängija 1"));
        StartNewGame(null, null);
    }

    private void StartNewGame(object sender, EventArgs e)
    {
        // Puhastame
        GameGrid.Children.Clear();

        if (_game.CurrentPlayer != null)
        {
            _game.CurrentPlayer.Score = 0;
        }


        // Kasutame valitud paaride arvu
        var cards = _game.InitializeGame(_currentPairs);

        foreach (var card in cards)
        {
            // Eemaldame vana sündmuse
            card.Clicked -= OnCardClicked;
            card.Clicked += OnCardClicked;

            GameGrid.Children.Add(card);
        }
        UpdateUI();
    }

    private async void OnCardClicked(object sender, EventArgs e)
    {
        var card = (Card)sender;
        bool isGameOver = await _game.SelectCard(card);
        UpdateUI();

        if (isGameOver)
        {
            await DisplayAlert("Mäng läbi!", $"Sinu skoor: {_game.CurrentPlayer.Score}", "OK");
            Preferences.Set("HighScore", _game.CurrentPlayer.Score);
        }
    }

    private void UpdateUI()
    {
        ScoreLabel.Text = $"Punktid: {_game.CurrentPlayer.Score}";
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1 && _themes != null)
        {
            var selectedTheme = _themes[selectedIndex];

            // Rakendame teema värvid
            this.BackgroundColor = selectedTheme.BackgroundColor;
            ScoreLabel.TextColor = selectedTheme.TextColor;

            // Apply theme
            selectedTheme.Apply(this);
        }
    }
    private void OnCardAmountChanged(object sender, ValueChangedEventArgs e)
    {
        _currentPairs = (int)e.NewValue;
        StartNewGame(null, null); // Alustab automaatselt uut mängu 
    }
    private void OnCardAmountReset(object sender, EventArgs e)
    { 
        _currentPairs = 6; 
        AmountStepper.Value = 6;
        StartNewGame(null, null);
    }


}