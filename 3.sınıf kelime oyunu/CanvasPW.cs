using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CanvasPW : MonoBehaviour
{
    public Text sampuan;
    public Text sampuan2;
    public static int puan1;
    public static int puan2;
    public static bool ready=false;
    PhotonView pw;
    // Start is called before the first frame update
    void Start()
    {
        pw=GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine)
        {
            if (ready)
            {
                sampuan.text = "Score1:" + puan1;
                sampuan2.text = "Score2:" + puan2;
                ready = false;
            }
        }
    }
}
