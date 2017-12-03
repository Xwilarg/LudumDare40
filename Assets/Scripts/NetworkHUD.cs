using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHUD : NetworkManager {

    public void StartHostS(int port)
    {
        SceneManager.LoadScene("MultiScene");
        singleton.networkPort = port;
        singleton.StartHost();
    }

    public void JoinGameS(string ip, int port)
    {
        SceneManager.LoadScene("MultiScene");
        singleton.networkAddress = ip;
        singleton.networkPort = port;
        singleton.StartClient();
    }
}
