using UnityEngine;
using UnityEngine.UI;

namespace SpaceShootuh.UI.GameHUD
{
    public class GameHUDView : BaseView, IGameHUDView
    {
        [SerializeField]
        private Image hpProgressBar;
        [SerializeField]
        public Text score;

        public void SetHP(float value)
        {
            hpProgressBar.fillAmount = value;
        }

        public void SetScore(int value)
        {
            score.text = value.ToString();
        }
    }
}