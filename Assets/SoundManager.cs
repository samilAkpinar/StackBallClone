using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //SoundManger tipinde instance oluþturulmasý.
    public static SoundManager instance;

    //Deðiþkenlerin tanýmlanmasý
    //Sesleri çalmak için AudioSource oluþturulur.
    private AudioSource audioSource;
    private bool sound = true;


    private void Awake()
    {
        //Singleton olarak kullanmak için
        makeSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void makeSingleton()
    {
        //Eðer instace deðeri nulldan farklý ise;
        if (instance != null)
        {
            //objeyi destroy yapýlmasý
            Destroy(gameObject);
        }
        else
        {
            //Eðer null ise dont destrol yapýlmasý
            instance = this;
            DontDestroyOnLoad(this); //Bu þekilde scoreManagerda kalan scorelar aktif olarak kullanýlabilir.
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

    //ses kýsma fonksiyonu
    public void SoundOnOff()
    {
        //true ise false atar. ses kýsma için 
        sound = !sound;
    }

    public void PlaySoundFX(AudioClip clip, float volume)
    {
        //Eðer sound açýk ise çalmaya baþlasýn
        if (sound)
        {
            //istenilen ses ve ses ayarýnda çalýnmasýný saðlanacaktýr.
            audioSource.PlayOneShot(clip, volume);
            //Debug.Log("sesin çýkmasý");
        }
    }
}
