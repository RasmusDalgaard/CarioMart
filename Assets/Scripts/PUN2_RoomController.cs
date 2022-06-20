using UnityEngine;
using Photon.Pun;

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{

    //Player instance prefab, must be located in the Resources folder
    public GameObject playerPrefab;

    //Player Camera instance prefab, must be located in the Resources folder
    private GameObject playerCamera;

    //Camera offset to target
    public Vector3 offset;

    //Player spawn point
    public Transform[] spawnPoints;

    // Use this for initialization
    void Start()
    {
        //In case we started this demo with the wrong scene being active, simply load the menu scene
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
            return;
        }
        
        //We're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        GameObject instantiatedPlayer = PhotonNetwork.Instantiate("Prefabs/Car", spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].rotation, 0);

        // instantiate cam
       // GameObject instantiatedCamera = PhotonNetwork.Instantiate("Prefabs/PlayerCamera", new Vector3(485, 5, 515), Quaternion.identity, 0);

        //Camera
        playerPrefab = instantiatedPlayer;
       // playerCamera = instantiatedCamera
        playerCamera = Camera.main.gameObject;

        //Set camera start position
        playerCamera.transform.position = playerPrefab.transform.position;
        
    }

    public void LookAtTarget()
    {
        Vector3 lookDirection = playerPrefab.transform.position - playerCamera.transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, rotation, 10 * Time.deltaTime);
    }

    public void MoveToTarget()
    {
        Vector3 targetPosition = playerPrefab.transform.position +
                             playerPrefab.transform.forward * offset.z +
                             playerPrefab.transform.right * offset.x +
                             playerPrefab.transform.up * offset.y;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, targetPosition, 10 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
        //Camera Movement
        if (playerPrefab != null && playerCamera != null)
        {
            //Camera Rotation
            LookAtTarget();

            //Camera Follow
            MoveToTarget();
        }
    }

    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }
}