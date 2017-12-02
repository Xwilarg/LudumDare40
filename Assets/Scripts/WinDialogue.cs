using UnityEngine;
using UnityEngine.UI;

public class WinDialogue : MonoBehaviour {
    
	void Start () {
        int nbDeath = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().nbDeath;
        int diff = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().difficulty;
        if (nbDeath > 3)
            GetComponent<Text>().text = nbDeath + " deaths, I have seen better, but nice job I guess.";
        else if (nbDeath > 0)
            GetComponent<Text>().text = nbDeath + " deaths, not that bad... Congratulation I guess.";
        else if (nbDeath == 0 && diff != 3)
            GetComponent<Text>().text = "You didn't even die once, you are better than what I would expect. But you were not on the hardest mode thought.";
        else if (nbDeath == 0 && diff == 3)
            GetComponent<Text>().text = "You didn't even die once, you are better than what I would expect. I would have made you a cake if I knew how to cook.";
    }
}
