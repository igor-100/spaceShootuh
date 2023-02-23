using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShootuh.UI.GameOver
{
    public class GameOverView : BaseView, IGameOverView
    {
        public event Action RestartClicked = () => { };

        [SerializeField] 
        private Button restartButton;
        [SerializeField]
        public Text score;

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }
        public void OnRestartClicked()
        {
            RestartClicked();
        }

        public void SetScore(int value)
        {
            score.text = value.ToString();
        }
    }
}
