using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateInvulnerable : MonoBehaviour {

    TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isGod == true)
        {
            text.text = "On";
        }
        else
        {
            text.text = "off";

        }
        //text.text = PlayerController.isGod.ToString();
    }
}
