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
        GroundBehavior();
    }

    /// <summary>
    /// Ground Behavior
    /// Creates ground and deletes it after player has moved x distance away from it so it is constantly spawning new platforms far infront of the player
    /// 
    /// </summary>
    private void GroundBehavior()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + .1f, transform.localPosition.y, transform.localPosition.z);

        if (transform.localPosition.x >= 21 && spawnNew == false)
        {
            Instantiate(prefabFloor, new Vector3(-10, -1, 0), Quaternion.identity);
            spawnNew = true;
        }
        if (transform.localPosition.x >= 21)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// WallBehavior
    /// creates wall and then destroys game object after certain distance from player
    /// </summary>
    private void WallBehavior()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + .1f, transform.localPosition.y, transform.localPosition.z);

        if (transform.localPosition.x >= 21 && spawnNew == false)
        {
            Instantiate(prefabFloor, new Vector3(-10, -1, 0), Quaternion.identity);
            spawnNew = true;
        }
        if (transform.localPosition.x >= 21)
        {
            Destroy(this.gameObject);
        }
    }
}
