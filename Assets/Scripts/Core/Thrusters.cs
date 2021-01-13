using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrusters : MonoBehaviour
{
    private Functions functions;
    private GameObject reactor;
    private Rigidbody rbody;


    // Start is called before the first frame update
    void Start()
    {
        functions = this.GetComponent<Functions>();

    }
    public void onstart() {
        reactor = functions.getAllOfAComponent("Reactor")[0];
        rbody = reactor.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            GameObject[] allThrusters = functions.getAllOfAComponent("Thruster");
            for(int i = 0; i < allThrusters.Length; i++) {
                if(allThrusters[i].GetComponent<Pickable_Object>().getOrientation() == "down") {
                    
                    rbody.AddForceAtPosition(allThrusters[i].transform.forward * -1 * Time.deltaTime * functions.getThrust(), allThrusters[i].transform.position);
                }
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            GameObject[] allThrusters = functions.getAllOfAComponent("Thruster");
            for (int i = 0; i < allThrusters.Length; i++) {
                if (allThrusters[i].GetComponent<Pickable_Object>().getOrientation() == "up") {

                    rbody.AddForceAtPosition(allThrusters[i].transform.forward * -1 * Time.deltaTime * functions.getThrust(), allThrusters[i].transform.position);
                }
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            GameObject[] allThrusters = functions.getAllOfAComponent("Thruster");
            for (int i = 0; i < allThrusters.Length; i++) {
                if (allThrusters[i].GetComponent<Pickable_Object>().getOrientation() == "right") {

                    rbody.AddForceAtPosition(allThrusters[i].transform.forward * -1 * Time.deltaTime * functions.getThrust(), allThrusters[i].transform.position);
                }
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            GameObject[] allThrusters = functions.getAllOfAComponent("Thruster");
            for (int i = 0; i < allThrusters.Length; i++) {
                if (allThrusters[i].GetComponent<Pickable_Object>().getOrientation() == "left") {

                    rbody.AddForceAtPosition(allThrusters[i].transform.forward * -1 *Time.deltaTime * functions.getThrust(), allThrusters[i].transform.position);
                }
            }
        }
    }
}
