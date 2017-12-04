using UnityEngine;
using UnityEngine.UI;

public class DisplayMsgTrigger : MonoBehaviour {

    private MeshRenderer text;

    private void Start()
    {
        text = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            text.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            text.enabled = false;
    }
}
