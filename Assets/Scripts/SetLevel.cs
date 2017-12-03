using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SetLevel : NetworkBehaviour {

    public Image b1, b2, b3;

    [Header("Connection informations")]
    public InputField ip;
    public InputField port;
    public Text msgError;

    private CountDeath cd;

    private void Start()
    {
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        setDiff(cd.difficulty);
    }

    private bool checkIp()
    {
        string[] ipPart = ip.text.Split('.');
        if (ipPart.Length != 4) return (false);
        else
        {
            foreach (string s in ipPart)
            {
                if (s.Length < 1 || s.Length > 3) return (false);
                foreach (char c in s)
                {
                    if (char.IsNumber(c))
                        return (false);
                }
            }
            return (true);
        }
    }

    public void launchGame(int mode)
    {
        cd.nbDeath = 0;
        cd.score = 0;
        cd.levelPlaying = mode;
        if (mode == 1)
            SceneManager.LoadScene("MainScene");
        else if (mode == 2)
            SceneManager.LoadScene("MainScene2");
        else if (mode == 3)
            SceneManager.LoadScene("RandomScene");
        else if (mode == 4)
        {
            if (port.text != "")
            {
                cd.port = System.Convert.ToInt32(port.text);
                cd.doesHost = true;
                SceneManager.LoadScene("MultiScene");
            }
            else
                msgError.text = "The port must be filled.";
        }
        else if (mode == 5)
        {
           // if (!checkIp())
             //   msgError.text = "The format of the IP is incorrect.";
            if (ip.text != "" || port.text != "")
            {
                cd.port = System.Convert.ToInt32(port.text);
                cd.ip = ip.text;
                cd.doesHost = false;
                SceneManager.LoadScene("MultiScene");
            }
            else
                msgError.text = "All fields must be filled.";
        }
    }

    public void setDiff(int level)
    {
        cd.difficulty = level;
        b1.color = Color.white;
        b2.color = Color.white;
        b3.color = Color.white;
        if (level == 1)
            b1.color = Color.green;
        else if (level == 2)
            b2.color = Color.green;
        else if (level == 3)
            b3.color = Color.green;
    }
}
