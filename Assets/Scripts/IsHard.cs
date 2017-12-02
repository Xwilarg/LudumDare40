using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHard : MonoBehaviour {
    
	void Start () {
        if (GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().difficulty == 3)
            gameObject.SetActive(false);

    }
}
