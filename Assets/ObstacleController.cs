using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //Obstacle arrayinin olu�turulmas�.
    [SerializeField] //obstacle'lar�n unity �zerinde g�r�nmesini sa�lar.  private olsa bile.
    private Obstacle[] obstacles = null;


    //B�t�n obstacle nesnelerinin k�r�lmas�
    public void ShutterAllObstacles()
    {
        //ba�lang��ta kontrol yap�lmas�
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        //obtacles i�erisindeki �zellikleri tek tek al�nmas�.
        foreach (var item in obstacles)
        {
            //item i�ersindeki shatter fonksiyonun �a�r�lmas�.
            item.Shatter();
        }

        //nesnelerin ekrandan silinmesi 
        StartCoroutine(RemoveAllShatterParts());

        


    }

    IEnumerator  RemoveAllShatterParts()
    {
        //1 saniye bekletilmesi
        yield return new WaitForSeconds(1);
        //nesnelerin silinmesi
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
