using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harfler : MonoBehaviour
{
    List<int> tekrarlar = new List<int>();
    public List<string> harfler = new List<string>();
    public List<string> harflerses = new List<string>();
    public List<Transform> butonlar = new List<Transform>();
    int i;
    int cont = 0;
    int rnd3 = -1;
    int rnd4 = -1;
    int rnd5 = -1;
    // Start is called before the first frame update
    void Start()
    {
        rnd4 = Random.Range(0, 100);
        rnd5 = Random.Range(0, 100);
        for (int x=rnd4%3 ; x > 0; x--)
        {
            int rnd = Random.Range(0, harfler.Count);
            rnd3 = Random.Range(0, butonlar.Count);
            butonlar[rnd3].GetComponent<Tikkontrol>().tekrar = true;
            butonlar[rnd3].GetComponent<TextMesh>().text = harfler[rnd];
            tekrarlar.Add(rnd3);
        }
        for (int x=rnd5%3;x>0;x--)
        {
            rnd3 = Random.Range(0, butonlar.Count);
            butonlar[rnd3].GetComponent<TextMesh>().text ="#";
            tekrarlar.Add(rnd3);
        }
        for (i = 0; i < butonlar.Count; i++)
        {
            cont = 0;
            foreach (int y in tekrarlar)
            {
                if (i == y) cont = 99;
            }
            if (cont == 99) continue;
            int rnd = Random.Range(0, harfler.Count);
            int rnd2 = Random.Range(0, harflerses.Count);
            if (i < 2) butonlar[i].GetComponent<TextMesh>().text = harflerses[rnd2];
            else butonlar[i].GetComponent<TextMesh>().text = harfler[rnd];
        }
    }
    private void OnMouseDown()
    {
        int rnd = Random.Range(0, harfler.Count);
        foreach (int y in tekrarlar)
        {
            butonlar[y].GetComponent<Tikkontrol>().tekrar = false;
            if (butonlar[y].GetComponent<TextMesh>().text == "#")
                butonlar[y].GetComponent<TextMesh>().text = harfler[rnd];

        }
        rnd4 = Random.Range(0, 100);
        rnd5 = Random.Range(0, 100);
        for (int x = rnd5 % 3; x > 0; x--)
        {
            rnd3 = Random.Range(0, butonlar.Count);
            butonlar[rnd3].GetComponent<TextMesh>().text = "#";
            tekrarlar.Add(rnd3);
        }
        for (int x=rnd4%3; x > 0; x--)
        {
            rnd3 = Random.Range(0, butonlar.Count);
            if(butonlar[rnd3].GetComponent<TextMesh>().text == "#")
            {
                x++;
                continue;
            }
            butonlar[rnd3].GetComponent<Tikkontrol>().tekrar = true;
            tekrarlar.Add(rnd3);
        }
        for (i = 0; i < butonlar.Count; i++)
        {
            if (rnd4 != 0)
            {
                cont = 0;
                foreach (int y in tekrarlar)
                {
                    if (i == y) cont = 99;
                }
                if (cont == 99) continue;
            }
            rnd = Random.Range(0, harfler.Count);
            int rnd2 = Random.Range(0, harflerses.Count);
            if (i < 2) butonlar[i].GetComponent<TextMesh>().text = harflerses[rnd2];
            else butonlar[i].GetComponent<TextMesh>().text = harfler[rnd];
        }
    }
}
