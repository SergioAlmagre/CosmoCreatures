using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Photonconexion : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Usará las settings de nuestro servidor.
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectados!!");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}
