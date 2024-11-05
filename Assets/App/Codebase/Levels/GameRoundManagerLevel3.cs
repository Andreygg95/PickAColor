using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace PickAColor
{
    public class GameRoundManagerLevel3 : IGameRoundManager
    {
        public event Action RoundStarted;
        public event Action RoundEnded;
        
        private List<Vector2> _quadPositions;

        private GameFieldModel _gameFieldModel;
        private RectTransform _gameField;
        private GameModel _gameModel;
        private ExamplesContainer _examplesContainer;

        public GameRoundManagerLevel3(GameRoundSettings settings)
        {
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
            _gameFieldModel.Background.GetComponent<Image>().color = RandomDistancedColor(pickedColors, .7f, 1f);
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
            List<Color> pickedColors = new List<Color>(_quadPositions.Count);

            pickedColors.Clear();
            for (int i = 0; i < _quadPositions.Count; i++)
            {
                pickedColors.Add(RandomDistancedColor(pickedColors, .5f, .7f));
            }

            return pickedColors;
        }

        protected void SpawnQuads(List<Color> pickedColors, List<Vector2> quadPositions)
        {
            for (var i = 0; i < pickedColors.Count; i++)
            {
                var color = pickedColors[i];
                var quad = Object.Instantiate(_examplesContainer.ExampleQuad, _gameFieldModel.Background);
                quad.Init(color, quadPositions[i]);
                _gameFieldModel.Quads.Add(quad);
            }
        }

        private Color RandomDistancedColor(List<Color> pickedColors, float distance, float a)
        {
            Color c = RandomColor(a);
            while (pickedColors.Count>0 && pickedColors.Any(x => ColorDistance(x, c) < distance))
            {
                c = RandomColor(a);
            }

            return c;
        }

        private Color RandomColor(float a) => new Color(Random.value, Random.value, Random.value, a);

        private float ColorDistance(Color a, Color b)
        {
            var r = Mathf.Abs(b.r - a.r);
            var g = Mathf.Abs(b.g - a.g);
            var bl = Mathf.Abs(b.b - a.b);
            
            return r + g + bl;
        }
    }
}