using TMPro;
using UnityEngine;

namespace PickAColor
{
    public class PlayerHintUI
    {
        private TMP_Text _label;
        public void Init(TMP_Text label, GameFieldModel gameFieldModel)
        {
            _label = label;
            gameFieldModel.RightColorChange += SetColor;
        }

        private void SetColor(Color color)
        {
            _label.text = $"You need to click on <color=#{color.ToRGBHex()}>{color.ToRGBHex()}</color> quad";
        }
    }
}