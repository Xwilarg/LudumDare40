using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkHUD : NetworkManager
{
    private void Start()
    {
        CountDeath cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        networkPort = cd.port;
        if (cd.doesHost)
            StartHost();
        else
        {
            networkAddress = cd.ip;
            StartClient();
        }
    }
}
