using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Vector2 playerInitPosition;

    void Start()
    {
        playerInitPosition= FindObjectOfType<NewBehaviourScript>().transform.position;
    }
    public void Restart()
    {
        //1- restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //2- reset the position
        //Save the players initial position when game starts
        //when respawning simply reposition the player to the initial position
        //Reset the players movement speed
        //FindObjectOfType<NewBehaviourScript>().ResetPlayer();
        //FindObjectOfType<NewBehaviourScript>().transform.position = playerInitPosition;
        //FindObjectOfType<HealthBar>().rese
    }
}
