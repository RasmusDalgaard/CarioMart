using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GameObject CountDown;
    //public AudioSource GetReadyAudio;
    //public AudioSource GoAudio;
    public GameObject LapTimer;
    //public GameObject CarControls;
    void Start()
    {
        StartCoroutine(CountStart());
    }

    //Play audio as counter counts down
    IEnumerator CountStart()
    {
        yield return new WaitForSeconds(0.5f);

        CountDown.GetComponent<Text>().text = "3";
        //GetReadyAudio.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "2";
        //GetReadyAudio.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "1";
        //GetReadyAudio.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        //GoAudio.Play()
        LapTimer.SetActive(true);
        //CarControls.SetActive(true);
    }
}
