using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    Animator myAnim;

    [SerializeField] float waitTime;
    [SerializeField] [Range(0, 1)] float animationOffset;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myAnim.SetFloat("WaitTime", 1 / waitTime);  //  1 / waitime, allows us to add time in seconds we want this animation to be.
        myAnim.Play("WaitTime", -1, animationOffset);       //If we have multiple crushers in the scene, they will not be sync with eachother.
    }

}
