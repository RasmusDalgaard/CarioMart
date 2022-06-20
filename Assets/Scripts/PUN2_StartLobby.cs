using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PUN2_StartLobby : MonoBehaviourPunCallbacks

{
    void OnGUI()
    {
        //Styling

        //Start button styling
        GUIStyle startButtonStyle = new GUIStyle(GUI.skin.button);
        startButtonStyle.fontSize = 25;
        Texture2D result = new Texture2D(1, 1);

        //Create transparent background for buttons when player != host
        Color[] pix = new Color[] { Color.clear };
        result.SetPixels(pix);
        result.Apply();
        if(!PhotonNetwork.IsMasterClient) { 
            startButtonStyle.normal.background = result;
            startButtonStyle.hover.background = result;
            startButtonStyle.active.background = result;
            startButtonStyle.fontSize = 35;
        }

        //Leave button styling
        GUIStyle leaveButtonStyle = new GUIStyle(GUI.skin.button);
        leaveButtonStyle.fontSize = 25;

        //Text & Label styling
        GUIStyle textStyle = new GUIStyle();
        textStyle.fontSize = 30;
        textStyle.normal.textColor = Color.white;


        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        if (GUI.Button(new Rect(5, 5, 200, 50), "Leave Room", leaveButtonStyle))
        {
            PhotonNetwork.LeaveRoom();
        }

        //Check if player is host
        string isHost = (PhotonNetwork.IsMasterClient ? "Start Game" : "Waiting for host to start");

        //Start Game
        if  (GUI.Button(new Rect(Screen.width / 2-200, Screen.height / 2, 400, 75), isHost, startButtonStyle))
        {
            if(PhotonNetwork.IsMasterClient)
            { 
            PhotonNetwork.LoadLevel("Playground");
            } 
        }

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": Host" : "");
            GUI.Label(new Rect(5, 70+30 * i, 400, 50), PhotonNetwork.PlayerList[i].NickName + isMasterClient, textStyle);
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }
}
