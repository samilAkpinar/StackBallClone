using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    //iki array oluþturulmasý
    public GameObject[] obstacleModel;
    [HideInInspector]
    public GameObject[] obstaclePrefab = new GameObject[4]; //model içindeki nesnelerden 4er adet almayý saðlar.

    public GameObject winPrefab;

    //nesnelere atanacak temp nesnesi oluþturulmasý
    private GameObject temp1Obstacle, temp2Obstacle;

    //level layer ve obstacle arasý geçiþ için sayý ekleme
    private int level = 1, addNumber = 7;

    //for düngüsünde kullanýlacak bir deðiþken tanýmlanmasý
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
        //Playerprefs deðerine 1 atanmýþ oldu.
        level = PlayerPrefs.GetInt("Level", 1);

        //rastgele obstacle nesnesi oluþturulmasý.
        randomObstacleGenerator();

        //enemy için random sayý oluþturma 
        float randomNumber = Random.value;

        //0.05f -> üsteki obstacle ile alttaki obstacle arasýndaki mesafedir. obstaclelar arasýndaki farktýr.
        //obstaclelarýn aþaðý doðru dizilmesi ile ilgilidir.
        for (obstacleNumber = 0; obstacleNumber > -level - addNumber; obstacleNumber -= 0.5f)
        {
            //level kontrolünün yapýlmasý
            if (level <= 20)
            {
                //0 ile 2 arasýndaki deðeri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(0, 2)]);
            }
            else if (level > 20 && level < 50)
            {
                //1 ile 3 arasýndaki deðeri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(1, 3)]);
            }
            else if (level >= 50 && level <= 100)
            {
                //2 ile 4 arasýndaki deðeri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(2, 4)]);
            }
            else if (level > 100)
            {
                //3 ile 4 arasýndaki deðeri temp1obstacle eklendi.
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(3, 4)]);
            }

            //temp1obstacle yönü ayarlanýr ve nesne eklemesi burada yapýlýr.
            temp1Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);


            //nesne eklerken kontrol yapýlmasý
            //obstacle number yukarýdan - geldiði için bunu + olarak alýnmasý gereklidir.
            if (Mathf.Abs(obstacleNumber) >= level * .3f && Mathf.Abs(obstacleNumber) <= level * .6f)
            {
                //nesne rotasyonu deðiþtirilebilir.  45 90 180 derece açý ile
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                temp1Obstacle.transform.eulerAngles += Vector3.up * 180; //180 derece birden döndürülmesi saðlanacaktýr.

            }
            else if (Mathf.Abs(obstacleNumber) > level * 0.8f)
            {
                //nesne rotasyonu deðiþtirilebilir.  45 90 180 derece açý ile
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);

                //döndürme iþleminin biraz daha zorlaþtýrýlmasý
                if (randomNumber > 0.75f)
                {
                    temp1Obstacle.transform.eulerAngles += Vector3.up * 180; //180 derece birden döndürülmesi saðlanacaktýr.
                }

            }





            //nesnelerin dönmesi için nesneleri rotate manager içine ekler.
            temp1Obstacle.transform.parent = FindObjectOfType<RotateManager>().transform;
        }

        temp2Obstacle = Instantiate(winPrefab);
        //temp2Obstacle pozisyonu;
        temp2Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //renklerin deðiþtirilmes iþlemi yapýlýr.
        if (Input.GetMouseButtonDown(0))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color + Color.gray;
            playerMeshRenderer.material.color = baseMat.color;

        }
    }

    //rastgele bir þekilde nesne oluþturma.
    public void randomObstacleGenerator()
    {
        //0 ile 5 arasýnda rastgele bir sayý alýr. 5 farklý obstacle türü oluduðu için.
        int random = Random.Range(0, 5);

        switch (random)
        {
            //case 0 olduðunda ilk dört model alýnýr.
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    //prefab içerisine model eklemesi yapýlýr.
                    obstaclePrefab[i] = obstacleModel[i];
                }
                break;

            //case 1 olduðunda ikinci dört model alýnýr.
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    //prefab içerisine model eklemesi yapýlýr.
                    //i+4 ikinci 4 model alýnmasý saðlanýyor.
                    obstaclePrefab[i] = obstacleModel[i + 4];
                }
                break;

            //case 2 olduðunda üçüncü dört model alýnýr.
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    //prefab içerisine model eklemesi yapýlýr.
                    obstaclePrefab[i] = obstacleModel[i + 8];
                }
                break;

            //case 3 olduðunda dördüncü dört model alýnýr.
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    //prefab içerisine model eklemesi yapýlýr.
                    //12 ile 15 arasýndaki modeleri atar.
                    obstaclePrefab[i] = obstacleModel[i + 12];
                }
                break;

            //case 4 olduðunda beþinci dört model alýnýr.
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    //prefab içerisine model eklemesi yapýlýr.
                    //16 ile 20 arasýndaki modelleri atar.
                    obstaclePrefab[i] = obstacleModel[i + 16];
                }
                break;




            default:
                break;
        }

    }


    //Oyunda Level iþlemi
    public void NextLevel()
    {
        //Level deðeri bir arttýtýlacaktýr.
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        //sahnenin eklenmesi 
        SceneManager.LoadScene(0); //ayný sahane eklenecek sadece level deðeri arttýrýlacaktýr. Her level de obstaclelarýn sayýsý artmaktadýr.

    }
}
