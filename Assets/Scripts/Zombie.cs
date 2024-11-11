using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Animator zombie_anima;
    public Rigidbody zombie_rigid;
    public Transform zombie_trans;
    public float walk_speed = 50;

    public ZombieState[] states;
    public int curState = 0;

    private bool walking = false;
    private float stopTime;
    private float curTime;
    private int totalStates;

    private void Start()
    {
        stopTime = Time.time;
        totalStates = states.Length;
    }

    private void FixedUpdate()
    {
        if (walking)
        {
            zombie_rigid.velocity = transform.forward * walk_speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        curTime = Time.time;

        if (!walking)
        {
            if (curTime - stopTime >= 2)
            {
                zombie_trans.Rotate(0, states[curState].rotatingDegree, 0);
                walking = true;
                zombie_anima.SetTrigger("Run");
                zombie_anima.ResetTrigger("Idle");
            }
        }
        if (walking)
        {
            if (zombie_trans.position.x * states[curState].xDirection >= states[curState].goal.x * states[curState].xDirection &&
                zombie_trans.position.z * states[curState].zDirection >= states[curState].goal.z * states[curState].zDirection)
            {
                walking = false;
                zombie_trans.position = new Vector3(states[curState].goal.x, transform.position.y, states[curState].goal.z);
                stopTime = Time.time;
                curState = (curState + 1)%totalStates;
                zombie_anima.ResetTrigger("Run");
                zombie_anima.SetTrigger("Idle");
            }
        }
    }
}
