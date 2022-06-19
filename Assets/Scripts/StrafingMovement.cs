using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafingMovement : MonoBehaviour
{
    public Transform[] target;
    public float speed;

    private int current = 0;



    void Update()
    {

        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % target.Length;

    }
}
