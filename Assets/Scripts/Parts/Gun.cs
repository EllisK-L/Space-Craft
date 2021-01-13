using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject laser;
    GameObject tempLaser;
    float oldTime;
    public float fireRate;
    private void Start() {
        oldTime = Time.time;
    }

    public void shoot() {
        tempLaser = Instantiate(laser, this.transform.position, this.transform.rotation);
        tempLaser.transform.Rotate(90, 0, 0);
        tempLaser.transform.Translate(Vector3.up * 1.2f);
        oldTime = Time.time;
    }

    // This is where the gun will move to the mouse cursor
    void Update()
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        this.transform.LookAt(new Vector3(v3.x,this.transform.position.y,v3.z));
        //Debug.Log(Time.time - oldTime);
        //shooting
        if (Input.GetMouseButton(0) && Time.time - oldTime >= (fireRate * Random.Range(1,1))) {
            //Checking if the shot is going to hit the ship by ray cast. Now this might be problematic for larger ships, as the shot is not a raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50, ~(1 << LayerMask.NameToLayer("Bullet")))) {
                if(hit.transform.gameObject.tag != "PlayerShip") {
                    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1);
                    shoot();
                }
                Debug.Log("Did Hit");
            }
            else {
                shoot();
            }
        }
    }
}
