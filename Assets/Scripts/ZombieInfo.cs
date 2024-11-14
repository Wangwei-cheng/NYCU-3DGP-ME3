using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZombieInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public int curState;
    public float walkingSpeed;
    public bool isWalking;
}
