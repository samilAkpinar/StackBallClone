using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    //player için rigitbody tanýmlamasý yapýlýr.
    public Rigidbody rb;

    //nesneye çarpmatý mý? kontrolü
    bool carpma;

    //Kullanýcýya verilecek süredir.
    //topun yenilmez olmamasýný saðlayacaktýr.
    //obstaclelara çarpýlýnca zaman artacaktýr.
    //obstaclelara gitmediðinde yani top yukarý gittiðinde zaman azaltýlacaktýr.
    float currentTime;

    //süreyi tamamladýðýnda player invintable olacaktýr.
    bool invincible;

    //topun yenilmez olduðunda alev almasý
    public GameObject fireShiled;

    //Ses kazanma, ölme, ölümsüz silme, silme, ve topun zýplamasý
    [SerializeField]  //Panelden eklenmesi için yapýldý.
    AudioClip win, depth, idestory, destroy, bounce; //Deðiþken atamalarý player kýsmýndan yapýldý.

    //Slider kýsmýnda gösterimi için 
    public int currentObstacleNumber;
    public int totalObstacleNumber;


    //Invictiple için image kodlamasý
    public Image InvictableSlider;
    public GameObject InvictableOBJ;

    //UI Tanýmlama
    public GameObject finishUI;
    public GameObject gameOverUI;


    //Player state
    public enum PlayerState
    {
        Prepare, //hazýrlýk aþamasý
        Playing, //oynama aþamasý
        Died, //ölüm aþamasý
        Finish //oyun bitti aþamasý,
    }

    //Kodun baþlangýçta gizlenmesi
    [HideInInspector]
    public PlayerState playerstate = PlayerState.Prepare;



    void Start()
    {
       totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;
    }

    private void Awake()
    {
        //rigitbody atamasý yapýlýr.
        rb = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Eðer playerstate playing ise kullanýcý oyun oynamaya baþlasýn.
        if (playerstate == PlayerState.Playing)
        {
            //input kontrolü yapýlýr.
            if (Input.GetMouseButtonDown(0))
            {
                carpma = true;
            }


            //býrakýldýðý anda ise
            if (Input.GetMouseButtonUp(0))
            {
                carpma = false;
            }

            //invincible(yenilmez) kontrolü yapýlýr.
            //top hep yenilmez olmayacaktýr.
            if (invincible)
            {
                //yenilmez ise currentTime ý azaltýlmasý gerekecektir
                currentTime -= Time.deltaTime * .35f;

                //top yenilmez olduðunda fireShiled eklenir. 
                //fireShiled kapalý ise ative edilmelidir.
                if (!fireShiled.activeInHierarchy)
                {
                    fireShiled.SetActive(true);
                }
            }
            else
            {
                //yenilmez deðil ise;
                //fireShied false olacaktýr.
                if (fireShiled.activeInHierarchy)
                {
                    fireShiled.SetActive(false);
                }

                //obstaclelara çarpma deðeri true ise;
                if (carpma)
                {
                    //zamaný attýrýyoruz.
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }


            if (currentTime >= 0.15f || InvictableSlider.color == Color.red)
            {
                InvictableOBJ.SetActive(true);
            }
            else
            {
                InvictableOBJ.SetActive(false);
            }


            //current time kontrolü yapýlýr
            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true; //yani player yenilmez olacaktýr.
                Debug.Log("invincible");
                InvictableSlider.color = Color.red;

            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                InvictableSlider.color = Color.white;
            }

            if (InvictableOBJ.activeInHierarchy)
            {
                //Yenilmez olduðunda filamount doldurulacaktýr.
                InvictableSlider.fillAmount = currentTime / 1;
            }

        }


        //Eðer playerstate prepare ise 
      /*  if (playerstate == PlayerState.Prepare)
        {
            //Fareye týkladýðýnda
           // if (Input.GetMouseButton(0)) //hata yeri burada verdi.
           // {
                //Oyun oynamaya geçilir.
                playerstate = PlayerState.Playing;
           // }
        }*/


        //Oyunun bitmesinin kontrolü
        //Eðer playerstate finish ise next level yapýlmasý gereklidir.
        if (playerstate == PlayerState.Finish)
        {
            //Mouse týkladýðýnda;
            if (Input.GetMouseButtonDown(0))
            {
                //win prefabýna gelip oyun bitti ise bir sonraki levele geçmesi saðlanýr.
                FindObjectOfType<LevelSpawner>().NextLevel();

            }
        }
    }

    public void shatterObstacles()
    {
        //Eðer top yenilmez ise score 1 arttýrýlýr.
        if (invincible)
        {
            //ScoreManagera ulaþýlýp score deðerini 1 arttýrdýk
            ScoreManager.instance.addScore(1);
        }

        //score deðerini 2 arttýrdýk
        ScoreManager.instance.addScore(2);

    }


    //fiziksel olaylarý burada kullanýlacak.
    private void FixedUpdate()
    {
        //Eðer oyun oynanýyor ise
        if (playerstate == PlayerState.Playing)
        {
            //carpma true ise
            if (carpma)
            {
                //topun aþaðý doðru gitmesi
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }

    }


    //enemy bulma
    private void OnCollisionEnter(Collision collision)
    {
        //carpma false ise
        if (!carpma)
        {
            //topu yukarý doðru hareket etmesi. 
            rb.velocity = new Vector3(0,50*Time.deltaTime*5,0);
        }
        else
        {
            //eðer top yenilmez ise
            if (invincible)
            {
                //enemy ve plane ise hepsini destroy yapýyor. bu kýsýmda top yenilmez olmaktadýr.
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    //Obstaclelarýn silinmesi
                    //Destroy(collision.transform.parent.gameObject);
                    //obtaclelarýn kýrýlma animasyonun eklenmesi ve nesnelerin kýrýlmasý
                    collision.transform.parent.GetComponent<ObstacleController>().ShutterAllObstacles();
                    shatterObstacles();
                    //Yenilmez olsuðundaki ses
                    SoundManager.instance.PlaySoundFX(idestory, 0.5f);
                    currentObstacleNumber++;
                }
                
            }
            else
            {
                //top yenilmez deðil ise;
                //top çarptýðý zaman nesneyi destroy yapýlmasý
                //nesnenin enemy adýndaki tag'ine carptýðýnda nesneyi yok edilmesi.
                if (collision.gameObject.tag == "enemy")
                {
                    //Obstaclelarýn silinmesi
                    //Destroy(collision.transform.parent.gameObject);
                    //obtaclelarýn kýrýlma animasyonun eklenmesi ve nesnelerin kýrýlmasý
                    collision.transform.parent.GetComponent<ObstacleController>().ShutterAllObstacles();
                    shatterObstacles();
                    //Kýrma sesi verilir.
                    SoundManager.instance.PlaySoundFX(destroy, 0.5f);
                    currentObstacleNumber++;
                }
                else if (collision.gameObject.tag == "plane") //top plane tagine carptýðý zaman oyun bitirilecektir.
                {
                    Debug.Log("GameOver");
                    gameOverUI.SetActive(true);
                    playerstate = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    //ScoreManager deðerine ulaþýlýp score deðerinin 0 yapýlmasý
                    ScoreManager.instance.ResetScore();
                    //Oyun bittiðinde ölme sesi versin, 1 full açýk sestir. 0.5 ise yarým ses.
                    SoundManager.instance.PlaySoundFX(depth, 0.5f);

                }
            }
        }

        //GameUI bulunmasý:
        //Çýkan sonuç levelslidera gönderilir
        FindObjectOfType<GameUI>().LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);


        //Eðer top win prefaba dokunduðu zaman ve playerstate playing ise finish verilmesi saðlanýr.
        if (collision.gameObject.tag == "Finish" && playerstate == PlayerState.Playing)
        {

            //playerstate'e finish deðerinin atanmasý
            playerstate = PlayerState.Finish;
            //Kazanma durumunda çýkan ses
            SoundManager.instance.PlaySoundFX(win, 0.5f);
            finishUI.SetActive(true);
            finishUI.transform.GetChild(0).GetComponent<Text>().text = "Level " + PlayerPrefs.GetInt("Level", 1);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        //carpma false ise
        if (!carpma || collision.gameObject.tag == "Finish")
        {
            //topu yukarý doðru hareket etmesi. 
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            //Topun zýplama sesi;
            SoundManager.instance.PlaySoundFX(bounce, 0.5f);
        }
    }
}
