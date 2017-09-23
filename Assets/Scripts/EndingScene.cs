using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // UpdateScore.text = "Score: " + PlayerController.score;
	}

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    private void Restart()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
