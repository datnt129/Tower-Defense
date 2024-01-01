using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum MusicName
    {
        intro,
        gameplay
    };

    [Header("Music")]
    [SerializeField] private AudioSource introSource;

    [SerializeField] private AudioSource gameplaySource;

    [SerializeField] [Range(0f, 1f)] private float m_maxMusicVolume;

    [Header("SFX")] [SerializeField] private AudioSource sfxSource;

    private readonly float k_volumeSteps = 0.01f;

    private void Start()
    {
        PlayMusic(MusicName.intro);
    }
   
    public void PlaySoundEffect(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    // Play the music of the game without any effect
    public void PlayMusic(MusicName musicToPlay)
    {
        if (musicToPlay == MusicName.intro)
        {
            introSource.enabled = true;
            introSource.volume = m_maxMusicVolume;
            introSource.Play();

            gameplaySource.Stop();
            gameplaySource.enabled = false;
        }
        else
        {
            gameplaySource.enabled = true;
            gameplaySource.volume = m_maxMusicVolume;
            gameplaySource.Play();

            introSource.Stop();
            introSource.enabled = false;
        }
    }

    public void SwitchToGameplayMusic()
    {
        gameplaySource.volume = 0f;
        gameplaySource.enabled = true;
        gameplaySource.Play();

        StartCoroutine(SwitchMusicToPlay(MusicName.gameplay));
    }

    private IEnumerator SwitchMusicToPlay(MusicName musicToPlay)
    {
        yield return FadeInMusicToPlayFadeOutCurrentMusic(musicToPlay);

        StopAudioOfCurrentMusic(musicToPlay);
    }

    private IEnumerator FadeInMusicToPlayFadeOutCurrentMusic(MusicName musicToPlay)
    {
        float volume = 0f;

        // Repeat until the volume go up to the max
        while (volume <= m_maxMusicVolume)
        {
            if (musicToPlay == MusicName.intro)
            {
                introSource.volume += k_volumeSteps;
                gameplaySource.volume -= k_volumeSteps;
            }
            else
            {
                introSource.volume -= k_volumeSteps;
                gameplaySource.volume += k_volumeSteps;
            }
            volume += k_volumeSteps;
            yield return new WaitForEndOfFrame();
        }
    }

    private void StopAudioOfCurrentMusic(MusicName musicToPlay)
    {
        if (musicToPlay == MusicName.intro)
        {
            gameplaySource.Stop();
            gameplaySource.enabled = false;
        }
        else
        {
            introSource.Stop();
            introSource.enabled = false;
        }
    }
}
