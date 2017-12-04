using UnityEngine;
using UnityEngine.UI;

public class WinDialogue : MonoBehaviour {

    public bool isNormalMode { set; private get; }
    private CountDeath cd;

    void Start()
    {
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        int nbDeath = cd.nbDeath;
        int diff = cd.difficulty;
        int whichLevel = cd.levelPlaying;
        if (whichLevel == 1)
        {
            if (nbDeath > 3)
                GetComponent<Text>().text = nbDeath + " deaths, I have seen better, but nice job I guess.";
            else if (nbDeath > 0)
                GetComponent<Text>().text = nbDeath + " deaths, not that bad... Congratulation I guess.";
            else if (nbDeath == 0 && diff != 3)
                GetComponent<Text>().text = "You didn't even die once, you are better than what I would expect. But you were not on the hardest mode thought.";
            else if (nbDeath == 0 && diff == 3)
                GetComponent<Text>().text = "You didn't even die once, you are better than what I would expect. I would have made you a cake if I knew how to cook.";
        }
        else if (whichLevel == 2)
        {
            if (nbDeath > 3)
                GetComponent<Text>().text = nbDeath + " deaths, I have seen better, but nice job I guess.";
            else if (nbDeath > 0)
                GetComponent<Text>().text = nbDeath + " deaths, not that bad... Congratulation I guess.";
            else if (nbDeath == 0 && diff != 3)
                GetComponent<Text>().text = "You didn't even die once, you are better than what I would expect. But you were not on the hardest mode thought.";
            else if (nbDeath == 0 && diff == 3)
                GetComponent<Text>().text = "You didn't even die once, I'm really impressed. Congratulation.";
        }
        else if (whichLevel == 7)
        {
            GetComponent<Text>().text = "Thanks for finishing the tutorial, you are now ready to play.";
        }
    }
}
