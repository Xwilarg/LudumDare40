using UnityEngine;

public class GenerateRandomly : MonoBehaviour {

    private int diff;
    public GameObject gameOver;
    public GameObject player;
    public GameObject robotMove, robotFreeze, wall, box, objective, laser;
    CountDeath cd;
    struct Item
    {
        public GameObject go { set; get; }
        public int supp { set; get; }
    }
    Item[,] map;
    const int maxY = 19 - 4 - 1;

    private bool isAreaFree(Vector2 pos)
    {
        for (int i = 0; i < 2; i++)
        {
            for (int y = 0; y < 2; y++)
            {
                if (pos.x + i >= 0 && pos.x + i < 30
                    && pos.y + y >= 0 && pos.y + y < maxY
                    && map[(int)pos.x + i, (int)pos.y + y].go != null)
                    return (false);
            }
        }
        return (true);
    }

    void Start()
    {
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        diff = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().difficulty;
        map = new Item[30, maxY];
        for (int i = 0; i < 30; i++)
        {
            for (int y = 0; y < maxY; y++)
            {
                map[i, y].go = null;
                map[i, y].supp = 0;
            }
        }
        int maxMove;
        if (cd.laserRobots)
        {
            if (diff == 1) maxMove = 1;
            else if (diff == 2) maxMove = 1;
            else if (diff == 3) maxMove = Random.Range(1, 3);
            else throw new System.Exception("diff have an invalid value.");
        }
        else maxMove = 0;
        int maxFreeze;
        if (cd.pushRobots)
        {
            if (diff == 1) maxFreeze = 1;
            else if (diff == 2) maxFreeze = Random.Range(1, 3);
            else if (diff == 3) maxFreeze = Random.Range(2, 4);
            else throw new System.Exception("diff have an invalid value.");
        }
        else
            maxFreeze = 0;
        int maxWall;
        if (diff == 1) maxWall = Random.Range(5, 11);
        else if (diff == 2) maxWall = Random.Range(10, 16);
        else if (diff == 3) maxWall = Random.Range(15, 21);
        else throw new System.Exception("diff have an invalid value.");
        int maxCrate;
        if (cd.crates)
        {
            if (diff == 1) maxCrate = Random.Range(1, 4);
            else if (diff == 2) maxCrate = Random.Range(2, 5);
            else if (diff == 3) maxCrate = Random.Range(3, 7);
            else throw new System.Exception("diff have an invalid value.");
        }
        else
            maxCrate = 0;
        int chanceSpawn = 50;
        for (int i = 0; i < maxWall; i++)
        {
            int xPos, yPos;
            do
            {
                xPos = Random.Range(0, 30);
                yPos = Random.Range(0, maxY);
            } while (map[xPos, yPos].go != null || !isAreaFree(new Vector2(xPos, yPos)));
            map[xPos, yPos].go = wall;
        }
        int nbLasers = maxWall;
        for (int i = 0; i < 30; i++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (map[i, y].go != null)
                {
                    if (map[i, y].go == wall)
                    {
                        Vector2[] allPos = new Vector2[4] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1) };
                        bool[] doesRot = new bool[4] { false, true, true, false };
                        Vector2 currPos = new Vector2(i, y);
                        if (Random.Range(0, 100) < chanceSpawn || nbLasers + maxWall / 3 < maxWall)
                        {
                            nbLasers--;
                            int z = Random.Range(0, 4);
                            if (isDirectionWall(currPos + allPos[z], allPos[z]))
                                addLasers(currPos + allPos[z], allPos[z], doesRot[z]);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < maxCrate; i++)
        {
            int xPos, yPos;
            do
            {
                xPos = Random.Range(0, 30);
                yPos = Random.Range(0, maxY);
            } while (map[xPos, yPos].go != null);
            map[xPos, yPos].go = box;
        }
        for (int i = 0; i < maxMove; i++)
        {
            int xPos, yPos;
            do
            {
                xPos = Random.Range(0, 30);
                yPos = Random.Range(0, maxY);
            } while (map[xPos, yPos].go != null);
            map[xPos, yPos].go = robotMove;
        }
        for (int i = 0; i < maxFreeze; i++)
        {
            int xPos, yPos;
            do
            {
                xPos = Random.Range(2, 30 - 2);
                yPos = Random.Range(2, maxY - 2);
            } while (map[xPos, yPos].go != null);
            map[xPos, yPos].go = robotFreeze;
        }
        for (int i = 0; i < 8; i++)
        {
            int xPos, yPos;
            do
            {
                xPos = Random.Range(0, 30);
                yPos = Random.Range(0, maxY);
            } while (map[xPos, yPos].go != null);
            map[xPos, yPos].go = objective;
        }
        int nbShake = 0;
        int nbKeyboard = 0;
        int nbAim = 0;
        int nbCorner = 0;
        int nbGun = 0;
        int nbVision = 0;
        int nbDuplicate = 0;
        int nbTimer = 0;
        int nbTrap = 0;
        for (int i = 0; i < 30; i++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (map[i, y].go != null)
                {
                    GameObject go;
                    if (map[i, y].go == laser && map[i, y].supp == 2)
                    {
                        go = Instantiate(map[i, y].go, new Vector2(i * 0.5f + -7.22f, y * -0.5f + 4.49f), Quaternion.Euler(0, 0, 90));
                    }
                    else if (map[i, y].go == laser && map[i, y].supp == 3)
                    {
                        go = Instantiate(map[i, y].go, new Vector2(i * 0.5f + -7.22f, y * -0.5f + 4.49f), Quaternion.Euler(0, 0, 90));
                        GameObject go2 = Instantiate(map[i, y].go, new Vector2(i * 0.5f + -7.22f, y * -0.5f + 4.49f), Quaternion.identity);
                        go2.GetComponent<MoveLaser>().pc = player.GetComponent<PlayerController>();
                        go2.GetComponent<MoveLaser>().gameOver = gameOver;
                    }
                    else
                    {
                        go = Instantiate(map[i, y].go, new Vector2(i * 0.5f + -7.22f, y * -0.5f + 4.49f), Quaternion.identity);
                    }
                    if (map[i, y].go == laser)
                    {
                        go.GetComponent<MoveLaser>().pc = player.GetComponent<PlayerController>();
                        go.GetComponent<MoveLaser>().gameOver = gameOver;
                    }
                    else if (map[i, y].go == objective)
                    {
                        if (!cd.shakePowerup && !cd.keyboardPowerup && !cd.magnetPowerup && !cd.cornerPowerup && !cd.gunPowerup && !cd.visionPowerup && !cd.duplicatePowerup && !cd.timerPowerup && !cd.trapPowerup)
                            map[i, y].go.GetComponent<PowerDown>().pde = (PowerDown.powerDownE)5; // None
                        else
                        {
                            int randomNb;
                            int it = 0;
                            do
                            {
                                randomNb = Random.Range(0, 9);
                                if (randomNb == 1) randomNb = 3;
                                else if (randomNb == 2) randomNb = 4;
                                else if (randomNb == 3) randomNb = 1;
                                else if (randomNb == 4) randomNb = 6;
                                else if (randomNb == 5) randomNb = 7;
                                else if (randomNb == 6) randomNb = 8;
                                else if (randomNb == 7) randomNb = 9;
                                else if (randomNb == 8) randomNb = 10;
                                it++;
                                if (it == 100) { randomNb = 5; break; }
                            } while ((randomNb == 0 && !cd.shakePowerup) || (randomNb == 3 && !cd.keyboardPowerup)
                            || (randomNb == 4 && !cd.magnetPowerup) || (randomNb == 1 && !cd.cornerPowerup) || (randomNb == 6 && !cd.gunPowerup) || (randomNb == 7 && !cd.visionPowerup) || (randomNb == 8 && !cd.duplicatePowerup)
                            || (randomNb == 9 && !cd.timerPowerup) || (randomNb == 10 && !cd.trapPowerup)
                            || (randomNb == 0 && nbShake >= 4) || (randomNb == 3 && nbKeyboard >= 2) || (randomNb == 4 && nbAim >= 4) || (randomNb == 1 && nbCorner >= 4)
                            || (randomNb == 6 && nbGun >= 1) || (randomNb == 7 && nbVision >= 1) || (randomNb == 8 && nbDuplicate >= 1) || (randomNb == 9 && nbTimer >= 1) || (randomNb == 10 && nbTrap >= 4));
                            if (randomNb == 0) nbShake++;
                            else if (randomNb == 3) nbKeyboard++;
                            else if (randomNb == 4) nbAim++;
                            else if (randomNb == 1) nbCorner++;
                            else if (randomNb == 6) nbGun++;
                            else if (randomNb == 7) nbVision++;
                            else if (randomNb == 8) nbDuplicate++;
                            else if (randomNb == 9) nbTimer++;
                            else if (randomNb == 10) nbTrap++;
                            map[i, y].go.GetComponent<PowerDown>().pde = (PowerDown.powerDownE)randomNb;
                        }
                    }
                    else if (map[i, y].go == robotMove)
                    {
                        go.GetComponent<KillPlayer>().player = player;
                        go.GetComponentInChildren<MoveLaser>().pc = player.GetComponent<PlayerController>();
                        go.GetComponentInChildren<MoveLaser>().gameOver = gameOver;
                    }
                    else if (map[i, y].go == robotFreeze)
                        go.GetComponent<LaunchCrate>().player = player;
                }
            }
        }
    }

    private bool isDirectionWall(Vector2 pos, Vector2 dir)
    {
        if (pos.y == maxY - 1 && pos.x >= 10 && pos.x <= 15) return (false);
        if (pos.x < 0 || pos.x >= 30 || pos.y < 0 || pos.y >= maxY) return (true);
        if (map[(int)pos.x, (int)pos.y].go == wall) return (true);
        return (isDirectionWall(pos + dir, dir));
    }

    private void addLasers(Vector2 pos, Vector2 dir, bool doesRot)
    {
        const int distance = 3;
        if (pos.x + dir.x * distance >= 30 || pos.x + dir.x * distance < 0
            || pos.y + dir.y * distance >= maxY || pos.y + dir.y * distance < 0)
        {
            if (pos.x >= 0 && pos.x < 30 && pos.y >= 0 && pos.y < maxY && map[(int)pos.x, (int)pos.y].go == null)
                map[(int)pos.x, (int)pos.y].go = wall;
            return;
        }
        if (pos.x < 0 || pos.x >= 30 || pos.y < 0 || pos.y >= maxY)
            return;
        if (map[(int)pos.x, (int)pos.y].go != wall)
        {
            map[(int)pos.x, (int)pos.y].go = laser;
            if (doesRot)
            {
                if (map[(int)pos.x, (int)pos.y].supp == 1)
                    map[(int)pos.x, (int)pos.y].supp = 3;
                else
                    map[(int)pos.x, (int)pos.y].supp = 2;
            }
            else
            {
                if (map[(int)pos.x, (int)pos.y].supp == 2)
                    map[(int)pos.x, (int)pos.y].supp = 3;
                else
                    map[(int)pos.x, (int)pos.y].supp = 1;
            }
            if (map[(int)pos.x, (int)pos.y].go != wall)
                addLasers(pos + dir, dir, doesRot);
        }
    }
}
