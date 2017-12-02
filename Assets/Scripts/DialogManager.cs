using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public GameObject DialogBox;
    public Text content;
    public Text speakName;
    public GameObject goChoice1, goChoice2, goChoice3, goChoice4;
    public Text textChoice1, textChoice2, textChoice3, textChoice4;
    public PlayerController pc;

    [Header("Special actions")]
    public moveDoor md;
    public MoveLaser laser;

    public enum typeEvent
    {
        intro
    }

    private typeEvent e;

    private int currId;

    private void Start()
    {
        currId = 0;
        launchDialog(typeEvent.intro);
    }

    public void launchDialog(typeEvent curr)
    {
        if (!DialogBox.activeInHierarchy)
        {
            e = curr;
            DialogBox.SetActive(true);
        }
        if (curr == typeEvent.intro)
        {
            if (currId == 0)
                loadText("Feminin voice", "Subject, are you awake ?", "Who are you ?", "Yes", "5 more minutes please...", "(Don't answer)");
            else if (currId == 1)
                loadText("Feminin voice", "That's none of your business, you're only a slave here.", "Sorry", "F*ck you", null, null);
            else if (currId == 2)
                loadText("Feminin voice", "You have to clean this room, eat all the wastes of magical rock so they will break up in your body.\n" + 
                    "We provided you an energy accumulator gun to help you, don't damage it.", "Isn't that dangerous ?", "Sure", null, null);
            else if (currId == 3)
                loadText("Feminin voice", "Who do you think you are ? Wake up!", "Sorry", "F*ck you", null, null);
            else if (currId == 4)
                loadText("Feminin voice", "Don't make me waking you up, you would regret it.", "Sorry", "F*ck you", null, null);
            else if (currId == 5)
                loadText("Feminin voice", "I don't think you understood your place here.", "...", null, null, null);
            else if (currId == 6)
                loadText("Feminin voice", "I wouldn't send someone as you if it was safe.", "Obviously", null, null, null);
        }
    }

    public void nextChoice(int idChoice)
    {
        if (currId == 0)
        {
            if (idChoice == 1) currId = 1;
            else if (idChoice == 2) currId = 2;
            else if (idChoice == 3) currId = 3;
            else if (idChoice == 4) currId = 4;
        }
        else if (currId == 1 || currId == 3 || currId == 4)
        {
            if (idChoice == 1) currId = 2;
            else if (idChoice == 2) currId = 5;
        }
        else if (currId == 2)
        {
            if (idChoice == 1) currId = 6;
            else if (idChoice == 2)
            {
                currId = 0;
                DialogBox.SetActive(false);
                pc.inIntro = false;
                md.move();
                return;
            }
        }
        else if (currId == 5)
        {
            currId = 0;
            DialogBox.SetActive(false);
            laser.move();
            return; 
        }
        else if (currId == 6)
        {
            currId = 0;
            DialogBox.SetActive(false);
            pc.inIntro = false;
            md.move();
            return;
        }
        launchDialog(e);
    }

    public void loadText(string authorM, string contentM, string text1, string text2, string text3, string text4)
    {
        speakName.text = authorM;
        content.text = contentM;
        if (text1 != null) { textChoice1.text = text1; goChoice1.SetActive(true); } else goChoice1.SetActive(false);
        if (text2 != null) { textChoice2.text = text2; goChoice2.SetActive(true); } else goChoice2.SetActive(false);
        if (text3 != null) { textChoice3.text = text3; goChoice3.SetActive(true); } else goChoice3.SetActive(false);
        if (text4 != null) { textChoice4.text = text4; goChoice4.SetActive(true); } else goChoice4.SetActive(false);
    }
}
