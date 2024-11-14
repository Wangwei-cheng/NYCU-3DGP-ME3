using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    public ZombieInfo[] zombieInfos;

    public GameData()
    {
        playerPosition = new Vector3(0, 1, 0);
        playerRotation = Quaternion.Euler(0, 90, 0);

        zombieInfos = new ZombieInfo[2];
        for (int i = 0; i < 2; i++)
        {
            zombieInfos[i] = new ZombieInfo();
            zombieInfos[i].rotation = Quaternion.Euler(0, -90, 0);
            zombieInfos[i].curState = 0;
            zombieInfos[i].walkingSpeed = 100;
            zombieInfos[i].isWalking = false;
        }

        zombieInfos[0].position = new Vector3(20, 1, -23);
        zombieInfos[1].position = new Vector3(20, 1, -8);
    }
}
