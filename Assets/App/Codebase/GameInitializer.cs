using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PickAColor
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private ExamplesContainer _examplesContainer;

        [SerializeField] private TMP_Text _hintLabel;
        [SerializeField] private TMP_Text _gameScoreLabel;
        
        [SerializeField] private RectTransform _gameField;
        
        [SerializeField] private List<GameRoundSettings> _gameSettings;

        public void StartGame(int level)
        {
            var settings = _gameSettings[level];
            
            switch (level)
            {
                case 0:
                    Init(new GameRoundManagerLevel1(settings));
                    break;
                
                case 1:
                    Init(new GameRoundManagerLevel2(settings));
                    break;
                
                case 2:
                    Init(new GameRoundManagerLevel3(settings));
                    break;
            }
        }

        public void Restart()
        {
            Destroy(_gameField.GetChild(0).gameObject);
        }

        private void Init(IGameRoundManager gameRoundManager)
        {
            var gameFieldModel = new GameFieldModel();
            var gameModel = new GameModel();
            
            var inputValidator = new InputValidator();
            var playerHintUI = new PlayerHintUI();
            var gameScoreUI = new GameScoreUI();

            inputValidator.Init(gameRoundManager, gameFieldModel);
            playerHintUI.Init(_hintLabel, gameFieldModel);
            gameScoreUI.Init(_gameScoreLabel, gameModel);
            gameRoundManager.Init(_gameField, gameModel, gameFieldModel, _examplesContainer);
            
            gameRoundManager.StartRound();
        }
    }
}