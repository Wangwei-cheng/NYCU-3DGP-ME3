using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IDataPersistence
{
    public Animator zombieAnima;
    public Rigidbody zombieRigid;
    public Transform zombieTrans;
    public float walkingSpeed = 50;

    public ZombieState[] states;
    public int curState = 0;

    private bool walking = false;
    private float stopTime;
    private float curTime;
    private int totalStates;

    public void LoadData(GameData data)
    {
        this.walkingSpeed = data.zombieWalkingSpeed;
    }

    public void SaveData(ref GameData data)
    {
        data.zombieWalkingSpeed = this.walkingSpeed;
    }

    private void Start()
    {
        stopTime = Time.time;
        totalStates = states.Length;
    }

    private void FixedUpdate()
    {
        if (walking)
        {
            zombieRigid.velocity = transform.forward * walkingSpeed * Time.deltaTime;
        }
    }

    private void Update()
    {
        curTime = Time.time;

        if (!walking)
        {
            if (curTime - stopTime >= 2)
            {
                zombieTrans.Rotate(0, states[curState].rotatingDegree, 0);
                walking = true;
                zombieAnima.SetTrigger("Run");
                zombieAnima.ResetTrigger("Idle");
            }
        }
        if (walking)
        {
            if (zombieTrans.position.x * states[curState].xDirection >= states[curState].goal.x * states[curState].xDirection &&
                zombieTrans.position.z * states[curState].zDirection >= states[curState].goal.z * states[curState].zDirection)
            {
                walking = false;
                zombieTrans.position = new Vector3(states[curState].goal.x, transform.position.y, states[curState].goal.z);
                stopTime = Time.time;
                curState = (curState + 1)%totalStates;
                zombieAnima.ResetTrigger("Run");
                zombieAnima.SetTrigger("Idle");
            }
        }
    }

    public void SetWalkingSpeed(float speed)
    {
        walkingSpeed = speed;
    }
}
