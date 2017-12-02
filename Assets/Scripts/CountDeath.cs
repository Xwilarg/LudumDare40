using UnityEngine;
using System.IO;

public class CountDeath : MonoBehaviour {
    
    public int nbDeath { set; get; }
    public int difficulty { set; get; }

	void Start () {
        DontDestroyOnLoad(gameObject);
        nbDeath = 0;
        difficulty = 2;
        if (!File.Exists("Saves.dat"))
        {
            File.WriteAllText("Saves.dat", getFile(0));
        }
    }

    string getFile(int lvl)
    {
        return ((lvl * 549 % 23 - lvl * 4) - 1).ToString();
    }
}
