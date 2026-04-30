using System;
using System.Collections.Generic;
using System.Text;

namespace yhendamisemang.Models;

public class Game
{
    private List<Card> _cards;
    private Card _firstSelected;
    private Card _secondSelected;
    private int _matchesFound;

    public Player CurrentPlayer { get; private set; }
    public bool IsProcessing { get; private set; } = false;

    public Game(Player player)
    {
        CurrentPlayer = player;
        _cards = new List<Card>();
    }

    public List<Card> InitializeGame(int pairs)
    {
        _cards.Clear();
        _matchesFound = 0;
        List<string> values = new List<string> { "🍎", "🍌", "🍇", "🍓", "🍒", "🍍", "🥝", "🍋" };
        var gameValues = values.Take(pairs).ToList();
        gameValues.AddRange(gameValues); // Dubleerime paarideks

        // Segamine
        var shuffled = gameValues.OrderBy(x => Guid.NewGuid()).ToList();

        foreach (var val in shuffled)
        {
            _cards.Add(new Card(val));
        }
        return _cards;
    }

    public async Task<bool> SelectCard(Card selected)
    {
        if (IsProcessing || selected.IsMatched || selected == _firstSelected) return false;

        selected.Reveal();

        if (_firstSelected == null)
        {
            _firstSelected = selected;
            return false;
        }

        _secondSelected = selected;
        IsProcessing = true;

        if (_firstSelected.Value == _secondSelected.Value)
        {
            _firstSelected.IsMatched = _secondSelected.IsMatched = true;
            _firstSelected.BackgroundColor = Colors.Green;
            _secondSelected.BackgroundColor = Colors.Green;
            CurrentPlayer.Score += 10;
            _matchesFound++;
            ResetSelection();
            return _matchesFound == _cards.Count / 2; // Tagastab true, kui mäng läbi
        }
        else
        {
            await Task.Delay(1000);
            _firstSelected.Hide();
            _secondSelected.Hide();
            ResetSelection();
            return false;
        }
    }

    private void ResetSelection()
    {
        _firstSelected = null;
        _secondSelected = null;
        IsProcessing = false;
    }
}
