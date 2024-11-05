using TMPro;

namespace PickAColor
{
    public class GameScoreUI
    {
        private TMP_Text _label;
        public void Init(TMP_Text label, GameModel gameModel)
        {
            _label = label;
            gameModel.ScoreChanged += SetScore;
            SetScore(0);
        }

        private void SetScore(int score)
        {
            _label.text = $"Your score: {score}";
        }
    }
}