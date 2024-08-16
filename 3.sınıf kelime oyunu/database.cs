using Mono.Data.Sqlite;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Data;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

public class database : MonoBehaviour
{
    public DatabaseReference usersRef;
    float ktime = (float)0.1;
    private string dbName;
    private string olustu="";
    private string tt;
    private string db = "kelimeler.db";
    public TextMesh Bitti;
    public TextMesh sayim;
    public TextMesh fire;
    string cont;
    public List<string> kelimeler = new List<string>();
    public List<string> potkel = new List<string>();
    int puan,Aded=0;
    int total;
    int control;
    bool joker=false;
    public Text sampuan;
    public Text foundedword;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        StartCoroutine(Initilization());
        if (Application.platform != RuntimePlatform.Android)
        {
            cont = Application.dataPath + "/Plugins/" + db;
            dbName = "URI=file:" + cont;
        }
        else
        {
            cont = Application.persistentDataPath+"/"+db;
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
        if (Application.platform != RuntimePlatform.Android)
        {
            string okuyolu = Application.streamingAssetsPath + "/Text/" + "DENE" + ".txt";
            if (File.Exists(okuyolu))
            {

                //List<string> dosyasatir = File.ReadAllLines(okuyolu).ToList();
                //foreach (string satir in dosyasatir)
                //{
                //    string[] yeni = satir.Split('8');
                //    addword(yeni[0], yeni[1]);
                //}
            }
        }
        else
        {
            tt = Application.persistentDataPath + "/" + "DENE"+".txt";
            if (File.Exists(tt))
            {
                List<string> dosyasatir = File.ReadAllLines(tt).ToList();
                foreach (string satir in dosyasatir)
                {
                    string[] yeni = satir.Split('8');
                    addword(yeni[0], yeni[1]);
                }
            }
        }
    }


   private IEnumerator Initilization()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        while (!task.IsCompleted)
        {
            yield return null;
        }
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("Database Error" + task.Exception);
        }
        var dependencyStatus = task.Result;
        Debug.Log(dependencyStatus);
        if (dependencyStatus == DependencyStatus.Available)
        {
            usersRef = FirebaseDatabase.DefaultInstance.RootReference.Child("Kelimeler");
            Debug.Log("init completed");
            fire.text = "init complated";
        }
        else
        {
            fire.text = "init Error";
            Debug.LogError("Database Error");
        }

    }
    public void Getuser()
    {
        StartCoroutine(Getdata());
    }
    public void putuser()
    {

        Bitti.text = "Bitti";
    }
    public IEnumerator Getdata()
    {
        string keli = "";
        var task1 = usersRef.GetValueAsync();
        while (!task1.IsCompleted)
        {
            yield return null;
        }
        if (task1.IsCanceled || task1.IsFaulted)
        {
            Debug.LogError("Database Error" + task1.Exception);
            yield break;
        }
        DataSnapshot hepsi = task1.Result;
        for(int i = 1; i <= hepsi.ChildrenCount; i++)
        {
            keli = "kelime" + i;
            var task = usersRef.Child(keli).GetValueAsync();
            while (!task.IsCompleted)
            {
                yield return null;
            }
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Database Error" + task.Exception);
                yield break;
            }
            DataSnapshot snapshot = task.Result;
            string tenw = "", ttrw = "";
            foreach (DataSnapshot kelime in snapshot.Children)
            {
                if (kelime.Key == "ENW")
                {
                    tenw = kelime.Value.ToString();
                }
                if (kelime.Key == "TRW")
                {
                    ttrw = kelime.Value.ToString();
                }
            }
            addword(tenw, ttrw);
            sayim.text = i.ToString();
            Debug.Log(i);
        }
        Bitti.text = "Tamamlanmıştır.";
    }
    private void Update()
    {
        ktime += Time.deltaTime;
        if (Tikkontrol.hazir == true)
        {
            Tikkontrol.hazir = false;
            checkword(Tikkontrol.girilen);
        }
    }
    // Update is called once per frame
    public void CreateDB()
    {
        using(var connection=new SqliteConnection(dbName))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS kelime (enw VARCHAR(40), trw VARCHAR(40));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void addword(string enword,string trword)
    {
        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO kelime (enw, trw) VALUES (TRIM('" + enword + "'),TRIM('" + trword + "'));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void checkword(string girdi)
    {
        string tmp="";
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
                            puan = (int)(girdi.Length * (90/ktime));
							ktime = (float)0.1;
                            total += puan;
                            sampuan.text = "Score:" + total;
                        }
                        else
                        {
                            puan = (int)(girdi.Length * (90/ktime));
                            total -= puan / 2;
                            ktime = (float)0.1;
                            sampuan.text = "Score:" + total;
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
                            puan = (int)(girdi.Length * (90/ktime)*Aded);
                            total += puan;
                            sampuan.text = "Score:" + total;
                            Aded = 0;
                            ktime = (float)0.1;
                        }
                        else 
                        {
                            puan = (int)(girdi.Length * (90/ktime)*potkel.Count);
                            total -= puan / 2;
                            sampuan.text = "Score:" + total;
                            control = 0;
                            Aded = 0;
                        }
                    }
                    joker = false;
                }
            }
            connection.Close();
        }
    }
}
