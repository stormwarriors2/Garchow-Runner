﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {
    #region Variables
    AABB player;

    static public List<AABB> groundTiles = new List<AABB>();
    static public List<AABB> powerups = new List<AABB>();
    static public List<AABB> walls = new List<AABB>();
    static public List<AABB> spikes = new List<AABB>();
    static public List<AABB> lavaground = new List<AABB>();
    public AudioClip lose;
    public AudioClip coin;
    public AudioClip upone;
    public AudioClip wallhit;

#endregion
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<AABB>();
        groundTiles.Clear();
        powerups.Clear();
        walls.Clear();
        spikes.Clear();
        lavaground.Clear();
        //     walled = GameObject.Find("Wall01").GetComponent<AABB>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //print(wall);
        DoCollisionDetectionGround();
        DoCollisionDetectionPowerup();
        if (!PlayerController.isGod)
        {
            DoCollisionDetectionWall();
            DoCollisionDetectionLava();
            DoCollisionDetectionSpike();
        }
    }
    #region Collision Detection Game Objects
    /// Ground
    /// Lava
    /// Spike
    /// PowerUp
    /// Wall

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
                if (wall != null) {
                    AudioSource.PlayClipAtPoint(wallhit, transform.position);
                    Destroy(wall.gameObject);
                walls.Remove(wall);
                PlayerController.life -= 1;
                GetComponent<GameController>().walls.Remove(wall.gameObject);
                return;
                    }
             //   print("collider");
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
                if (powerup != null)
                {

                    powerup.GetComponent<Powerup>().obtainPowerup();
                    AudioSource.PlayClipAtPoint(coin, transform.position);
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
    }
    /// <summary>
    /// Collision Detection - Lava
    /// Detects collision between player and powerup
    /// Object does many things
    /// Is destroyed on touch
    /// Minuses life from playercontroller
    /// is then removed
    /// </summary>
    void DoCollisionDetectionLava()
    {
        foreach (AABB lava in lavaground)
        {
            bool resultLava = player.checkOverlap(lava);
            //print(resultWall);
            if (resultLava == true)
            {
                if (lava != null)
                {
                    AudioSource.PlayClipAtPoint(lose, transform.position);
                    PlayerController.life -= 2;
                    Destroy(lava.gameObject);
                    lavaground.Remove(lava);
                    GetComponent<GameController>().lavas.Remove(lava.gameObject);
                    return;
                }
                //   print("collider");
            }
            
        }
    }
    /// <summary>
    /// Collision Detection - Spike
    /// Detects collision between player and powerup
    /// Object does many things
    /// Is destroyed on touch
    /// Deals 1 damage to player
    /// Removes 200 points
    /// 
    /// </summary>
    void DoCollisionDetectionSpike()
    {

        foreach (AABB spike in spikes)
        {
            if (spike != null)
            {
               
                bool resultWall = player.checkOverlap(spike);
            //print(resultWall);
            if (resultWall == true)
            {
                    AudioSource.PlayClipAtPoint(wallhit, transform.position);
                    PlayerController.life -= 1;
                    PlayerController.score -= 200;
                Destroy(spike.gameObject);
                spikes.Remove(spike);
                GetComponent<GameController>().spikes.Remove(spike.gameObject);
                return;
                //   print("collider");
                }
            }
            else
            {
            }
        }
    }
#endregion
}
