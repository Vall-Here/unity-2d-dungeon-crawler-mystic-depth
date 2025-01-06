using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance;

    public sound[] musicSounds, sfxSound;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan audioManager saat pindah scene
        }
        else
        {
            Destroy(gameObject); // Hancurkan instance duplikat
        }
    }

    private void Start()
    {
        PlayMusic("theme"); // Mulai musik tema di awal
    }

    public void PlayMusic(string name)
    {
        sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found: " + name);
            return;
        }

        // Jika musik yang diminta sudah diputar, jangan ubah
        if (musicSource.clip == s.clip && musicSource.isPlaying)
        {
            Debug.Log("Music already playing: " + name);
            return;
        }
        StartCoroutine(TransitionMusic(name)); // Gunakan transisi saat memutar musik

        musicSource.clip = s.clip;
        musicSource.Play();

    }

    public void playSFX(string name)
    {
        sound s = Array.Find(sfxSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX not found: " + name);
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    private IEnumerator TransitionMusic(string newMusicName)
    {
        // Fade out current music
        if (musicSource.isPlaying)
        {
            for (float volume = musicSource.volume; volume > 0; volume -= Time.deltaTime / 1f) // Durasi fade-out 1 detik
            {
                musicSource.volume = volume;
                yield return null;
            }
            musicSource.Stop();
        }

        // Find the new music
        sound newMusic = Array.Find(musicSounds, x => x.name == newMusicName);

        if (newMusic == null)
        {
            Debug.Log("Music not found: " + newMusicName);
            yield break;
        }

        // Play new music and fade in
        musicSource.clip = newMusic.clip;
        musicSource.Play();
        for (float volume = 0; volume <= 1; volume += Time.deltaTime / 1f) // Durasi fade-in 1 detik
        {
            musicSource.volume = volume;
            yield return null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ganti musik berdasarkan nama scene
        if (scene.name == "mainMenu" || scene.name == "selectLevelScene")
        {
            PlayMusic("theme"); // Musik untuk menu utama dan pemilihan level
        }
        else if (scene.name.StartsWith("Level")) // Untuk scene game seperti Level1, Level2, dst.
        {
            PlayMusic("backsoundGame"); // Musik untuk game
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

