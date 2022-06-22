using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GameObject CountDown;
    public AudioSource source;
    public AudioClip GetReadyAudio;
    public AudioClip GoAudio;
    public GameObject LapTimer;
    public GameObject RoomController;
    void Start()
    {
        StartCoroutine(CountStart());
    }

    //Play audio as counter counts down
    IEnumerator CountStart()
    {
        yield return new WaitForSeconds(0.5f);

        CountDown.GetComponent<Text>().text = "3";
        source.PlayOneShot(GetReadyAudio);
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "2";
        source.PlayOneShot(GetReadyAudio);
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "1";
        source.PlayOneShot(GetReadyAudio);
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        source.PlayOneShot(GoAudio);
        LapTimer.SetActive(true);

        //Activate Car motor force
        var Car = RoomController.GetComponent<PUN2_RoomController>().playerPrefab;
        Car.GetComponent<NewCarController>().motorForce = 500;
    }
}
