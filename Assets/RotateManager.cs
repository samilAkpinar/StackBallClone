using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    //nesnenin dönme hýzý
    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate nesnesi update de dönmesi;
        transform.Rotate(new Vector3(0, speed * Time.deltaTime));
    }
}
