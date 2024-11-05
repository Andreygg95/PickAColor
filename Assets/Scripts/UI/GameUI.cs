using UnityEngine;
using UnityEngine.UI;

namespace PickAColor
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _startStateParent;
        [SerializeField] private GameObject _inGameStateParent;
        [SerializeField] private Button[] _initButtons;
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private GameInitializer _gameInitializer;

        public void Awake()
        {
            for (var i = 0; i < _initButtons.Length; i++)
            {
                var level = i;
                _initButtons[i].onClick.AddListener(() => StartGame(level));
            }

            _restartButton.onClick.AddListener(Restart);
        }

        public void OnDestroy()
        {
            foreach (var button in _initButtons)
            {
                button.onClick.RemoveAllListeners();
            }

            _restartButton.onClick.RemoveListener(Restart);
        }

        private void StartGame(int level)
        {
            SetButtons(true);
            _gameInitializer.StartGame(level);
        }

        private void Restart()
        {
            SetButtons(false);
            _gameInitializer.Restart();
        }

        private void SetButtons(bool isGameStarted)
        {
            _inGameStateParent.SetActive(isGameStarted);
            _startStateParent.SetActive(!isGameStarted);
        }
    }
}