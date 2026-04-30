using System;
using System.Collections.Generic;
using System.Text;

namespace yhendamisemang.Models
{
    public class Player
    {
        public string Name { get; private set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}
