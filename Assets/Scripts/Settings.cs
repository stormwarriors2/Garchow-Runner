using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings {

    //public int score = 0;

    public bool achievementDied = false;
    //only exists in the class not as instances

    static public GameObject player;

    public static Settings main = new Settings();

    private Settings()
    {

    }
}
