using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    float WPradius = 1;  //WaypointRadius - if the speed or size of object too large, it might miss the waypoint. if the wpraidus is 1 it means it reached and is ready to go to next waypoint

    void Update()
    {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius) //check if current position is less than Waypoint radis.
        {
            current++;                                                                              //adds 1 to current, and makes us go to the next waypoint
            if (current >= waypoints.Length)                                                        // if the current is >= waypoints length we have reached, and it sets current back to 0, so it keeps going in a loop
            {
                current = 0;
            }
        }
        //moves the object between current and target. with Time.deltaTime * speed, we can make it go faster
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);    
    }
}
