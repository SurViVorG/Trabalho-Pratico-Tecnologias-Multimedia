using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sons")]
    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip gameMusic;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource         = gameObject.AddComponent<AudioSource>();
        sfxSource           = gameObject.AddComponent<AudioSource>();
        musicSource.loop    = true;
        musicSource.volume  = PlayerPrefs.GetFloat("GameVolume", 0.5f) * 0.4f;
    }

    void Start()
    {
        if (gameMusic != null)
        {
            musicSource.clip = gameMusic;
            musicSource.Play();
        }
    }

    public void PlayHit()
    {
        if (hitSound != null)
            sfxSource.PlayOneShot(hitSound);
    }

    public void PlayGameOver()
    {
        musicSource.Stop();
        if (gameOverSound != null)
            sfxSource.PlayOneShot(gameOverSound);
    }
}