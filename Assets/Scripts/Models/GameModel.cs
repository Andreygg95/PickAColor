using System;

namespace PickAColor
{
    public class GameModel
    {
        public event Action<int> ScoreChanged;

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreChanged?.Invoke(_score);
            }
        }
    }
}