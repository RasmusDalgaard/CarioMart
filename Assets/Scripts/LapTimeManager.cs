using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string MilliDisplay;
    public GameObject MinuteBox;
    public GameObject SecondBox;
    public GameObject MilliBox;
    public static float RawTime;

    void Update()
    {
        //MILISECONDS
        MilliCount += Time.deltaTime * 10;
        RawTime += Time.deltaTime;
        //Convert to string value
        MilliDisplay = MilliCount.ToString("F0").Replace(",", "");
        //Display milliseconds in the UI element
        MilliBox.GetComponent<Text>().text = "" + MilliDisplay;
        //Reset MilliCount to 0 when it hits 10, and increment SecondCount
        if (MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;
        }

        //SECONDS
        if (SecondCount <= 9)
        {
            SecondBox.GetComponent<Text>().text = "0" + SecondCount + ".";
        }
        else
        {
            SecondBox.GetComponent<Text>().text = "" + SecondCount + ".";
        }

        if (SecondCount == 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        //MINUTES
        if (MinuteCount <= 9)
        {
            MinuteBox.GetComponent<Text>().text = "0" + MinuteCount + ":";
        }
        else
        {
            MinuteBox.GetComponent<Text>().text = "" + MinuteCount + ":";
        }
    }
}
