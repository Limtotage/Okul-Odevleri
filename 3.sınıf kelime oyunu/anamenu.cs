
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anamenu : MonoBehaviour
{
    public TextMesh kul1;
    public void Settin()
    {
        SceneManager.LoadScene(4);
    }
    public void QUI()
    {
        Application.Quit(0);
    }
    public void back()
    {
        SceneManager.LoadScene(0);
    }
    public void KARE()
    {
        SceneManager.LoadScene(1);
    }
    public void KELIME()
    {
        SceneManager.LoadScene(2);
    }
    public void ONLINE()
    {
        SceneManager.LoadScene(3);
    }
}
