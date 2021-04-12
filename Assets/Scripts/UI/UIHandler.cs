using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        [Header("MainMenu")] public GameObject mainLayer;
        public Button startButton;

        void Start()
        {
            startButton.onClick.AddListener(StartButton);
        }

        private void StartButton()
        {
            SceneManager.LoadScene(1);
        }
    }
}