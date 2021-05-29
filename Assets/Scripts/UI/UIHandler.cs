using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        [Header("MainMenu")] public GameObject mainLayer;
        public Button startButton;
        public Button selectCharacter;

        void Start()
        {
            startButton.onClick.AddListener(StartButton);
            selectCharacter.onClick.AddListener(CharacterButton);
        }

        private void StartButton()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.button);
            SceneManager.LoadScene(2);
        }
        private void CharacterButton()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.button);
            SceneManager.LoadScene(1);
        }
    }
}