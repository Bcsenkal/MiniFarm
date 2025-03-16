using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    
    [AddComponentMenu("Rocinante/PauseButton")]
    public class PauseButton : MonoBehaviour
    {
        private Button _button;
        private bool _isPaused;

        private Image _image;
        private Color _offColor;
        private readonly Color _onColor = Color.red;
        void Start()
        {
            _image = GetComponent<Image>();
            _offColor = _image.color;
            
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PauseGame);
            
            EventManager.Instance.ONLevelEnd += ONLevelEnd;
        }

        private void ONLevelEnd(bool obj)
        {
            _button.onClick.RemoveListener(PauseGame);
        }


        private void PauseGame()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;

            _image.color = _isPaused ? _onColor : _offColor;
            
            AudioManager.Instance.PlayButtonClick();

        }
    }
}
