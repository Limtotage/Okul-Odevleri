using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static float timer = 0;
    int kalan;
    public int zaman;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Tikkontrol.gameon == true)
        {
            timer += Time.deltaTime;
            kalan = (int)(zaman - timer);
            text.text = "Time:" + kalan;
        }
        if (timer >= zaman)
        {
            Tikkontrol.gameon = false;
        }
    }
    public static void bastan()
    {
        timer = 0;
        Tikkontrol.gameon = true;
    }
}
