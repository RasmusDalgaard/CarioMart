using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLapTime : MonoBehaviour
{
    public int MinCount;
    public int SecCount;
    public float MilliCount;
    public GameObject MinDisplayBest;
    public GameObject SecDisplayBest;
    public GameObject MilliDisplayBest;
    void Start()
    {
        MinCount = PlayerPrefs.GetInt("MinSave");
        SecCount = PlayerPrefs.GetInt("SecSave");
        MilliCount = PlayerPrefs.GetFloat("MilliSave");

        MinDisplayBest.GetComponent<Text>().text = "" + MinCount + ":";
        SecDisplayBest.GetComponent<Text>().text = "" + SecCount + ".";
        MilliDisplayBest.GetComponent<Text>().text = "" + MilliCount.ToString().Replace(",", "");
    }

}
