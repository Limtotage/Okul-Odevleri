using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontrol : MonoBehaviour
{
    public List<Transform> kareler;
    public List<int[]> diziler=new List<int[]>();
    public List<string> kelimeler;
    public int[] dizi1;
    public int[] dizi2;
    public int[] dizi3;
    public int[] dizi4;
    public int[] dizi5;
    public int[] dizi6;
    public int[] dizi7;
    public int[] dizi8;
    public int[] dizi9;
    public int[] dizi10;
    // Start is called before the first frame update
    void Start()
    {
        diziler.Add(dizi1);
        diziler.Add(dizi2);
        diziler.Add(dizi3);
        diziler.Add(dizi4);
        diziler.Add(dizi5);
        diziler.Add(dizi6);
        diziler.Add(dizi7);
        diziler.Add(dizi8);
        diziler.Add(dizi9);
        diziler.Add(dizi10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Tikkontrol.hazir==true)
        {
            Tikkontrol.hazir = false;
            for(int i = 0; i < kelimeler.Count; i++)
            {
                if (Tikkontrol.girilen == kelimeler[i])
                {
                    int[] indisler = diziler[i];
                    for(int j = 0; j < kelimeler[i].Length; j++)
                    {
                        kareler[indisler[j]].GetComponent<TextMesh>().text = Tikkontrol.girilen[j].ToString();
                    }
                }
            }
        }
    }
}
