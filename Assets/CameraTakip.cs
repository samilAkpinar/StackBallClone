using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTakip : MonoBehaviour
{

    //üç deðiþken tanýmlamasý yapýlýr:
    //cameranýn yönü
    private Vector3 cameraPos;

    //player ve winin pozisyonlarýnýn bulunmasý
    private Transform player, win;

    private float cameraOffset = 4f;

    //ilk açýlýr açýlmaz awake fonksitonu çalýþmaktadýr.
    private void Awake()
    {
        //yön bilgisini player objesine atýyoruz;
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //win nesnesi yok ise
        if (win == null)
        {
            //win nesnesinin yönünün bulunmasý
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();

        }

        //kameranýn ne zaman takip etmesi
        if (transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffset)
        {
            //kamera pozisyonun atanmasý
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            //-5 kameranýn z deðeridir.
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);
        }
    }
}
