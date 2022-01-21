using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    //player i�in rigitbody tan�mlamas� yap�l�r.
    public Rigidbody rb;

    //nesneye �arpmat� m�? kontrol�
    bool carpma;

    //Kullan�c�ya verilecek s�redir.
    //topun yenilmez olmamas�n� sa�layacakt�r.
    //obstaclelara �arp�l�nca zaman artacakt�r.
    //obstaclelara gitmedi�inde yani top yukar� gitti�inde zaman azalt�lacakt�r.
    float currentTime;

    //s�reyi tamamlad���nda player invintable olacakt�r.
    bool invincible;

    //topun yenilmez oldu�unda alev almas�
    public GameObject fireShiled;

    //Ses kazanma, �lme, �l�ms�z silme, silme, ve topun z�plamas�
    [SerializeField]  //Panelden eklenmesi i�in yap�ld�.
    AudioClip win, depth, idestory, destroy, bounce; //De�i�ken atamalar� player k�sm�ndan yap�ld�.

    //Slider k�sm�nda g�sterimi i�in 
    public int currentObstacleNumber;
    public int totalObstacleNumber;


    //Invictiple i�in image kodlamas�
    public Image InvictableSlider;
    public GameObject InvictableOBJ;

    //UI Tan�mlama
    public GameObject finishUI;
    public GameObject gameOverUI;


    //Player state
    public enum PlayerState
    {
        Prepare, //haz�rl�k a�amas�
        Playing, //oynama a�amas�
        Died, //�l�m a�amas�
        Finish //oyun bitti a�amas�,
    }

    //Kodun ba�lang��ta gizlenmesi
    [HideInInspector]
    public PlayerState playerstate = PlayerState.Prepare;



    void Start()
    {
       totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;
    }

    private void Awake()
    {
        //rigitbody atamas� yap�l�r.
        rb = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //E�er playerstate playing ise kullan�c� oyun oynamaya ba�las�n.
        if (playerstate == PlayerState.Playing)
        {
            //input kontrol� yap�l�r.
            if (Input.GetMouseButtonDown(0))
            {
                carpma = true;
            }


            //b�rak�ld��� anda ise
            if (Input.GetMouseButtonUp(0))
            {
                carpma = false;
            }

            //invincible(yenilmez) kontrol� yap�l�r.
            //top hep yenilmez olmayacakt�r.
            if (invincible)
            {
                //yenilmez ise currentTime � azalt�lmas� gerekecektir
                currentTime -= Time.deltaTime * .35f;

                //top yenilmez oldu�unda fireShiled eklenir. 
                //fireShiled kapal� ise ative edilmelidir.
                if (!fireShiled.activeInHierarchy)
                {
                    fireShiled.SetActive(true);
                }
            }
            else
            {
                //yenilmez de�il ise;
                //fireShied false olacakt�r.
                if (fireShiled.activeInHierarchy)
                {
                    fireShiled.SetActive(false);
                }

                //obstaclelara �arpma de�eri true ise;
                if (carpma)
                {
                    //zaman� att�r�yoruz.
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


            //current time kontrol� yap�l�r
            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true; //yani player yenilmez olacakt�r.
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
                //Yenilmez oldu�unda filamount doldurulacakt�r.
                InvictableSlider.fillAmount = currentTime / 1;
            }

        }


        //E�er playerstate prepare ise 
      /*  if (playerstate == PlayerState.Prepare)
        {
            //Fareye t�klad���nda
           // if (Input.GetMouseButton(0)) //hata yeri burada verdi.
           // {
                //Oyun oynamaya ge�ilir.
                playerstate = PlayerState.Playing;
           // }
        }*/


        //Oyunun bitmesinin kontrol�
        //E�er playerstate finish ise next level yap�lmas� gereklidir.
        if (playerstate == PlayerState.Finish)
        {
            //Mouse t�klad���nda;
            if (Input.GetMouseButtonDown(0))
            {
                //win prefab�na gelip oyun bitti ise bir sonraki levele ge�mesi sa�lan�r.
                FindObjectOfType<LevelSpawner>().NextLevel();

            }
        }
    }

    public void shatterObstacles()
    {
        //E�er top yenilmez ise score 1 artt�r�l�r.
        if (invincible)
        {
            //ScoreManagera ula��l�p score de�erini 1 artt�rd�k
            ScoreManager.instance.addScore(1);
        }

        //score de�erini 2 artt�rd�k
        ScoreManager.instance.addScore(2);

    }


    //fiziksel olaylar� burada kullan�lacak.
    private void FixedUpdate()
    {
        //E�er oyun oynan�yor ise
        if (playerstate == PlayerState.Playing)
        {
            //carpma true ise
            if (carpma)
            {
                //topun a�a�� do�ru gitmesi
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
            //topu yukar� do�ru hareket etmesi. 
            rb.velocity = new Vector3(0,50*Time.deltaTime*5,0);
        }
        else
        {
            //e�er top yenilmez ise
            if (invincible)
            {
                //enemy ve plane ise hepsini destroy yap�yor. bu k�s�mda top yenilmez olmaktad�r.
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    //Obstaclelar�n silinmesi
                    //Destroy(collision.transform.parent.gameObject);
                    //obtaclelar�n k�r�lma animasyonun eklenmesi ve nesnelerin k�r�lmas�
                    collision.transform.parent.GetComponent<ObstacleController>().ShutterAllObstacles();
                    shatterObstacles();
                    //Yenilmez olsu�undaki ses
                    SoundManager.instance.PlaySoundFX(idestory, 0.5f);
                    currentObstacleNumber++;
                }
                
            }
            else
            {
                //top yenilmez de�il ise;
                //top �arpt��� zaman nesneyi destroy yap�lmas�
                //nesnenin enemy ad�ndaki tag'ine carpt���nda nesneyi yok edilmesi.
                if (collision.gameObject.tag == "enemy")
                {
                    //Obstaclelar�n silinmesi
                    //Destroy(collision.transform.parent.gameObject);
                    //obtaclelar�n k�r�lma animasyonun eklenmesi ve nesnelerin k�r�lmas�
                    collision.transform.parent.GetComponent<ObstacleController>().ShutterAllObstacles();
                    shatterObstacles();
                    //K�rma sesi verilir.
                    SoundManager.instance.PlaySoundFX(destroy, 0.5f);
                    currentObstacleNumber++;
                }
                else if (collision.gameObject.tag == "plane") //top plane tagine carpt��� zaman oyun bitirilecektir.
                {
                    Debug.Log("GameOver");
                    gameOverUI.SetActive(true);
                    playerstate = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    //ScoreManager de�erine ula��l�p score de�erinin 0 yap�lmas�
                    ScoreManager.instance.ResetScore();
                    //Oyun bitti�inde �lme sesi versin, 1 full a��k sestir. 0.5 ise yar�m ses.
                    SoundManager.instance.PlaySoundFX(depth, 0.5f);

                }
            }
        }

        //GameUI bulunmas�:
        //��kan sonu� levelslidera g�nderilir
        FindObjectOfType<GameUI>().LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);


        //E�er top win prefaba dokundu�u zaman ve playerstate playing ise finish verilmesi sa�lan�r.
        if (collision.gameObject.tag == "Finish" && playerstate == PlayerState.Playing)
        {

            //playerstate'e finish de�erinin atanmas�
            playerstate = PlayerState.Finish;
            //Kazanma durumunda ��kan ses
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
            //topu yukar� do�ru hareket etmesi. 
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            //Topun z�plama sesi;
            SoundManager.instance.PlaySoundFX(bounce, 0.5f);
        }
    }
}
