using System;
using UnityEngine;

namespace PickAColor
{
    //Only created interface for class which can have different realisations
    public interface IGameRoundManager
    {
        event Action RoundStarted;
        event Action RoundEnded;
        void Init(RectTransform gameField, GameModel gameModel, GameFieldModel gameFieldModel, ExamplesContainer examplesContainer);
        void StartRound();
        void EndRound();
    }
}