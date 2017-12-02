using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	
	void Update () {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        if (pos.x < -3f) pos.x = -3f;
        else if (pos.x > 3f) pos.x = 3f;
        transform.position = pos;
    }
}
