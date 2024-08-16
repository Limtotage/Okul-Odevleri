using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xbutonu : MonoBehaviour
{
	public List<string> harfler = new List<string>();
	public List<int> hindis = new List<int>();
	public List<Transform> butonlar = new List<Transform>();
    public void OnMouseDown()
    {
		hindis.Clear();
		harfler.Clear();
		int i,syc=0;
		for (i = 0; i < butonlar.Count; i++)
		{
			harfler.Add(butonlar[i].GetComponent<TextMesh>().text);//a-b-c-d&&0-1-2-3
			hindis.Add(-1);
		}
		for (i = 0; i < butonlar.Count; i++)
		{
			while (true)
			{
				syc++;
				int rnd = Random.Range(0, butonlar.Count);
				bool ok = bul(rnd);
				if (ok==true)
				{
					hindis[i] = rnd;//hindis 0 = 3||hindis 1=1||hindis 2=0
					butonlar[rnd].GetComponent<TextMesh>().text = harfler[i];//3. buttona 0. harf||1. buttona 1. harf||0. buttona 2. harf
					break;
				}
			}
		}
	}
	public bool bul(int x)
	{
		int kont = 0;
		int i;
		for (i = 0; i < hindis.Count; i++)
		{
			if (hindis[i] == x) kont++;
		}
		if (kont == 0) return true;
		else return false;
	}
}
