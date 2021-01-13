using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SaveObject
{

    public string orientation;
    public float[] pos;
    public string componentType = "nub";
   

    public SaveObject(Vector2 pos, string componentType,string orientation) {
        this.pos = new float[] {pos.x,pos.y};
        this.componentType = componentType;
        this.orientation = orientation;
    }
    public Vector2 getPos() {
        return new Vector2(pos[0],pos[1]);
    }
    public string getType() {
        return componentType;
    }
    public void setOrientation(string orientation) {
        this.orientation = orientation;
    }
    public string getOrientation() {
        return orientation;
    }
}
[Serializable]
public class CustomSaveArray
{
    public SaveObject[] saveArray = new SaveObject[0];

    public void add(SaveObject toAdd) {
        Array.Resize(ref saveArray, saveArray.Length + 1);
        saveArray[saveArray.Length - 1] = toAdd;
    }
    public SaveObject[] getList() {
        return saveArray;

    }
    
}
public class Save_Load_Ship : MonoBehaviour
{

    public GameObject gridHelper;


    public void saveShip(string saveName) {
        //Debug.Log(test.getList()[0].getType());
        //Debug.Log(JsonUtility.ToJson(test));


        CustomSaveArray saveArray = new CustomSaveArray();
        GameObject[,] gridArray = gridHelper.GetComponent<Grid_Script>().getGridArray();

        for(int i=0; i < gridArray.GetLength(0); i++) {
            for(int j = 0; j < gridArray.GetLength(1); j++) {
                if (gridArray[i, j].GetComponent<Grid_Object>().containsObject()) {
                    Grid_Object currentGridObject = gridArray[i, j].GetComponent<Grid_Object>();
                    saveArray.add(new SaveObject(new Vector2(currentGridObject.transform.position.x, currentGridObject.transform.position.z), currentGridObject.getObject().GetComponent<Pickable_Object>().getType(),currentGridObject.getObject().GetComponent<Pickable_Object>().getOrientation()));
                    
                }
                else {
                    Debug.Log("Doesn't contain");
                }
            }
        }
            string jsonObject = JsonUtility.ToJson(saveArray);
            Debug.Log(jsonObject);


        /*        for(int i = 0; i < gridX; i++) {
                    for (int j = 0; j < gridZ; j++) {
                        gridArray[i,j].GetComponent<BoxCollider>().enabled = false;
                        gridArray[i, j].GetComponent<Renderer>().enabled = false;
                        Debug.Log("Disable");
                    }
                }*/

        //Saving the ship file
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+saveName+".jpeg";

        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log(Application.persistentDataPath);
        formatter.Serialize(stream, saveArray);
        stream.Close();

    }

    public void loadShip(string saveName) {
        string path = Application.persistentDataPath + "/" + saveName + ".jpeg";
        Debug.Log(path);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CustomSaveArray ship = formatter.Deserialize(stream) as CustomSaveArray;
            stream.Close();


            Debug.Log(ship);
            string jsonObject = JsonUtility.ToJson(ship);
            Debug.Log(jsonObject);

            //putting the ship into the game as a real life thingy
            GameObject[,] gridArray = gridHelper.GetComponent<Grid_Script>().getGridArray();

            GameObject tempObject;
            foreach (SaveObject component in ship.getList()) {
                tempObject = Instantiate(GameObject.Find(component.getType()), new Vector3(component.getPos().x, .5f, component.getPos().y), GameObject.Find(component.getType()).transform.rotation);
                tempObject.GetComponent<Pickable_Object>().setOriginalSource(GameObject.Find(component.getType()));
                GameObject.Find(component.getType()).GetComponent<Pickable_Object>().changeQuantity(-1); //changing the quantity of the source block

                //rotating the block so it was in the orientation it was saved in (Yes, this is kinda messy but it would be either way)
                if(component.getOrientation() == "down") {
                    tempObject.GetComponent<Pickable_Object>().rotateObject(true);
                    tempObject.GetComponent<Pickable_Object>().rotateObject(true);
                }
                else if (component.getOrientation() == "right") {
                    tempObject.GetComponent<Pickable_Object>().rotateObject(true);
                }
                else if (component.getOrientation() == "left") {
                    tempObject.GetComponent<Pickable_Object>().rotateObject(false);
                }

                //adding that object to a grid object (I know this is an shit way to do it, but whatever)
                foreach (GameObject gridGameObject in gridArray) {
                    Grid_Object gridObject = gridGameObject.GetComponent<Grid_Object>();

                    if (gridObject.getX() ==(int) component.getPos().x && gridObject.getY() == (int)component.getPos().y) {
                        Debug.Log(gridObject.getX().ToString() + component.getPos().x.ToString() + gridObject.getY().ToString() + component.getPos().y.ToString());
                        gridObject.setObject(tempObject);
                        Debug.Log("Adding to this grid object");
                    }
                }
            }





        }
        else {
            Debug.LogError("File not found: "+Application.persistentDataPath);
        }
    }

}
