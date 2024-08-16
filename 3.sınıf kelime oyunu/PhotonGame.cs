using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class PhotonGame : MonoBehaviourPunCallbacks
{
    public TextMesh kul1;
    PhotonView pw;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        kul1 = GameObject.Find("Kelime").GetComponent<TextMesh>();
        if (pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                gameObject.name = "Master";
                kul1.text = "Rakip Bekleniyor";
                transform.position = new Vector3(-0.8f, -1.4f, 0);
                InvokeRepeating("oyuncukont", 0, 0.5f);
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                gameObject.name = "Rakip";
                transform.position = new Vector3(-0.8f, 3.55f, 0);
            }
        }
    }
    void oyuncukont()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            kul1.text = "";
            UI.bastan();
            CancelInvoke("oyuncukont");
        }
    }

}
