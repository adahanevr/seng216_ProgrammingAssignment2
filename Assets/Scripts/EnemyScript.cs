using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // array of zombie prefabs (same as in GameController.cs)
    public float spawnInterval = 3f; // time interval between the creation of enemy zombies
    private int _selectedCharacterIndex; // index number of selected zombie type
    public GameObject enemy;
    private Animator _enemyAnimator;

    
    void Start()
    {
        // Get the index of the selected character (stored from previous selection)
        _selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        StartCoroutine(SpawnZombies()); // start a coroutine to spawn zombies
    }
    
    IEnumerator SpawnZombies()
    {
        // continuously spawn zombies at set intervals
        while (true)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval); // wait for the set interval before spawning another zombie
        }
    }

    void SpawnZombie()
    {
        int zombieIndex = GetRandomZombieIndex(); // get a random zombie type (other than the selected one)
        // set a random x position for the enemy zombie to spawn
        float randomX = Random.Range(5, 7.403f); // (>=5 so that it doesnt spawn too close to the player)
        Vector2 randomPosition = new Vector2(randomX, -2.7f);

        enemy = Instantiate(zombiePrefabs[zombieIndex], randomPosition, Quaternion.identity); // instantiate the zombie at the random position
        enemy.transform.localScale = new Vector3(-1.8f, 1.8f, 1); // scale the enemy zombie so that it faces to left
        _enemyAnimator = enemy.GetComponent<Animator>();
        _enemyAnimator.SetBool("isRunning", true);

        ZombieMovement zombieMovement = enemy.AddComponent<ZombieMovement>(); // movement of the enemy zombie
        zombieMovement.speed = 3f; // speed of the enemy zombie
    }

    int GetRandomZombieIndex()
    {
        // ensure the random zombie index is different from the selected character index
        int zombieIndex;
        do
        {
            zombieIndex = Random.Range(0, zombiePrefabs.Length);
        } while (zombieIndex == _selectedCharacterIndex); // if it's the same, try until getting a different zombie type

        return zombieIndex;
    }
    
}

public class ZombieMovement : MonoBehaviour
{
    public float speed = 2f;  // speed of the enemy zombies
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;  // disable gravity
    }
    
    void Update()
    {
        _rb.velocity = new Vector2(-speed, _rb.velocity.y); // movement of the enemies
    }
    
    void OnCollisionEnter2D(Collision2D collision) // if an enemy zombie collides with the player, return to the menu scene
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
    
}
