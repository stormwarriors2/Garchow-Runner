using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLife : MonoBehaviour
{
    TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

        text.text = PlayerController.life.ToString();
    }
}
