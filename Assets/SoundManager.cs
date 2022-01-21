using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //SoundManger tipinde instance olu�turulmas�.
    public static SoundManager instance;

    //De�i�kenlerin tan�mlanmas�
    //Sesleri �almak i�in AudioSource olu�turulur.
    private AudioSource audioSource;
    private bool sound = true;


    private void Awake()
    {
        //Singleton olarak kullanmak i�in
        makeSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void makeSingleton()
    {
        //E�er instace de�eri nulldan farkl� ise;
        if (instance != null)
        {
            //objeyi destroy yap�lmas�
            Destroy(gameObject);
        }
        else
        {
            //E�er null ise dont destrol yap�lmas�
            instance = this;
            DontDestroyOnLoad(this); //Bu �ekilde scoreManagerda kalan scorelar aktif olarak kullan�labilir.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ses k�sma fonksiyonu
    public void SoundOnOff()
    {
        //true ise false atar. ses k�sma i�in 
        sound = !sound;
    }

    public void PlaySoundFX(AudioClip clip, float volume)
    {
        //E�er sound a��k ise �almaya ba�las�n
        if (sound)
        {
            //istenilen ses ve ses ayar�nda �al�nmas�n� sa�lanacakt�r.
            audioSource.PlayOneShot(clip, volume);
            //Debug.Log("sesin ��kmas�");
        }
    }
}
