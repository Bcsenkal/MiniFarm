using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource extraSfxSource;
        
        [Space(10)]
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private AudioClip winMusic;
        [SerializeField] private AudioClip failMusic;
        [SerializeField] private AudioClip collectSfx;
        [SerializeField] private AudioClip buttonClickSfx;

        [SerializeField] private AudioClip positiveButtonClickSfx;
        [SerializeField] private AudioClip negativeButtonClickSfx;

        #region Snapshot Functions

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxSource.volume = volume;
            extraSfxSource.volume = volume;
        }

        #endregion

        #region Music

        public void PlayMusic(bool isPlaying)
        {
            if (isPlaying)
            {
                musicSource.clip = musicClip;
                musicSource.loop = true;
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
                musicSource.loop = false;
            }
        }

        public void PlayFailMusic(){
            musicSource.Stop();
            musicSource.loop = false;
            musicSource.PlayOneShot(failMusic);
        }

        public void PlayWinMusic(){
            musicSource.Stop();
            musicSource.loop = false;
            musicSource.PlayOneShot(winMusic);
        }
        #endregion

        #region SFX

        public void PlayButtonClick()
        {
            sfxSource.PlayOneShot(buttonClickSfx);
        }

        public void PlayPositiveButtonClick()
        {
            sfxSource.PlayOneShot(positiveButtonClickSfx);
        }
        
        public void PlayNegativeButtonClick()
        {
            sfxSource.PlayOneShot(negativeButtonClickSfx);
        }
        
        public void PlayCollectSfx()
        {
            extraSfxSource.PlayOneShot(collectSfx);
        }

        #endregion
    }
}
