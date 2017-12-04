using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

    CountDeath cd;
    public Button laserRobots, pushRobots, crates, shakePowerup, keyboardPowerup, magnetPowerup, cornerPowerup, gunPowerup, visionPowerup;

    public void switchLaser()
    {
        cd.laserRobots = !cd.laserRobots;
        laserRobots.colors = setBlockColor(laserRobots.colors, (cd.laserRobots) ? (Color.green) : (Color.red));
    }

    public void switchPush()
    {
        cd.pushRobots = !cd.pushRobots;
        pushRobots.colors = setBlockColor(pushRobots.colors, (cd.pushRobots) ? (Color.green) : (Color.red));
    }

    public void switchCrates()
    {
        cd.crates = !cd.crates;
        crates.colors = setBlockColor(crates.colors, (cd.crates) ? (Color.green) : (Color.red));
    }

    public void switchShake()
    {
        cd.shakePowerup = !cd.shakePowerup;
        shakePowerup.colors = setBlockColor(shakePowerup.colors, (cd.shakePowerup) ? (Color.green) : (Color.red));
    }

    public void switchKeyboard()
    {
        cd.keyboardPowerup = !cd.keyboardPowerup;
        keyboardPowerup.colors = setBlockColor(keyboardPowerup.colors, (cd.keyboardPowerup) ? (Color.green) : (Color.red));
    }

    public void switchMagnet()
    {
        cd.magnetPowerup = !cd.magnetPowerup;
        magnetPowerup.colors = setBlockColor(magnetPowerup.colors, (cd.magnetPowerup) ? (Color.green) : (Color.red));
    }

    public void switchCorner()
    {
        cd.cornerPowerup = !cd.cornerPowerup;
        cornerPowerup.colors = setBlockColor(cornerPowerup.colors, (cd.cornerPowerup) ? (Color.green) : (Color.red));
    }

    public void switchGun()
    {
        cd.gunPowerup = !cd.gunPowerup;
        gunPowerup.colors = setBlockColor(gunPowerup.colors, (cd.gunPowerup) ? (Color.green) : (Color.red));
    }

    public void switchVision()
    {
        cd.visionPowerup = !cd.visionPowerup;
        visionPowerup.colors = setBlockColor(visionPowerup.colors, (cd.visionPowerup) ? (Color.green) : (Color.red));
    }

    private ColorBlock setBlockColor(ColorBlock cb, Color color)
    {
        cb.normalColor = color;
        cb.pressedColor = color;
        cb.highlightedColor = color;
        return (cb);
    }

    void Start ()
    {
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        laserRobots.colors = setBlockColor(laserRobots.colors, (cd.laserRobots) ? (Color.green) : (Color.red));
        pushRobots.colors = setBlockColor(pushRobots.colors, (cd.pushRobots) ? (Color.green) : (Color.red));
        crates.colors = setBlockColor(crates.colors, (cd.crates) ? (Color.green) : (Color.red));
        shakePowerup.colors = setBlockColor(shakePowerup.colors, (cd.shakePowerup) ? (Color.green) : (Color.red));
        keyboardPowerup.colors = setBlockColor(keyboardPowerup.colors, (cd.keyboardPowerup) ? (Color.green) : (Color.red));
        magnetPowerup.colors = setBlockColor(magnetPowerup.colors, (cd.magnetPowerup) ? (Color.green) : (Color.red));
        cornerPowerup.colors = setBlockColor(cornerPowerup.colors, (cd.cornerPowerup) ? (Color.green) : (Color.red));
        gunPowerup.colors = setBlockColor(gunPowerup.colors, (cd.gunPowerup) ? (Color.green) : (Color.red));
        visionPowerup.colors = setBlockColor(visionPowerup.colors, (cd.visionPowerup) ? (Color.green) : (Color.red));
    }
}
