using Firebase.Database;
using Mono.Data.Sqlite;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Photon_Obje : MonoBehaviourPunCallbacks
{
    public DatabaseReference usersRef;
    float ktime = (float)0.1;
    private string dbName;
    private string db = "kelimeler.db";
    string cont;
    public List<string> kelimeler = new List<string>();
    public List<string> potkel = new List<string>();
    int puan, Aded = 0;
    int total;
    int total1;
    int total2;
    int control;
    bool joker = false;
    public Text foundedword;
    PhotonView pw;
    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        if (Application.platform != RuntimePlatform.Android)
        {
            cont = Application.dataPath + "/Plugins/" + db;
            dbName = "URI=file:" + cont;
        }
        else
        {
            cont = Application.persistentDataPath + "/" + db;
            if (!File.Exists(cont))
            {
                WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/kelimeler.db");
                while (!load.isDone)
                {
                    File.WriteAllBytes(cont, load.bytes);
                }
            }
            dbName = "URI=file:" + cont;
        }
        CreateDB();
    }
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS kelime (enw VARCHAR(40), trw VARCHAR(40));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ktime += Time.deltaTime;
            if (Tikkontrol.hazir == true)
            {
                Tikkontrol.hazir = false;
                checkword(Tikkontrol.girilen, "MR");
            }
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            ktime += Time.deltaTime;
            if (Tikkontrol.hazir == true)
            {
                Tikkontrol.hazir = false;
                checkword(Tikkontrol.girilen, "RP");
            }
        }

    }
    public void checkword(string girdi,string kim)
    {
        if (kim == "MR")
        {
            total = total1;
        }
        else
        {
            total = total2;
        }
        string tmp = "";
        string tmp2 = "";
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                for (int i = 0; i < girdi.Length; i++)
                {
                    if (girdi.ToCharArray()[i] == '#')
                    {
                        joker = true;
                        tmp2 += "_";
                    }
                    else tmp2 += girdi.ToCharArray()[i];
                }
                girdi = tmp2;
                if (!joker)
                {
                    command.CommandText = "SELECT*FROM kelime WHERE enw='" + girdi + "'";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tmp = reader["enw"].ToString();
                        }
                        reader.Close();
                    }
                    if (tmp == girdi)
                    {
                        foreach (string kem in kelimeler)
                        {
                            if (kem == tmp) control = 99;
                        }
                        if (control == 0)
                        {
                            kelimeler.Add(girdi);
                            foundedword.text += girdi + "\n";
                            puan = (int)(girdi.Length * (90 / ktime));
                            ktime = (float)0.1;
                            total += puan;
                            if (kim == "MR")
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total;
                                CanvasPW.puan2 = total2;
                            }
                            else
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total1;
                                CanvasPW.puan2 = total;
                            }
                        }
                        else
                        {
                            puan = (int)(girdi.Length * (90 / ktime));
                            total -= puan / 2;
                            ktime = (float)0.1;
                            if (kim == "MR")
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total;
                                CanvasPW.puan2 = total2;
                            }
                            else
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total1;
                                CanvasPW.puan2 = total;
                            }
                            control = 0;
                        }
                    }
                }
                else
                {
                    potkel.Clear();
                    Debug.Log(girdi);
                    command.CommandText = "SELECT* FROM kelime WHERE enw LIKE '" + girdi + "'";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tmp = reader["enw"].ToString();
                            potkel.Add(tmp);
                        }
                        reader.Close();
                    }
                    if (potkel.Count > 0)
                    {
                        foreach (string kel in potkel)
                        {
                            Debug.Log(kel);
                            foreach (string kem in kelimeler)
                            {
                                if (kem == kel)
                                {
                                    control = 99;
                                }
                            }
                            if (control == 0)
                            {
                                kelimeler.Add(kel);
                                foundedword.text += kel + "\n";
                                Aded++;
                            }
                            else control = 0;
                        }
                        if (Aded > 0)
                        {
                            puan = (int)(girdi.Length * (90 / ktime) * Aded);
                            total += puan;
                            if (kim == "MR")
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total;
                                CanvasPW.puan2 = total2;
                            }
                            else
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total1;
                                CanvasPW.puan2 = total;
                            }
                            Aded = 0;
                            ktime = (float)0.1;
                        }
                        else
                        {
                            puan = (int)(girdi.Length * (90 / ktime) * potkel.Count);
                            total -= puan / 2;
                            if (kim == "MR")
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total;
                                CanvasPW.puan2 = total2;
                            }
                            else
                            {
                                CanvasPW.ready = true;
                                CanvasPW.puan1 = total1;
                                CanvasPW.puan2 = total;
                            }
                            control = 0;
                            Aded = 0;
                        }
                    }
                    joker = false;
                }
            }
            connection.Close();
        }
        if (kim == "MR")
        {
            total1 = total;
        }
        else
        {
            total2 = total;
        }
    }
}
