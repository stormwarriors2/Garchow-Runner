using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour {

    // Update is called once per frame
    /// <summary>
    /// Update
    /// Calls every second teh Restart Method
    /// </summary>
    void Update()
    {
        Restart();
    }

    /// <summary>
    /// Restart
    /// Asks for certain button ENTER)
    /// Loads MainMenu Scene
    /// </summary>
    private void Restart()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
