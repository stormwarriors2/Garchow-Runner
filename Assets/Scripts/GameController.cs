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
    void Start () {
		
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
      //  ObstacleSpawn();
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
        var random = Random.Range(1, 6);
      
      
        Remove();
        while (chunks.Count < 6)
        {
            Vector3 position = Vector3.zero;
            Vector3 wallPosition = new Vector3(0, 0, 0);
  
          
            if (chunks.Count > 0)
            {
                position = chunks[chunks.Count - 1].transform.Find("Connecter").position;
                if (random == 1) wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint01").position;
                if (random == 2) wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint02").position;
                if (random == 3) wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint03").position;
                if (random == 4) wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint04").position;
                if (random == 5) wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint05").position;
                if (random == 6) wallPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint06").position;
            }


            SpawnPowerUp();
            SpawnSpikes();
            SpawnLava();
        
            GameObject Wallobj = Instantiate(wall, wallPosition, Quaternion.identity);
            GameObject obj = Instantiate(ground, position, Quaternion.identity);

            chunks.Add(obj);
            walls.Add(Wallobj);

          

            AABB groundAABB = obj.GetComponent<AABB>();
            AABB wallAABB = Wallobj.GetComponent<AABB>();



            CollisionManager.walls.Add(wallAABB);

            CollisionManager.groundTiles.Add(groundAABB);


        }
    }

    /// <summary>
    /// Spawns PowerUp 
    /// creates random number for where a spike needs to PowerUp.
    /// Adds it to the collision manager, array, and to the exact position of the spawn point
    /// </summary>
    private void SpawnPowerUp()
    {
        var spawn = Random.Range(1, 9);
        Vector3 upPosition = new Vector3(0, 0, 0);
        if (powerups.Count > 0)
        {
            //position = chunks[chunks.Count - 1].transform.Find("Connecter").position;
            if (spawn == 1) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint01").position;
            if (spawn == 2) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint02").position;
            if (spawn == 3) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint03").position;
            if (spawn == 4) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint04").position;
            if (spawn == 5) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint05").position;
            if (spawn == 6) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint06").position;
            if (spawn == 7) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint07").position;
            if (spawn == 8) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint08").position;
            if (spawn == 9) upPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint09").position;
        }
        GameObject newObj = Instantiate(powerUp, upPosition, Quaternion.identity);
        powerups.Add(newObj);
        AABB powerAABB = newObj.GetComponent<AABB>();
        CollisionManager.powerups.Add(powerAABB);

    }
    /// <summary>
    /// Spawns lava 
    /// creates random number for where a lava needs to spawn.
    /// Adds it to the collision manager, array, and to the exact position of the spawn point
    /// </summary>
    private void SpawnLava()
    {
        var rand = Random.Range(1, 6);
        Vector3 lvPosition = new Vector3(0, 1, 0);
        if (lavas.Count > 0)
        {
            //position = chunks[chunks.Count - 1].transform.Find("Connecter").position;
            if (rand == 1) lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint01").position;
            if (rand == 2) lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint02").position;
            if (rand == 3) lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint03").position;
            if (rand == 4) lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint07").position;
            if (rand == 5) lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint08").position;
            if (rand == 6) lvPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint09").position;
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
    private void SpawnSpikes()
    {
        var ofran = Random.Range(1, 3);
        Vector3 ranP = new Vector3(0, 1, 0);
        if (spikes.Count > 0)
        {
            //position = chunks[chunks.Count - 1].transform.Find("Connecter").position;
            if (ofran == 1) ranP = chunks[chunks.Count - 1].transform.Find("SpawnPoint10").position;
            if (ofran == 2) ranP = chunks[chunks.Count - 1].transform.Find("SpawnPoint11").position;
            if (ofran == 3) ranP = chunks[chunks.Count - 1].transform.Find("SpawnPoint12").position;
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
        else
        {
            print("your fine");
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
        if (chunks.Count > 0)
        {
            if (player.position.x - chunks[0].transform.position.x > 35)
            {
                Destroy(chunks[0]);
                chunks.RemoveAt(0);
                CollisionManager.groundTiles.RemoveAt(0);
            }
        }
        if (powerups.Count > 0 )
        {
            if (player.position.x - powerups[0].transform.position.x > 25)
            {
                Destroy(powerups[0]);
                powerups.RemoveAt(0);
                CollisionManager.powerups.RemoveAt(0);
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
