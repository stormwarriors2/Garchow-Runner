﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    AABB player;

    static public List<AABB> groundTiles = new List<AABB>();
    static public List<AABB> powerups = new List<AABB>();
    static public List<AABB> walls = new List<AABB>();



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


    }

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

}
