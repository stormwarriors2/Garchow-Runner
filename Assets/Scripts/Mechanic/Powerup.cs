using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// PowerUp Controller
/// Bool : Life controls player life
/// Bool : IsGodPowerUp checks to see if god mode is created - used for testing not obtainable in game
/// Bool : canRemove checks to see if the powerup is removed
/// </summary>
public class Powerup : MonoBehaviour {

    PlayerController player;
    public bool isLife = false;
    public bool isGodPowerup = false;
    public bool isResetPowerup = false;
    public bool canRemove = false;

    // Use this for initialization
    /// <summary>
    /// Initialization Case Switch statement
    /// Is changed randomly by using PowerPick
    /// Players score is increase or decreased depending on the powerup attained
    /// Player can also lose score if they touch black powerup
    /// </summary>
    void Start()
    {
        int powerPick = Random.Range(1, 7);
        switch (powerPick)
        {
            case 1:
                PlayerController.score += 1500;
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.red;
                return;
            case 2:
                PlayerController.score += 100;
                GetComponent<MeshRenderer>().material.color = Color.magenta;
                return;
            case 3:
                PlayerController.score += 200;
                GetComponent<MeshRenderer>().material.color = Color.cyan;
                return;
            case 4:
                PlayerController.score -= 300;
                GetComponent<MeshRenderer>().material.color = Color.black;
                return;
            case 5:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                return;
            case 6:
                PlayerController.score -= 25;
                GetComponent<MeshRenderer>().material.color = Color.green;
                return;
            case 7:
                PlayerController.isGod = true;
                GetComponent<MeshRenderer>().material.color = Color.yellow;
                return;
            default:
                break;
        }
    }

    /// <summary>
    /// Resets Player Movement
    /// player movement delay is added,
    /// </summary>
    void resetPlayerMovement()
    {
        if(player.addedMoveDelay > 0)
        {
            player.addedMoveDelay = 0;
        }
        canRemove = true;
    }
    /// <summary>
    /// ObtainPowerUp
    /// checks to see what type of powerup was attained
    /// If bool is true applies powerup
    /// </summary>
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

    /// <summary>
    /// Resets Player
    /// player movement and multipler of speed is increased by 1
    /// </summary>
    void resetPlayer()
    {
        print("This aint free");
        if (player.addedMoveDelay > 0)
        {
            player.addedMoveDelay = 0;
            player.multiplier = 1;
        }
        canRemove = true;
    }

    /// <summary>
    /// extraLife
    /// Player gains an extra life
    /// </summary>
    void extraLife()
    {

        print("You've Gained Life");
        if (PlayerController.life < 3)
        {
            PlayerController.life += 1;
        }
        canRemove = true;
    }
    /// <summary>
    /// GodMode
    /// A test mod
    /// </summary>
    void godMode()
    {
        print("If someone asks you if your god, SAY YES!");
        PlayerController.isGod = true;
        player.godTimer = 1;
        canRemove = true;
    }
}
