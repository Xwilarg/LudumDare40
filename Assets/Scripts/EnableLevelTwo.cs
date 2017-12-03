using UnityEngine;
using UnityEngine.UI;

public class EnableLevelTwo : MonoBehaviour {
    
	void Start () {
        Button b = GetComponent<Button>();
        Text t = GetComponentInChildren<Text>();
        if (GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().currLevel != 1)
        {
            t.text = "Finish level 1 before";
            b.interactable = false;
        }
        else
        {
            t.text = "Level 2";
            b.interactable = true;
        }
	}
}
