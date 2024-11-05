using System.Linq;
using UnityEngine.UI;

namespace PickAColor
{
    //Inheritance to reduce copy-paste.
    public class GameRoundManagerLevel2 : GameRoundManagerLevel1
    {
        public GameRoundManagerLevel2(GameRoundSettings settings) : base(settings) { }

        public override void StartRound()
        {
            base.StartRound();

            _gameFieldModel.Background.GetComponent<Image>().color = _allColors.First(x => _gameFieldModel.Quads.All(y => y.MyColor != x));
        }
    }
}