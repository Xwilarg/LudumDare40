﻿using UnityEngine;
using System.IO;

public class CountDeath : MonoBehaviour {
    
    public int nbDeath { set; get; }
    public int difficulty { set; get; }
    public int currLevel { set; get; }
    public int levelPlaying { set; get; }
    public int score { set; get; }
    public string ip { set; get; }
    public int port { set; get; }
    public bool doesHost { set; get; }

    public bool laserRobots { set; get; }
    public bool pushRobots { set; get; }
    public bool crates { set; get; }
    public bool shakePowerup { set; get; }
    public bool keyboardPowerup { set; get; }
    public bool magnetPowerup { set; get; }
    public bool cornerPowerup { set; get; }
    public bool gunPowerup { set; get; }
    public bool visionPowerup { set; get; }
    public bool duplicatePowerup { set; get; }
    public bool timerPowerup { set; get; }
    public bool trapPowerup { set; get; }

    void Start()
    {
        laserRobots = true;
        pushRobots = true;
        crates = true;
        shakePowerup = true;
        keyboardPowerup = true;
        magnetPowerup = true;
        cornerPowerup = true;
        gunPowerup = true;
        visionPowerup = true;
        duplicatePowerup = true;
        timerPowerup = true;
        trapPowerup = true;
        DontDestroyOnLoad(gameObject);
        nbDeath = 0;
        difficulty = 2;
        if (!File.Exists("Saves.dat"))
        {
            File.WriteAllText("Saves.dat", getFile(0));
            currLevel = 0;
        }
        else
        {
            string content = File.ReadAllText("Saves.dat");
            if (content == getFile(1)) currLevel = 1;
        }
    }

    public void increaseFile(int level)
    {
        string content = File.ReadAllText("Saves.dat");
        if (content != getFile(level))
            File.WriteAllText("Saves.dat", getFile(level));
    }

    string getFile(int lvl)
    {
        return ((lvl * 549 % 23 - lvl * 4) - 1).ToString();
    }
}
