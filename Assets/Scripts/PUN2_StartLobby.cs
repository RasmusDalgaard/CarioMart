using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PUN2_StartLobby : MonoBehaviourPunCallbacks

{
    void OnGUI()
    {
        //Styling

        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 25;

        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 30;
        myStyle.normal.textColor = Color.white;


        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        if (GUI.Button(new Rect(5, 5, 200, 50), "Leave Room", myButtonStyle))
        {
            PhotonNetwork.LeaveRoom();
        }

        //Start Game
        if  (GUI.Button(new Rect(Screen.width / 2-125, Screen.height / 2, 250, 75), "Start Game", myButtonStyle))
        {
            Debug.Log("Start");
            PhotonNetwork.LoadLevel("Playground");
        }

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": Host" : "");
            GUI.Label(new Rect(5, 70+30 * i, 400, 50), PhotonNetwork.PlayerList[i].NickName + isMasterClient, myStyle);
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }
}
