using System.Collections.Generic;
using UnityEngine;

namespace PickAColor
{
    [CreateAssetMenu]
    public class GameRoundSettings : ScriptableObject
    {
        public List<Color> AllColors;
        public List<Vector2> QuadPositions;
    }
}