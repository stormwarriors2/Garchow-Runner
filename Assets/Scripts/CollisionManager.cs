using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    AABB player;

    static public List<AABB> groundTiles = new List<AABB>();
    static public List<AABB> powerups = new List<AABB>();
    static public List<AABB> walls = new List<AABB>();
    static public List<AABB> spikes = new List<AABB>();
    static public List<AABB> lavaground = new List<AABB>();


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<AABB>();
   //     walled = GameObject.Find("Wall01").GetComponent<AABB>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //print(wall);
        DoCollisionDetectionGround();
        DoCollisionDetectionWall();
            DoCollisionDetectionPowerup();
            DoCollisionDetectionLava();
            DoCollisionDetectionSpike();

    }
    /// <summary>
    /// Collision Detection - Ground
    /// Detects collision between player and ground
    /// Fixes player location to always stay ontop of the ground
    /// Gravity is stopped for the player allowing for the player to jump
    /// </summary>
    void DoCollisionDetectionGround()
    {

        foreach (AABB ground in groundTiles)
        {

            bool resultGround = player.checkOverlap(ground);
            //print(resultGround);
            if(resultGround == true)
            {
                //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5 * Time.deltaTime, player.transform.position.z);
                //player.GetComponent<PlayerController>().stopGravity = true;
                //player.GetComponent<MeshRenderer>().material.color = Color.black;
                Vector3 fix = player.CalculateOverlapFix(ground);
            //    print(fix);
                player.GetComponent<PlayerController>().ApplyFix(fix);

                return;
            }
            else
            {
                player.GetComponent<PlayerController>().stopGravity = false;
                //player.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }



        
    }

    /// <summary>
    /// Collision Detection - Wall
    /// Detects collision between player and wall
    /// object is removed and player takes damage.
    /// Player is minused their life
    /// </summary>
    void DoCollisionDetectionWall()
    {
        foreach (AABB wall in walls) { 
        bool resultWall = player.checkOverlap(wall);
        //print(resultWall);
        if (resultWall == true)
        {
                PlayerController.life -= 1;
                Destroy(wall.gameObject);
                walls.Remove(wall);
                GetComponent<GameController>().walls.Remove(wall.gameObject);
                return;
             //   print("collider");
        }
        else
            {

                print("no collision!");
            }
    }
    }

    /// <summary>
    /// Collision Detection - powerup
    /// Detects collision between player and powerup
    /// Object does many things
    /// Is destroyed on touch
    /// 
    /// </summary>
    void DoCollisionDetectionPowerup()
    {
        foreach (AABB powerup in powerups)
        {
            bool resultPowerup = player.checkOverlap(powerup);

            if (resultPowerup == true)
            {
                powerup.GetComponent<Powerup>().obtainPowerup();
                if (powerup.GetComponent<Powerup>().canRemove == true)
                {
                    Destroy(powerup.gameObject);
                    powerups.Remove(powerup);
                    GetComponent<GameController>().powerups.Remove(powerup.gameObject);
                    return;
                }
            }
        }
    }
    void DoCollisionDetectionLava()
    {
        foreach (AABB lava in lavaground)
        {
            bool resultWall = player.checkOverlap(lava);
            //print(resultWall);
            if (resultWall == true)
            {
                PlayerController.life -= 2;
                Destroy(lava.gameObject);
                lavaground.Remove(lava);
                GetComponent<GameController>().lavas.Remove(lava.gameObject);
                return;
                //   print("collider");
            }
            else
            {

                print("no collision!");
            }
        }
    }
    void DoCollisionDetectionSpike()
    {
        foreach (AABB spike in spikes)
        {
            bool resultWall = player.checkOverlap(spike);
            //print(resultWall);
            if (resultWall == true)
            {

                PlayerController.life -= 1;
              //  PlayerController.life -= 2;
                Destroy(spike.gameObject);
                spikes.Remove(spike);
                GetComponent<GameController>().spikes.Remove(spike.gameObject);
                return;
                //   print("collider");
            }
            else
            {

                print("no collision!");
            }
        }
    }
}
