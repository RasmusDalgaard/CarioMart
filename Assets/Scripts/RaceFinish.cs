using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    public GameObject RoomController;

    void OnTriggerEnter()
    {
        GameObject Car = RoomController.GetComponent<PUN2_RoomController>().playerPrefab;
        Car.SetActive(false);
        Car.GetComponent<NewCarController>().motorForce = 0;
        Car.GetComponent<NewCarController>().enabled = false;
        Car.SetActive(true);
        GameObject FinishCam = Car.transform.GetChild(1).gameObject;
        FinishCam.SetActive(true);
    }
}
