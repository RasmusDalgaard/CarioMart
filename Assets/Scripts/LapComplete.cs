using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{
    public GameObject FinishLineTrigger;
    public GameObject HalfPointTrigger;

    public GameObject MinuteDisplayBest;
    public GameObject SecondDisplayBest;
    public GameObject MilliDisplayBest;
    public GameObject LapCounter;
    public int LapsDone = 0;
    public float RawTime;

    public GameObject RaceFinish;

    void Update()
    {
        if (LapsDone == 2)
        {
            RaceFinish.SetActive(true);
        }
    }

    void OnTriggerEnter()
    {
        LapsDone++;
        RawTime = PlayerPrefs.GetFloat("RawTime");
        if (LapTimeManager.RawTime <= RawTime)
        {
            if (LapTimeManager.SecondCount <= 9)
            {
                SecondDisplayBest.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
            }
            else
            {
                SecondDisplayBest.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
            }

            if (LapTimeManager.MinuteCount <= 9)
            {
                MinuteDisplayBest.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ".";
            }
            else
            {
                MinuteDisplayBest.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ".";
            }

            MilliDisplayBest.GetComponent<Text>().text = "" + LapTimeManager.MilliCount.ToString("F0").Replace(",", " ");
        }

        PlayerPrefs.SetInt("MinSave", LapTimeManager.MinuteCount);
        PlayerPrefs.SetInt("SecSave", LapTimeManager.SecondCount);
        PlayerPrefs.SetFloat("MilliSave", LapTimeManager.MilliCount);
        PlayerPrefs.SetFloat("RawTime", LapTimeManager.RawTime);
        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MilliCount = 0;
        LapTimeManager.RawTime = 0;

        LapCounter.GetComponent<Text>().text = "" + LapsDone;
        HalfPointTrigger.SetActive(true);
        FinishLineTrigger.SetActive(false);
    }

}
