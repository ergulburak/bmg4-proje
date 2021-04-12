using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameSceneUIHandler : MonoBehaviour
    {
        public enum Status
        {
            Paused,
            OnGoing,
            Finished
        }

        public static GameSceneUIHandler Instance;
        [Header("MainGUI")] public GameObject mainLayer;
        public Button pauseButton;
        public Text enemyCount;
        
        [Header("PauseGUI")] public GameObject pauseLayer;
        public Button resumeButton;

        public Status status = Status.OnGoing;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            pauseButton.onClick.AddListener(Pause);
            resumeButton.onClick.AddListener(Resume);
        }

        private void Update()
        {
            if (!(enemyCount is null))
            {
                enemyCount.text = GameManager.instance.EnemyCount.ToString();
            }
        }

        private void Resume()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            status = Status.OnGoing;
            pauseLayer.SetActive(false);
            mainLayer.SetActive(true);
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            status = Status.Paused;
            pauseLayer.SetActive(true);
            mainLayer.SetActive(false);
        }
    }
}