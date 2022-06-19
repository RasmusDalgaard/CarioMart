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
        myAnim.SetFloat("WaitTime", 1 / waitTime);
        myAnim.Play("WaitTime", -1, animationOffset);
    }

}
