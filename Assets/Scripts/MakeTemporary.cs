using UnityEngine;
using UnityEngine.UI;

public class MakeTemporary : MonoBehaviour {

    private const float refTime = 5.0f;
    private float currTime;
    public Text content;

    private void Start()
    {
        currTime = 0f;
    }

    public void reset(string toDisplay)
    {
        currTime = 0f;
        content.text = toDisplay;
    }

	private void Update () {
        currTime += Time.deltaTime;
        if (currTime > refTime)
        {
            gameObject.SetActive(false);
        }
	}
}
