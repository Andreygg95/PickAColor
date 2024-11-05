using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace PickAColor
{
    public class GameRoundManagerLevel1 : IGameRoundManager
    {
        public event Action RoundStarted;
        public event Action RoundEnded;
        
        protected List<Color> _allColors;
        private List<Vector2> _quadPositions;
        private List<Color> _previousColors;

        protected GameFieldModel _gameFieldModel;
        private RectTransform _gameField;
        private GameModel _gameModel;
        private ExamplesContainer _examplesContainer;

        public GameRoundManagerLevel1(GameRoundSettings settings)
        {
            _allColors = settings.AllColors;
            _quadPositions = settings.QuadPositions;
        }
        
        public void Init(RectTransform gameField, GameModel gameModel, GameFieldModel gameFieldModel, ExamplesContainer examplesContainer)
        {
            _gameField = gameField;
            _gameModel = gameModel;
            _gameFieldModel = gameFieldModel;
            _examplesContainer = examplesContainer;
        }
        
        public virtual void StartRound()
        {
            _gameFieldModel.Background = Object.Instantiate(_examplesContainer.SimpleBackground, _gameField);
            _gameFieldModel.Quads.Clear();
            
            var pickedColors = PickColors();

            SpawnQuads(pickedColors, _quadPositions);

            _gameFieldModel.RightColor = pickedColors[Random.Range(0, pickedColors.Count)];

            _previousColors = pickedColors;
            RoundStarted?.Invoke();
        }

        public void EndRound()
        {
            _gameModel.Score++;
            RoundEnded?.Invoke();
            Object.Destroy(_gameFieldModel.Background.gameObject);
            StartRound();
        }

        private List<Color> PickColors()
        {
            var canUseColors = false;
            List<Color> pickedColors = new List<Color>(_quadPositions.Count);
            
            //dont like do while
            while (!canUseColors)
            {
                var availableColors = new List<Color>(_allColors);
                pickedColors.Clear();
                for (int i = 0; i < _quadPositions.Count; i++)
                {
                    var index = Random.Range(0, availableColors.Count);
                    pickedColors.Add(availableColors[index]);
                    availableColors.RemoveAt(index);
                }

                canUseColors = CanUseColors(pickedColors);
            }

            return pickedColors;
        }

        private void SpawnQuads(List<Color> pickedColors, List<Vector2> quadPositions)
        {
            for (var i = 0; i < pickedColors.Count; i++)
            {
                var color = pickedColors[i];
                var quad = Object.Instantiate(_examplesContainer.ExampleQuad, _gameFieldModel.Background);
                quad.Init(color, quadPositions[i]);
                _gameFieldModel.Quads.Add(quad);
            }
        }

        private bool CanUseColors(List<Color> colors)
        {
            if (_previousColors == null)
                return true;

            for (int i = 0; i < _previousColors.Count; i++)
            {
                if (_previousColors[i] != colors[i])
                    return true;
            }

            return false;
        }
    }
}