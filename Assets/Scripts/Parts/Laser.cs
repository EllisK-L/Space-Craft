using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    void Start() {
        Destroy(this.gameObject, 2);    
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up*10*Time.deltaTime);

    }
}
