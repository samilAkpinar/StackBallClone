using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //Obstacle arrayinin oluþturulmasý.
    [SerializeField] //obstacle'larýn unity üzerinde görünmesini saðlar.  private olsa bile.
    private Obstacle[] obstacles = null;


    //Bütün obstacle nesnelerinin kýrýlmasý
    public void ShutterAllObstacles()
    {
        //baþlangýçta kontrol yapýlmasý
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        //obtacles içerisindeki özellikleri tek tek alýnmasý.
        foreach (var item in obstacles)
        {
            //item içersindeki shatter fonksiyonun çaðrýlmasý.
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
