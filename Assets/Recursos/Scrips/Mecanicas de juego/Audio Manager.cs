using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sources")]
    public AudioSource musicSource;   // Para música de fondo (loop)
    public AudioSource sfxSource;     // Para efectos (si está vacío, se crea uno)

    [Header("Clips - Música")]
    public AudioClip musicaMenu;
    public AudioClip musicaNivel;

    [Header("Clips - SFX")]
    public AudioClip moneda;
    public AudioClip caminar;
    public AudioClip saltar;
    public AudioClip recibirDanio;
    public AudioClip gameOver;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Si no se asignó un sfxSource en el Inspector, creamos uno
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        // Reproducir música de nivel por defecto
        CambiarMusica(musicaNivel);
    }

    /// <summary> Reproduce un efecto de sonido sin interrumpir la música. </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        // Ahora siempre tenemos un sfxSource válido
        sfxSource.PlayOneShot(clip);
    }

    /// <summary> Cambia la música de fondo. </summary>
    public void CambiarMusica(AudioClip nuevaMusica)
    {
        if (musicSource.clip == nuevaMusica) return;

        musicSource.Stop();
        musicSource.clip = nuevaMusica;
        musicSource.loop = true;
        musicSource.Play();
    }

    /// <summary> Detiene la música de fondo. </summary>
    public void StopMusica()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }
}