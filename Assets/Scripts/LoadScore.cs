using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour {

	private void Start()
    {
        GetComponent<Text>().text = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().score.ToString();
    }
}
