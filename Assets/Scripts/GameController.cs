using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This is the Game Controller
/// Game Objs
///     ground
///     player
///     powerUps
///     Lava
///     Walls
///     Spikes
/// These are placed into arrays where they are stored as information.
/// This controller both removes and adds objects to the scene for the player to interact with
/// This was designed based on Nick Pattison's version but using heavily modified
/// </summary>
public class GameController : MonoBehaviour {

    public GameObject ground;
    public Transform player;
    public GameObject powerUp;
    public GameObject wall;
    public GameObject lavaO;
    public GameObject spike;


    List<GameObject> chunks = new List<GameObject>();
    List<GameObject> ups = new List<GameObject>();
    public List<GameObject> lavas = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> powerups = new List<GameObject>();
    public List<GameObject> spikes = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        CleanUp();
        
    }
    /// <summary>
    /// CleanUp
    /// Clears all objects from the list and array.
    /// </summary>
    private void CleanUp()
    {
        chunks.Clear();
        walls.Clear();
        lavas.Clear();
        powerups.Clear();
        spikes.Clear();
        PlayerController.score = 0;
    }

    /// <summary>
    /// Update
    /// Updates all objects within the game.
    /// </summary>
    // Update is called once per frame
    void Update ()
    {
        chunkCreator();
        Lose();
        Remove();
    }


    /// <summary>
    /// Chunk Creator
    /// creates all parts of the chunks and walls and obstacles of the game.
    /// var random controls the position of the wall objects
    /// var spawn controls the position of the power up objects
    /// Remove() refers to the removal objects
    /// contains #random - controls first 6 objects
    /// contains #spawn - controls powerup spawns
    /// contains #rand - controls spawning of lava
    /// SpawnPowerUp - creates PowerUps
    /// SpawnSpikes - creates Spikes
    /// SpawnLava - Creates Lava
    /// Walls and ground are tied to the same positioning this is to prevent a potential bug. 
    /// 
    /// </summary>
    private void chunkCreator()
    {

      
      

        while (chunks.Count <= 7)
        {
            Vector3 position = Vector3.zero;


            if(chunks.Count > 0)
            {
            //    position = chunks[chunks.Count - 1].transform.Find("Connector").position;
            }

            // for(int i = 1; i < 9; i++) { }
            Vector3 wallPosition = new Vector3(0, 0, 0);
            for (int i = 1; i < 12; i++)
            {
         
                if (chunks.Count > 0)
                {
                    position = chunks[chunks.Count - 1].transform.Find("Connecter").position;
                }
                GameObject obj = Instantiate(ground, position, Quaternion.identity);
                chunks.Add(obj);
                AABB groundAABB = obj.GetComponent<AABB>();
                CollisionManager.groundTiles.Add(groundAABB);

                WallSpawn(i);
                SpawnPowerUp();
                SpawnSpikes(i);
                SpawnLava(i);




            }
        }
    }

    private Vector3 WallSpawn(int i)
    {
        Vector3 wallPosition = new Vector3(0, 0, 0);
        if (walls.Count > 0 && walls.Count < 6 && walls.Count < 8 && walls.Count < 12)
        {
            wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint0" + i.ToString()).position;
        }
        GameObject Wallobj = Instantiate(wall, wallPosition, Quaternion.identity);
        walls.Add(Wallobj);
        AABB wallAABB = Wallobj.GetComponent<AABB>();
        CollisionManager.walls.Add(wallAABB);
        return wallPosition;
    }

    /// <summary>
    /// Spawns PowerUp 
    /// creates random number for where a spike needs to PowerUp.
    /// Adds it to the collision manager, array, and to the exact position of the spawn point
    /// </summary>
    private void SpawnPowerUp()
    {
        for (int i = 1; i < 8; i++)
        {
            Vector3 upPosition = new Vector3(0, 2, 0);
            if (powerups.Count > 3 && powerups.Count <= 8)
            {
                upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint0" + i.ToString()).position;
            }
            GameObject newObj = Instantiate(powerUp, upPosition, Quaternion.identity);
            powerups.Add(newObj);
            AABB powerAABB = newObj.GetComponent<AABB>();
            CollisionManager.powerups.Add(powerAABB);
        }

    }
    /// <summary>
    /// Spawns lava 
    /// creates random number for where a lava needs to spawn.
    /// Adds it to the collision manager, array, and to the exact position of the spawn point
    /// </summary>
    private void SpawnLava(int i)
    {
        Vector3 lvPosition = new Vector3(0, 2, 0);
        if (lavas.Count > 4 && lavas.Count <= 7)
        {
            lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint0" + i.ToString()).position;
        }
        GameObject lavaobj = Instantiate(lavaO, lvPosition, Quaternion.identity);
        AABB lavaAABB = lavaobj.GetComponent<AABB>();
        CollisionManager.lavaground.Add(lavaAABB);
        lavas.Add(lavaobj);
    }
    /// <summary>
    /// Spawns Spikes 
    /// creates random number for where a spike needs to spawn.
    /// Adds it to the collision manager, array, and to the exact position of the spawn point
    /// </summary>
    private void SpawnSpikes(int i)
    {
        Vector3 ranP = new Vector3(0, 2, 0);
        if (spikes.Count > 0 && spikes.Count < 2)
        {
            ranP = chunks[chunks.Count - 1].transform.Find("SpawnPoint0" + i.ToString()).position;
        }
        GameObject obj = Instantiate(spike, ranP, Quaternion.identity);
        spikes.Add(obj);
        AABB spikeAABB = obj.GetComponent<AABB>();
        CollisionManager.spikes.Add(spikeAABB);
    }
    /// <summary>
    /// Lose
    /// This is the controller to check to see if the player has lost all its life
    /// If player has lost all health, 
    /// Loads Game Over Screen
    /// </summary>
    private void Lose()
    {
        if (PlayerController.life < 0)
        {
            print("I HAVE LOST");
            SceneManager.LoadScene("GameOver");
            //game over
        }
      

    }

    /// <summary>
    /// Remove
    /// Removes Chunks from array, and from scene
    /// -Checks to see if there is greater Chunks if so, remove and destroy if certain distance from player
    /// Removes Powerups from array, and from scene
    /// -Checks to see if there is greater Powerups if so, remove and destroy if certain distance from player
    /// Removes Walls from array, and from scene
    /// -Checks to see if there is greater Walls if so, remove and destroy if certain distance from player
    /// Removes lava from array, and from scene
    /// -Checks to see if there is greater Lava if so, remove and destroy if certain distance from player
    /// Removes spikes from array, and from scene
    /// -Checks to see if there is greater Spikes if so, remove and destroy if certain distance from player
    /// </summary>
    private void Remove()
    {
        if (powerups.Count > 0)
        {
            if (player.position.x - powerups[0].transform.position.x > 25)
            {
                Destroy(powerups[0]);
                powerups.RemoveAt(0);
                CollisionManager.powerups.RemoveAt(0);
            }
        }
        if (chunks.Count > 0)
        {
            if (player.position.x - chunks[0].transform.position.x > 35)
            {
                Destroy(chunks[0]);
                chunks.RemoveAt(0);
                CollisionManager.groundTiles.RemoveAt(0);
            }
        }

        if (walls.Count > 0)
        {
            if (player.position.x - walls[0].transform.position.x > 25)
            {
                Destroy(walls[0]);
                walls.RemoveAt(0);
                CollisionManager.walls.RemoveAt(0);
            }
        }
        if (lavas.Count > 0)
        {
            if (player.position.x - lavas[0].transform.position.x > 25)
            {
                Destroy(lavas[0]);
                lavas.RemoveAt(0);
                CollisionManager.lavaground.RemoveAt(0);
            }
        }
        if (spikes.Count > 0)
        {
            if (player.position.x - spikes[0].transform.position.x > 15)
            {
                Destroy(spikes[0]);
                spikes.RemoveAt(0);
                CollisionManager.spikes.RemoveAt(0);
            }
        }
    }
}
