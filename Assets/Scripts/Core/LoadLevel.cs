using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void loadLevel(string levelName) {
        this.GetComponent<Save_Load_Ship>().saveShip("temp");
        SceneManager.LoadScene(levelName);
    }
}
