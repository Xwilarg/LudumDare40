using UnityEngine;
using UnityEngine.UI;

public class RandomSentenceDeath : MonoBehaviour {

    private Text sentence;

    private void Start()
    {
        CountDeath cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        cd.nbDeath++;
        sentence = GetComponent<Text>();
        switch (Random.Range(0, 10))
        {
            case 0: sentence.text = "Are you even trying ?"; break;
            case 1: sentence.text = "Death won't save you, I still have many clones of you."; break;
            case 2: sentence.text = "Another death... take your time..."; break;
            case 3: sentence.text = "Where is the tutorial ? This is the tutorial!"; break;
            case 4: sentence.text = "That was fun but you can begin to try now."; break;
            case 5: sentence.text = "Why the robots aren't helping you ? They consider you as too unevoled to survive."; break;
            case 6: sentence.text = "Did you try using your brain instead of running into lasers ?"; break;
            case 7: sentence.text = "Running into lasers won't make them malfunction, you can't stop with this strategy now."; break;
            case 8: sentence.text = "I wish I had someone clever to do this job but I'm stuck with you instead..."; break;
            case 9: sentence.text = "There is a cake awaiting you at the end... Maybe..."; break;
        }
        sentence.text += System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine + "(You died " + cd.nbDeath + " times.)";
    }
}
