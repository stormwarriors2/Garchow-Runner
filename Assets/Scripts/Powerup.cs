using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    PlayerController player;
    public bool isLife = false;
    public bool isGodPowerup = false;
    public bool isResetPowerup = false;
    public bool canRemove = false;

    // Use this for initialization
    void Start()
    {
        int powerPick = Random.Range(1, 7);
        switch (powerPick)
        {
            case 1:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.red;
                return;
                break;
            case 2:
                PlayerController.score += 1000;
                GetComponent<MeshRenderer>().material.color = Color.magenta;
                return;
                break;
            case 3:
                PlayerController.score += 3000;
                GetComponent<MeshRenderer>().material.color = Color.cyan;
                return;
                break;
            case 4:
                PlayerController.score -= 1000;
                GetComponent<MeshRenderer>().material.color = Color.black;
                return;
                break;
            case 5:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                return;
                break;
            case 6:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material.color = Color.green;
                return;
                break;
            case 7:
                isGodPowerup = true;
                GetComponent<MeshRenderer>().material.color = Color.gray;
                return;
                break;
            default:
                break;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    void resetPlayerMovement()
    {
        if(player.addedMoveDelay > 0)
        {
            player.addedMoveDelay = 0;
        }
        canRemove = true;
    }

    public void obtainPowerup()
    {
        player = FindObjectOfType<PlayerController>();
        //print("PICK A POWERUP");

        if (isLife == true)
        {
            extraLife();
        }
        else if (isGodPowerup == true)
        {
            godMode();
        }
        else if (isResetPowerup == true)
        {
          //  resetPlayerMovement();
        }

        canRemove = true;
    }

    void resetPlayer()
    {
        print("RESET MOVE!");
        if (player.addedMoveDelay > 0)
        {
            player.addedMoveDelay = 0;
            player.multiplier = 1;
        }
        canRemove = true;
    }

    void extraLife()
    {
        print("Gain LIFE!");
        if (PlayerController.life < 3)
        {
            PlayerController.life += 1;
        }
        canRemove = true;
    }

    void godMode()
    {
        print("ARE GOD!");
        PlayerController.isGod = true;
        player.godTimer = 1;
        canRemove = true;
    }
}
