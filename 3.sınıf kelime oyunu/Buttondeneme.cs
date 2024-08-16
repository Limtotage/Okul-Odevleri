using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttondeneme : MonoBehaviour
{
    public List<string> Kelimeler = new List<string>();
    public List<Button> butonlar = new List<Button>();
    public Text Hedef;
    public void kelime0()
    {
        Hedef.text = Kelimeler[0];
    }
    public void kelime1()
    {
        Hedef.text = Kelimeler[1];
    }
    public void kelime2()
    {
        Hedef.text = Kelimeler[2];
    }
    public void kelime3()
    {
        Hedef.text = Kelimeler[3];
    }
    public void kelime4()
    {
        Hedef.text = Kelimeler[4];
    }public void kelime5()
    {
        Hedef.text = Kelimeler[5];
    }
    public void kelime6()
    {
        Hedef.text = Kelimeler[6];
    }
    public void kelime7()
    {
        Hedef.text = Kelimeler[7];
    }
    public void kelime8()
    {
        Hedef.text = Kelimeler[8];
    }
    public void kelime9()
    {
        Hedef.text = Kelimeler[9];
    }
}
