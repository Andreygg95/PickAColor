using UnityEngine;

namespace PickAColor
{
    public class InputValidator
    {
        private GameFieldModel _gameFieldModel;
        private IGameRoundManager _roundManager;

        public void Init(IGameRoundManager roundManager, GameFieldModel gameFieldModel)
        {
            _gameFieldModel = gameFieldModel;
            _roundManager = roundManager;

            _roundManager.RoundStarted += Subscribe;
            _roundManager.RoundEnded += Unsubscribe;
        }

        private void Subscribe()
        {
            foreach (var quad in _gameFieldModel.Quads)
            {
                quad.Clicked += Validate;
            }
        }

        private void Unsubscribe()
        {
            foreach (var quad in _gameFieldModel.Quads)
            {
                quad.Clicked -= Validate;
            }
        }

        private void Validate(ClickableColoredQuad quad, Color color)
        {
            if (color == _gameFieldModel.RightColor)
            {
                _roundManager.EndRound();
            }
            else
            {
                quad.Blink();
            }
        }
    }
}