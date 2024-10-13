using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch1 : MonoBehaviour
{
    public int SceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D other) {
        print("trigger Entered");
        //occhio alle maiuscole camelCase
        if(other.tag == "wari_true (2)") {
            print("switching scene to " + SceneBuildIndex);
            SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
