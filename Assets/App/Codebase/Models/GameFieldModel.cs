using System;
using System.Collections.Generic;
using UnityEngine;

namespace PickAColor
{
    public class GameFieldModel
    {
        public event Action<Color> RightColorChange;
        public Transform Background;
        public List<ClickableColoredQuad> Quads = new ();
        private Color _rightColor;
        public Color RightColor
        {
            get => _rightColor;
            set
            {
                _rightColor = value;
                RightColorChange?.Invoke(value);
            }
        }
    }
}