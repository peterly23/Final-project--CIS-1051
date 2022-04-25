using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public float health;
    
    public void LoseHealth(int value)
    {
        //if no lives remaining do nothing
        if(health<=0)
            return;
        //Reduce the health 
        health -= value;
        //refresh the UI fillbar
        fillBar.fillAmount = health / 100;
        //check if your health is zero or less = dead
        if(health<=0)
        {
            //Debug.Log("YOU DIED");
            //FindObjectOfType<LevelManager>().Restart();
            FindObjectOfType<NewBehaviourScript>().Die();
            
        }

    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Return))
           //LoseHealth(25);
    }
}
