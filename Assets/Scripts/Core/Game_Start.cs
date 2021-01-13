using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Start : MonoBehaviour
{
    GameObject reactor;

    //Need to make this automatic in the future!!

    Dictionary<string, ArrayList> shipComponents = new Dictionary<string, ArrayList>();

    GameObject[] forwardThrusters;
    GameObject[] backwardsThrusters;
    GameObject[] rightThrusters;
    GameObject[] leftThrusters;

    GameObject[] cubes;
    Functions functions;

    


    public void startGame(GameObject[,] gridArray) {
        functions = this.GetComponent<Functions>();

        //Find the reactor
        Debug.Log("Starting Game");
        for(int i = 0; i < gridArray.GetLength(0); i++) {
            for(int j = 0; j < gridArray.GetLength(1); j++) {
                if(gridArray[i, j].GetComponent<Grid_Object>().containsObject()) {
                    if (gridArray[i, j].GetComponent<Grid_Object>().getObject(false).name == "Reactor(Clone)") {
                        reactor = gridArray[i, j].GetComponent<Grid_Object>().getObject(false);
                    }
                }
            }
        }

        //setting all objects to children of the reactor and giving all the objects the tag of "PlayerShip"
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                if (gridArray[i, j].GetComponent<Grid_Object>().containsObject()) {
                    gridArray[i, j].GetComponent<Grid_Object>().getObject(false).transform.parent = reactor.transform;
                    gridArray[i, j].GetComponent<Grid_Object>().getObject(false).gameObject.tag = "PlayerShip";
                }
            }
        }
        reactor.gameObject.tag = "PlayerShip";
        //add the rigid body comonent to the reactor
        reactor.AddComponent<Rigidbody>();
        Rigidbody rbody = reactor.GetComponent<Rigidbody>();
        rbody.useGravity = false;
        rbody.drag = 1;
        rbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;


        //Get a list of all the objects independently

        //adding all the keys to the dict
        string[] blockTypes = functions.getBlockTypes();
        for(int i = 0; i < blockTypes.Length; i++) {
            shipComponents.Add(blockTypes[i], new ArrayList());
        }

        //adding all the componets to the dict as values 
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                Grid_Object gridObject = gridArray[i, j].GetComponent<Grid_Object>();
                if (gridObject.containsObject()) {
                        shipComponents[gridObject.getObject(false).GetComponent<Pickable_Object>().getType()].Add(gridObject.getObject(false));
                }
            }
        }



        //This should be at the end of this entire method
        functions.setGridArray(gridArray);
        functions.setShipComponents(shipComponents);
        this.GetComponent<Thrusters>().onstart();

        //Debug to check if dict contains the right values
        /*
        foreach(KeyValuePair<string,ArrayList> temp in shipComponents) {
            foreach(GameObject gamobj in temp.Value) {
                Debug.Log(gamobj.GetComponent<Pickable_Object>().getType());
            }
        }
        */




    }

   


}
