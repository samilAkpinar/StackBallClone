using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTakip : MonoBehaviour
{

    //�� de�i�ken tan�mlamas� yap�l�r:
    //cameran�n y�n�
    private Vector3 cameraPos;

    //player ve winin pozisyonlar�n�n bulunmas�
    private Transform player, win;

    private float cameraOffset = 4f;

    //ilk a��l�r a��lmaz awake fonksitonu �al��maktad�r.
    private void Awake()
    {
        //y�n bilgisini player objesine at�yoruz;
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
            //win nesnesinin y�n�n�n bulunmas�
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();

        }

        //kameran�n ne zaman takip etmesi
        if (transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffset)
        {
            //kamera pozisyonun atanmas�
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            //-5 kameran�n z de�eridir.
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);
        }
    }
}
