using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    //iki array olu�turulmas�
    public GameObject[] obstacleModel;
    [HideInInspector]
    public GameObject[] obstaclePrefab = new GameObject[4]; //model i�indeki nesnelerden 4er adet almay� sa�lar.

    public GameObject winPrefab;

    //nesnelere atanacak temp nesnesi olu�turulmas�
    private GameObject temp1Obstacle, temp2Obstacle;

    //level layer ve obstacle aras� ge�i� i�in say� ekleme
    private int level = 1, addNumber = 7;

    //for d�ng�s�nde kullan�lacak bir de�i�ken tan�mlanmas�
    float obstacleNumber = 0;

    public Material plateMat, baseMat;
    public MeshRenderer playerMeshRenderer;

    //Levelin 1'e indirilmesi;
    //private void Awake()
    //{
    //    PlayerPrefs.SetInt("Level", 1);
    //}

    void Start()
    {
        //Playerprefs de�erine 1 atanm�� oldu.
        level = PlayerPrefs.GetInt("Level", 1);

        //rastgele obstacle nesnesi olu�turulmas�.
        randomObstacleGenerator();

        //enemy i�in random say� olu�turma 
        float randomNumber = Random.value;

        //0.05f -> �steki obstacle ile alttaki obstacle aras�ndaki mesafedir. obstaclelar aras�ndaki farkt�r.
        //obstaclelar�n a�a�� do�ru dizilmesi ile ilgilidir.
        for (obstacleNumber = 0; obstacleNumber > -level - addNumber; obstacleNumber -= 0.5f)
        {
            //level kontrol�n�n yap�lmas�
            if (level <= 20)
            {
                //0 ile 2 aras�ndaki de�eri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(0, 2)]);
            }
            else if (level > 20 && level < 50)
            {
                //1 ile 3 aras�ndaki de�eri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(1, 3)]);
            }
            else if (level >= 50 && level <= 100)
            {
                //2 ile 4 aras�ndaki de�eri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(2, 4)]);
            }
            else if (level > 100)
            {
                //3 ile 4 aras�ndaki de�eri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(3, 4)]);
            }

            //temp1obstacle y�n� ayarlan�r ve nesne eklemesi burada yap�l�r.
            temp1Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);


            //nesne eklerken kontrol yap�lmas�
            //obstacle number yukar�dan - geldi�i i�in bunu + olarak al�nmas� gereklidir.
            if (Mathf.Abs(obstacleNumber) >= level * .3f && Mathf.Abs(obstacleNumber) <= level * .6f)
            {
                //nesne rotasyonu de�i�tirilebilir.  45 90 180 derece a�� ile
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                temp1Obstacle.transform.eulerAngles += Vector3.up * 180; //180 derece birden d�nd�r�lmesi sa�lanacakt�r.

            }
            else if (Mathf.Abs(obstacleNumber) > level * 0.8f)
            {
                //nesne rotasyonu de�i�tirilebilir.  45 90 180 derece a�� ile
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);

                //d�nd�rme i�leminin biraz daha zorla�t�r�lmas�
                if (randomNumber > 0.75f)
                {
                    temp1Obstacle.transform.eulerAngles += Vector3.up * 180; //180 derece birden d�nd�r�lmesi sa�lanacakt�r.
                }

            }





            //nesnelerin d�nmesi i�in nesneleri rotate manager i�ine ekler.
            temp1Obstacle.transform.parent = FindObjectOfType<RotateManager>().transform;
        }

        temp2Obstacle = Instantiate(winPrefab);
        //temp2Obstacle pozisyonu;
        temp2Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //renklerin de�i�tirilmes i�lemi yap�l�r.
        if (Input.GetMouseButtonDown(0))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color + Color.gray;
            playerMeshRenderer.material.color = baseMat.color;

        }
    }

    //rastgele bir �ekilde nesne olu�turma.
    public void randomObstacleGenerator()
    {
        //0 ile 5 aras�nda rastgele bir say� al�r. 5 farkl� obstacle t�r� oludu�u i�in.
        int random = Random.Range(0, 5);

        switch (random)
        {
            //case 0 oldu�unda ilk d�rt model al�n�r.
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    //prefab i�erisine model eklemesi yap�l�r.
                    obstaclePrefab[i] = obstacleModel[i];
                }
                break;

            //case 1 oldu�unda ikinci d�rt model al�n�r.
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    //prefab i�erisine model eklemesi yap�l�r.
                    //i+4 ikinci 4 model al�nmas� sa�lan�yor.
                    obstaclePrefab[i] = obstacleModel[i + 4];
                }
                break;

            //case 2 oldu�unda ���nc� d�rt model al�n�r.
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    //prefab i�erisine model eklemesi yap�l�r.
                    obstaclePrefab[i] = obstacleModel[i + 8];
                }
                break;

            //case 3 oldu�unda d�rd�nc� d�rt model al�n�r.
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    //prefab i�erisine model eklemesi yap�l�r.
                    //12 ile 15 aras�ndaki modeleri atar.
                    obstaclePrefab[i] = obstacleModel[i + 12];
                }
                break;

            //case 4 oldu�unda be�inci d�rt model al�n�r.
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    //prefab i�erisine model eklemesi yap�l�r.
                    //16 ile 20 aras�ndaki modelleri atar.
                    obstaclePrefab[i] = obstacleModel[i + 16];
                }
                break;




            default:
                break;
        }

    }


    //Oyunda Level i�lemi
    public void NextLevel()
    {
        //Level de�eri bir artt�t�lacakt�r.
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        //sahnenin eklenmesi 
        SceneManager.LoadScene(0); //ayn� sahane eklenecek sadece level de�eri artt�r�lacakt�r. Her level de obstaclelar�n say�s� artmaktad�r.

    }
}
