using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public GameObject particle;
    public GameObject lockedObject;
    public GameObject mCursor;
    public GameObject cursorCube;
    private bool unlocking = false;
    public GameObject gridHelper;
 
    
    void Update()
    {
        rotateUpdate();
        mouseUpdate();

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit)) {
                //Check if the object can be picked up
                if(lockedObject == null && (hit.transform.gameObject.GetComponent<Pickable_Object>() != null || hit.transform.gameObject.GetComponent<Grid_Object>() != null) && unlocking == false) {
                    lockObject(hit.transform.gameObject, hit);
                    Debug.Log("Locking");
                }
            }
        }

        //Deleting Object
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit1;
            Ray mouseRay1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay1, out hit1))
            {
                //if (lockedObject == null && hit1.transform.gameObject.GetComponent<Pickable_Object>() != null && hit1.transform.gameObject.GetComponent<Pickable_Object>().isSource() == false)
                if(lockedObject == null && hit1.transform.gameObject.GetComponent<Grid_Object>() != null && hit1.transform.gameObject.GetComponent<Grid_Object>().getObject(false) != null){
                    hit1.transform.gameObject.GetComponent<Grid_Object>().getObject(false).GetComponent<Pickable_Object>().getSource().GetComponent<Pickable_Object>().changeQuantity(1);

                    //hit1.transform.gameObject.GetComponent<Pickable_Object>().getSource().GetComponent<Pickable_Object>().changeQuantity(1);
                    Destroy(hit1.transform.gameObject.GetComponent<Grid_Object>().getObject(true));
                }
            }
            gridHelper.GetComponent<Grid_Script>().disableGrid();
        }


        unlocking = false;
    }
    private void lockObject(GameObject objectToLock, RaycastHit hit) {
        //If moving an object from a source block
        if(objectToLock.GetComponent<Grid_Object>() == null && objectToLock.GetComponent<Pickable_Object>().available() == true) { 
            objectToLock.GetComponent<Pickable_Object>().changeQuantity(-1);
            lockedObject = Instantiate(objectToLock, objectToLock.transform.position, objectToLock.transform.rotation);
            lockedObject.GetComponent<Pickable_Object>().setSource(false);
            lockedObject.GetComponent<Pickable_Object>().setOriginalSource(objectToLock);
        }
        //if moving an already moved block
        else if (objectToLock.GetComponent<Grid_Object>().getObject(false) != null) {
            lockedObject = objectToLock.GetComponent<Grid_Object>().getObject(true);
            //removes the refernce of the block being moved from the grid object it was attached to

        }
        //This is a fix. It makes sure an object can be picked up even if the player tried to pick up an object with 0 quantity 
        if (lockedObject != null)
        {

        }

    }
    private void unlockObject(GameObject gridSquare) {
        //setting the contained object of that grid sqare to the object being dropped into place
        gridSquare.GetComponent<Grid_Object>().setObject(lockedObject);
        lockedObject = null;
        unlocking = true;
    }
    private void mouseUpdate() {
        //If an block is locked and is being moved by the mouse
        if (lockedObject != null) {
            RaycastHit hit;
            Ray newMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(newMouseRay, out hit)) {
                //if the raycast hits a section of the grid
               if(hit.transform.gameObject.GetComponent<Grid_Object>() != null) {
                    lockedObject.transform.position = new Vector3(hit.transform.position.x, .5f, hit.transform.position.z);
                    //Adds the block to the grid block
                    //hit.transform.gameObject.GetComponent<Grid_Object>().setObject(lockedObject);
               }
                
            }
            if (Input.GetMouseButtonDown(0)) {
                //checking if the grid sqaure already contains an object
                if (!hit.transform.gameObject.GetComponent<Grid_Object>().containsObject()) {
                    Debug.Log("Unlock");
                    unlockObject(hit.transform.gameObject);
                }
            }



           
        }
    }
    private void rotateUpdate() {
        if (lockedObject != null) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                lockedObject.GetComponent<Pickable_Object>().rotateObject(false);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                lockedObject.GetComponent<Pickable_Object>().rotateObject(true);
            }
        }
    }
    
}
