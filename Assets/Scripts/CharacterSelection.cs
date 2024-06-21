using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    // this script allows the user to start the game with the selected zombie type
    
    /*
     different index values are assigned to each zombie type (in Inspector panel)
     Zombie1 -> 0
     Zombie2 -> 1
     Zombie3 -> 2
     Zombie4 -> 3
     */
    
    public int characterIndex;

    public void SelectCharacter(int index)
    {
        PlayerPrefs.SetInt("SelectedCharacter", index);
        SceneManager.LoadScene("GameScene");
    }
    
}
