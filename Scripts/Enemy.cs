
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]


public class Enemy : MonoBehaviour
{
    int decayAmount = 25;
    //Reference to waypoints
    public List<Transform> points; 
    // the int value for the next point index
    public int nextID=0;
    //The value of that applies to ID for changing
    int idChangeValue = 1;
    //speed of movement or flying
    public float speed =2;



    private void Reset()
    {
        Init();


    }

    void Init()
    {
        //Make box collider trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
        
        //Create root object
        GameObject root = new GameObject(name + "_Root");

        //Reset position of Root to enemy object
        root.transform.position = transform.position;
        //set enemy object as child of root
        transform.SetParent(root.transform);
        //create waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //reset waypoints position to root
        //make waypoint object child of root 
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //create two points (gameobject) and reset their position to waypoints objects
        // make the points children of waypoint object
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform);p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform);p2.transform.position = root.transform.position;

        //Init points list then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
        

    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //Get the next Point transform
        Transform goalPoint = points[nextID];
        //flip the enemy transform to look into the points direction
        if(goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        //Move the enemy towards the goal points
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed*Time.deltaTime);
        //check the distance between enemy and goal points to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position)<0.2f)
        {
            //check if we are at the end of the line (make the change -1)
            if(nextID == points.Count - 1)
                idChangeValue = -1;
            //check if we are at the start of the line(make the chagne+1)
            if(nextID == 0)
               idChangeValue = 1; 
            //apply the change on the nextID
            nextID += idChangeValue;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<HealthBar>().LoseHealth(decayAmount);
            
        }
    }
    
}
