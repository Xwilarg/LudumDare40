using UnityEngine;

public class CountDeath : MonoBehaviour {
    
    public int nbDeath { set; get; }

	void Start () {
        DontDestroyOnLoad(gameObject);
        nbDeath = 0;
    }
}
