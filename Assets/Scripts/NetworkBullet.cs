using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBullet : MonoBehaviour {

	public void launch (GameObject player) {
        GetComponent<Rigidbody2D>().AddForce(player.transform.up * 1000, ForceMode2D.Impulse);
    }
}
