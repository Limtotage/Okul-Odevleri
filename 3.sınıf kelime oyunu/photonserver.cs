using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class photonserver : MonoBehaviourPunCallbacks
{
    public TMP_InputField odaisim;
    public GameObject yen;
    public GameObject ken;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {

        Debug.Log("oda bekleniyor");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("giremedi");
    }
    public void odakur()
    {
        if (string.IsNullOrEmpty(odaisim.text)) return;
        RoomOptions yenioda = new RoomOptions();
        yenioda.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(odaisim.text, yenioda, TypedLobby.Default);

    }
	public void odakatil()
    {
        if (string.IsNullOrEmpty(odaisim.text)) return;
        PhotonNetwork.JoinRoom(odaisim.text);
    }
    public override void OnJoinedRoom()
    {
        yen.SetActive(false);
        ken.SetActive(true);
        GameObject kullanici = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, null);
        Debug.Log("girdikhaci");
    }

    public void Vazgectim()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
