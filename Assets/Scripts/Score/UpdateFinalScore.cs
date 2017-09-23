using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFinalScore : MonoBehaviour {
    TextMesh text;
    // Use this for initialization
    void Start () {
        text = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.life <= 0)
        {
            text.text = PlayerController.score.ToString();
            return;
        }
    }
}
