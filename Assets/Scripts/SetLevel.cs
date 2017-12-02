using UnityEngine;
using UnityEngine.UI;

public class SetLevel : MonoBehaviour {

    public Image b1, b2, b3;
    private CountDeath cd;

    private void Start()
    {
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        setDiff(cd.difficulty);
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
