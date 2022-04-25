using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{
    public GameObject FinishLineTrigger;
    public GameObject HalfPointTrigger;

    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;
    public GameObject LapCounter;
    public int LapsDone;
    public float RawTime;

    void OnTriggerEnter()
    {
        LapsDone++;
        RawTime = PlayerPrefs.GetFloat("RawTime");
        if (LapTimeManager.RawTime <= RawTime)
        {
            if (LapTimeManager.SecondCount <= 9)
            {
                SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
            }
            else
            {
                SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
            }

            if (LapTimeManager.MinuteCount <= 9)
            {
                MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ".";
            }
            else
            {
                MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ".";
            }

            MilliDisplay.GetComponent<Text>().text = "" + LapTimeManager.MilliCount.ToString("F0").Replace(",", "");
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
