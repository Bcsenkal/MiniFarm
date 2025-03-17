using Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    
    [AddComponentMenu("Rocinante/MuteButton")]
    public class MuteButton : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot normalSnapshot;
        [SerializeField] private AudioMixerSnapshot mutedSnapshot;
        private Button _button;
        private bool _isMuted;

        private Image _image;
        private Color _offColor;
        private readonly Color _onColor = Color.red;
        void Start()
        {
            _image = GetComponent<Image>();
            _offColor = _image.color;
            
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(MuteSound);
        }

        private void MuteSound()
        {
            _isMuted = !_isMuted;
            _image.color = _isMuted ? _onColor : _offColor;

            if (_isMuted)
            {
                mutedSnapshot.TransitionTo(.2f);
            }
            else
            {
                normalSnapshot.TransitionTo(.2f);
            }
            
            AudioManager.Instance.PlayButtonClick();

            
        }
        
    }
}
