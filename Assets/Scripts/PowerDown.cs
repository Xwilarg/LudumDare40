using UnityEngine;

public class PowerDown : MonoBehaviour {

	public enum powerDownE
    {
        addForce,
        darker,
        premium,
        changeControl
    }

    public Vector3 help;

    private void Start()
    {
        int diff = 3 - GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().difficulty;
        transform.position += help * diff * .1f;
    }

    public powerDownE pde;
}
