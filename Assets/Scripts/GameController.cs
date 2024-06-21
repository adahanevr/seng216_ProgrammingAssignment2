using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int selectedCharacterIndex; // index number of the selected zombie type
    public GameObject[] characters; // array of zombie prefabs
    public GameObject player;
    private Animator _playerAnimator;
    private Rigidbody2D _rb;
    
    public Vector2 playerSpawnPosition = new Vector2(0, -2.7f);
    public float moveSpeed = 5f; // horizontal speed of the player
    public float jumpForce = 9f; // vertical force for player to jump
    private bool _isGrounded; // boolean variable to check if the player is on the ground

    public GameObject background; // used for background movement (Background gameObject is assigned to this field in Inspector)
    
    public float parallaxSpeed;  // speed of the parallax movement of background (set at 0.4 in Inspector)
    private Renderer _backgroundRenderer; // Renderer component for the background object
    private Vector3 _lastPlayerPosition; // position of the player

    
    void Start()
    {
        // create character based on user's selection
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); // get the index value of the selected zombie type
        player = Instantiate(characters[selectedCharacterIndex], playerSpawnPosition, Quaternion.identity); // create the player from the selected zombie type's prefab object
        player.tag = "Player";
        
        _rb = player.GetComponent<Rigidbody2D>();
        _playerAnimator = player.GetComponent<Animator>();
        
        _lastPlayerPosition = Vector3.zero;  // the player starts at x=0
        _backgroundRenderer = background.GetComponent<Renderer>();  // retrieve the Renderer component of the background
    }

    void Update()
    {
        // vertical and horizontal movement of the player
        float vertical = _rb.velocity.y;
        float horizontal = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(horizontal * moveSpeed, _rb.velocity.y);

        if (horizontal != 0) // if there's horizontal user input (at any direction)
        {
            _playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            _playerAnimator.SetBool("isRunning", false);
        }

        if (horizontal > 0) // if the user presses "right arrow" key
        {
            player.transform.localScale = new Vector3(1.8f, 1.8f, 1); // the player faces to right
        }
        else if (horizontal < 0) // if the user presses "left arrow" key
        {
            player.transform.localScale = new Vector3(-1.8f, 1.8f, 1); // scale the player object so that it faces to left
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) // if the player is on the ground and the user presses "Space" key
        {
            vertical = jumpForce; // jump (jumpForce = 9f defined above)
        }
        
        _rb.velocity = new Vector2(0, vertical); // hold the player at x=0 (so that it remains in the middle of the screen)
        

        if (player.transform.position.y <= -2.7f) // player's y component is set to -2.7f initially (in Inspector)
        {
            // player.transform.position.y <= -2.7f means the player touches the ground
            player.transform.position = new Vector3(player.transform.position.x, -2.7f, player.transform.position.z);
            // set the y value at -2.7 so that it doesnt keep falling
            _isGrounded = true;
            _playerAnimator.SetBool("isJumping", false);
            _rb.gravityScale = 0f;
        }
        else
        {
            // if the player is not on the ground
            _isGrounded = false;
            _playerAnimator.SetBool("isJumping", true);
            _rb.gravityScale = 1f;
        }
        
        HandleParallax(); // handle the background parallax movement
    }
    
    public void onClickExit(){ // controls the "EXIT" button in the game scene
        SceneManager.LoadScene("MenuScene");
    }
    
    private void HandleParallax()
    {
        float horizontalInput = Input.GetAxis("Horizontal");  // get horizontal input
        Vector2 offset = new Vector2(horizontalInput * parallaxSpeed * Time.deltaTime, 0);  // calculate the offset value based on horizontal input
        _backgroundRenderer.material.mainTextureOffset += offset;  // update the texture offset of the background
    }
    

}

