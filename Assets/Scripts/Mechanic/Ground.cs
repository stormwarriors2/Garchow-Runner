using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ground
/// obtains powerup 
/// obtains wall 
/// </summary>

public class Ground : MonoBehaviour {

    public GameObject prefabFloor;
    public GameObject prefabWall01;
    public GameObject prefabWall02;
    public GameObject prefabWall03;
    public GameObject prefabPowerUp01;
    public GameObject prefabPowerUp02;
    public GameObject prefabPowerUp03;
    private bool spawnNew = false;

	/// <summary>
    /// Update
    /// Updates the ground to spawn and transform its position overtime
    /// </summary>
	void Update ()
    {
        //GroundBehavior();
    }

}
