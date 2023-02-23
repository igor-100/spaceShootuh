using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SpaceShootuh.UI.Pause
{
    public class PauseScreenView : BaseView, IPauseScreenView
    {
        public event Action ResumeClicked = () => { };
        public event Action RestartClicked = () => { };

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeClicked);
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        public void OnResumeClicked()
        {
            ResumeClicked();
        }

        public void OnRestartClicked()
        {
            RestartClicked();
        }
    }
}
