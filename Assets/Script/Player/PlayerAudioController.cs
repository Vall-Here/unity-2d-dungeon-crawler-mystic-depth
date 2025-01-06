using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip walk;
    public AudioClip attack;
    public AudioClip pushing;
    public AudioClip Dead;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalk() {
        float walkVolume = 0.3f; 
        audioSource.PlayOneShot(walk, walkVolume);
    }

    public void PlayAttack() {
        // audioSource.clip = attack;
        audioSource.PlayOneShot(attack);
    }

    public void PlayPushing() {
        // audioSource.clip = pushing;
        if (pushing != null) {
            audioSource.PlayOneShot(pushing);
        }
    }

    public void PlayDead() {
        // audioSource.clip = Dead;
        audioSource.PlayOneShot(Dead);
    }


    public void StopAudio() {
        audioSource.Stop();
    }

    public bool isPlaying() {
        return audioSource.isPlaying;
    }
}
