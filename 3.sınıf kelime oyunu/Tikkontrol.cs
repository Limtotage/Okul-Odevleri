using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tikkontrol : MonoBehaviour
{
    public static string girilen = "";
    public static bool ilk=false;
    public bool tekrar = false;
    public static bool gameon = true;
    public bool aktif = true;
    public static bool hazir = false;
    TextMesh text;
    public TextMesh Cikti;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ilk&& gameon)
        {
            if(tekrar) text.color = Color.blue;
            else text.color = Color.black;
            aktif = true;
        }
    }
    public void tiklandi()
    {
        text = GetComponent<TextMesh>();
        if(!tekrar)text.color = Color.cyan;
        girilen += GetComponent<TextMesh>().text;
        Cikti.text = girilen;
        if (tekrar == false) aktif = false;
    }
    public void OnMouseDown()
    {
        if (!ilk&& gameon)
        {
            girilen = "";
            ilk = true;
            aktif = true;
            tiklandi();
        }
    }
    public void OnMouseEnter()
    {
        if (ilk&&aktif&&gameon)
        {
            tiklandi();
        }
    }
    public void OnMouseUp()
    {
        if (ilk&& gameon)
        {
            hazir = true;
            Cikti.text = "";
            ilk = false;
        }
    }
}
