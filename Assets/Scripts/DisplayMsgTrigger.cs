using UnityEngine;
using UnityEngine.UI;

public class DisplayMsgTrigger : MonoBehaviour {

    private MeshRenderer text;
    private const float refTime = 3.0f;
    private float currTime;

    private void Start()
    {
        currTime = -1.0f;
        text = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (currTime >= refTime)
        {
            text.enabled = false;
            currTime = -1.0f;
        }
        else if (currTime >= 0.0f)
            currTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.enabled = true;
            currTime = -1.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            currTime = 0.0f;
    }
}
