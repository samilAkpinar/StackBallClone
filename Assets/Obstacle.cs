using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    //Obstacle içindeki özelliklerin eklenmesi.
    private Rigidbody rigidbody;
    private MeshRenderer meshRenderer;
    private Collider collider;

    //Obstacle konrolü
    private ObstacleController obstacleController;

    //özellikleri program ilk çalýþtýðýnda tanýmlanmasý yapýlýr.
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        //nesnenin parent kýsmýndaki tanýmlamasý yapýlýr.
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


    //Obstacle kýrýlacaðý zamanki animasyonun kodlanmasý
    //Dýþarýdan çaðýrýlacaðý için public denmesi
    public void Shatter()
    {
        //rigitbody'nin kinetaðinin kapatýlmasý
        rigidbody.isKinematic = false;
        //obstacle parçalandýrktan sonra herhangi bir yere çarpmamasý için collider deðeri false yapýlýr.
        collider.enabled = false;

        //Üç adet pointer eklenmesi.
        Vector3 forcePoint = transform.parent.position; //parent kýsmýndaki orta noktayý verir.
        float parentXPos = transform.parent.position.x; //parent kýsmýndaki orta noktanýn x kýsmýndýr ve 0 dýr.
        float xPos = meshRenderer.bounds.center.x; //meshrenderer üzerindeki orta noktaynýn x deðerini verir.

        //bir direction bulunmasý gereklidir. saða veya sola gitmesi saðlanýr.
        Vector3 subdir = (parentXPos - xPos < 0) ? Vector3.right : Vector3.left;

        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        //Force ve tork eklenmesi
        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        //Yapýlan iþlemleri rigitbody ile güç ve ivme verme
        rigidbody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);

        //Force eklenmesi yapýldýktan sonra tork eklemesi yapýlýr.
        rigidbody.AddTorque(Vector3.left * torque);

        //Rigitbodye hýz verilmesi
        rigidbody.velocity = Vector3.down;
    }
}
