using UnityEngine;
using System.IO;

public class CountDeath : MonoBehaviour {
    
    public int nbDeath { set; get; }
    public int difficulty { set; get; }
    public int currLevel { set; get; }
    public int levelPlaying { set; get; }
    public int score { set; get; }
    public string ip { set; get; }
    public int port { set; get; }
    public bool doesHost { set; get; }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        nbDeath = 0;
        difficulty = 2;
        if (!File.Exists("Saves.dat"))
        {
            File.WriteAllText("Saves.dat", getFile(0));
            currLevel = 0;
        }
        else
        {
            string content = File.ReadAllText("Saves.dat");
            if (content == getFile(1)) currLevel = 1;
        }
    }

    public void increaseFile(int level)
    {
        string content = File.ReadAllText("Saves.dat");
        if (content != getFile(level))
            File.WriteAllText("Saves.dat", getFile(level));
    }

    string getFile(int lvl)
    {
        return ((lvl * 549 % 23 - lvl * 4) - 1).ToString();
    }
}
