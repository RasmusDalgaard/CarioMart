using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPointScript : MonoBehaviour
{
    public GameObject FinishLineTrigger;
    public GameObject HalfPointTrigger;
    void OnTriggerEnter()
    {
        FinishLineTrigger.SetActive(true);
        HalfPointTrigger.SetActive(false);
    }
}
