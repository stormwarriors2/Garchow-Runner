using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{


    /// <summary>
    /// Update
    /// Calls every second the GameBegin Method
    /// </summary>
    void Update()
    {
        GameBegin();
    }

    /// <summary>
    /// GameBegin
    /// Asks for certain button ENTER)
    /// Loads GameBegin Scene
    /// </summary>
    private void GameBegin()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
