using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject ground;
    public Transform player;
    public GameObject powerUp;
    public GameObject wall;


    List<GameObject> chunks = new List<GameObject>();
    List<GameObject> ups = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> powerups = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
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
    /// </summary>
    private void chunkCreator()
    {
        var random = Random.Range(1, 6);
        var spawn = Random.Range(1, 10);
        Remove();
        while (chunks.Count < 6)
        {
            Vector3 position = Vector3.zero;
            Vector3 wallPosition = new Vector3(0, 0, 0);
            Vector3 upPosition = new Vector3(0, -1, 0);
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
                if (spawn == 10) print("hello");
            }

            GameObject newObj = Instantiate(powerUp, wallPosition, Quaternion.identity);
            GameObject Wallobj = Instantiate(wall, upPosition, Quaternion.identity);
            GameObject obj = Instantiate(ground, position, Quaternion.identity);

            chunks.Add(obj);
            walls.Add(Wallobj);
            powerups.Add(newObj);

            AABB groundAABB = obj.GetComponent<AABB>();
            AABB wallAABB = Wallobj.GetComponent<AABB>();
            AABB powerAABB = newObj.GetComponent<AABB>();

            CollisionManager.walls.Add(wallAABB);
            CollisionManager.powerups.Add(powerAABB);
            CollisionManager.groundTiles.Add(groundAABB);

        }
    }

    /// <summary>
    /// This is the controller to check to see if the player has lost all its life
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
    private void Remove()
    {
        if (chunks.Count > 0)
        {
            if (player.position.x - chunks[0].transform.position.x > 25)
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
            if (player.position.x - walls[0].transform.position.x > 25)
            {
                Destroy(walls[0]);
                walls.RemoveAt(0);
                CollisionManager.walls.RemoveAt(0);
            }
    }

    private void ObstacleSpawn()
   {
        
        var random = Random.Range(1, 10);
        // Debug.Log(random);
        //var random = 4; 

        while (chunks.Count < 5)
        {
            Vector3 position = new Vector3(0, -1, 0);
            Vector3 otherPosition = new Vector3(0, 0, 0);
            if (chunks.Count > 0)
            {
              //  position = chunks[chunks.Count - 1].transform.Find("Connector").position;
                if (random == 1) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint01").position;
                if (random == 2) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint02").position;
                if (random == 3) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint03").position;
                if (random == 4) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint04").position;
                if (random == 5) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint05").position;
                if (random == 6) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint06").position;
                if (random == 7) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint07").position;
                if (random == 8) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint08").position;
                if (random == 9) otherPosition = chunks[chunks.Count - 1].transform.Find("SpawnPoint09").position;
                
            }
            GameObject obj = Instantiate(wall, position, Quaternion.identity);
       //     GameObject newObj = Instantiate(powerUp, otherPosition, Quaternion.identity);


         //   chunks.Add(obj);
          //  ups.Add(newObj);
            // Debug.Log(chunks);
        }
    }
}
