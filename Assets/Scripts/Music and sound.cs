using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    #endregion
    public AudioClip PushObject;
    public AudioClip electric_dead;
    public AudioClip tocar_sup;
    public AudioClip cambio_dir;
    public AudioClip dash;
    public AudioClip salto;
    public AudioClip run;
    public AudioClip Gameover;
    public AudioClip Gameplay;
    public AudioClip Menu;



    private AudioSource audioSource;
    private AudioSource Backmusic;
    private AudioSource menu;


    



    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Backmusic = gameObject.AddComponent<AudioSource>();
        Backmusic.clip = Gameplay;
        Backmusic.loop = true;

        menu = gameObject.AddComponent<AudioSource>();
        menu.clip = Menu;
        menu.loop = true;

        
    }
    public void MainMenuSong()
    {
        menu.Play();
    }
    public void StopMainMenuSong()
    {
        menu.Stop();
    }
    public void GameplaySong()
    {
        Backmusic.Play();
    }
    public void StopGameplaySong()
    {
        Backmusic.Stop();
    }
    public void GameoverSong()
    {
        audioSource.PlayOneShot(Gameover);
    }
    public void ElectricDeadSound()
    {
        audioSource.PlayOneShot(electric_dead);
    }
    public void TocarSupSound()
    {
        audioSource.PlayOneShot(tocar_sup);
    }
    public void JumpSound()
    {
        audioSource.PlayOneShot(salto);
    }
    public void DashSound()
    {
        audioSource.PlayOneShot(dash);
    }
    public void CambioDirSound()
    {
        audioSource.PlayOneShot(cambio_dir);
    }
    public void PushObjectSound()
    {
        audioSource.PlayOneShot(PushObject);
    }
    public void RunSound()
    {
        audioSource.PlayOneShot(run);
    }
}