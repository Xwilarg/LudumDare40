using UnityEngine;
using UnityEngine.UI;

public class DisableWebGL : MonoBehaviour {

    public GameObject warning;

	void Start () {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            warning.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
