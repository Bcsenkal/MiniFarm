using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        
        [BoxGroup("MixerSnapshots")] 
        [SerializeField] private AudioMixerSnapshot normalSnapshot;
        [BoxGroup("MixerSnapshots")] 
        [SerializeField] private AudioMixerSnapshot mutedSnapshot;
        [BoxGroup("MixerSnapshots")] 
        [SerializeField] private AudioMixerSnapshot musicMutedSnapshot;
        
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
        [SerializeField] private AudioClip rocinanteSfx;

        #region Snapshot Functions

        public void MuteSound(bool isMuted)
        {
            
            if (isMuted)
            {
                mutedSnapshot.TransitionTo(.2f);
            }
            else
            {
                normalSnapshot.TransitionTo(.2f);
            }


        }
        
        public void MuteMusic(bool isMuted)
        {
            
            if (isMuted)
            {
                musicMutedSnapshot.TransitionTo(.2f);
            }
            else
            {
                normalSnapshot.TransitionTo(.2f);
            }


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

        public void PlayRocinanteSfx(){
            sfxSource.PlayOneShot(rocinanteSfx);
        }

#endregion
    }
}
