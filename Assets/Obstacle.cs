using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    //Obstacle i�indeki �zelliklerin eklenmesi.
    private Rigidbody rigidbody;
    private MeshRenderer meshRenderer;
    private Collider collider;

    //Obstacle konrol�
    private ObstacleController obstacleController;

    //�zellikleri program ilk �al��t���nda tan�mlanmas� yap�l�r.
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        //nesnenin parent k�sm�ndaki tan�mlamas� yap�l�r.
        obstacleController = transform.parent.GetComponent<ObstacleController>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Obstacle k�r�laca�� zamanki animasyonun kodlanmas�
    //D��ar�dan �a��r�laca�� i�in public denmesi
    public void Shatter()
    {
        //rigitbody'nin kineta�inin kapat�lmas�
        rigidbody.isKinematic = false;
        //obstacle par�aland�rktan sonra herhangi bir yere �arpmamas� i�in collider de�eri false yap�l�r.
        collider.enabled = false;

        //�� adet pointer eklenmesi.
        Vector3 forcePoint = transform.parent.position; //parent k�sm�ndaki orta noktay� verir.
        float parentXPos = transform.parent.position.x; //parent k�sm�ndaki orta noktan�n x k�sm�nd�r ve 0 d�r.
        float xPos = meshRenderer.bounds.center.x; //meshrenderer �zerindeki orta noktayn�n x de�erini verir.

        //bir direction bulunmas� gereklidir. sa�a veya sola gitmesi sa�lan�r.
        Vector3 subdir = (parentXPos - xPos < 0) ? Vector3.right : Vector3.left;

        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        //Force ve tork eklenmesi
        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        //Yap�lan i�lemleri rigitbody ile g�� ve ivme verme
        rigidbody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);

        //Force eklenmesi yap�ld�ktan sonra tork eklemesi yap�l�r.
        rigidbody.AddTorque(Vector3.left * torque);

        //Rigitbodye h�z verilmesi
        rigidbody.velocity = Vector3.down;
    }
}
