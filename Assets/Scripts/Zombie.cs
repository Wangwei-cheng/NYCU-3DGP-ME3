using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour, IDataPersistence
{
    public Animator zombieAnimator;
    public Rigidbody zombieRigid;
    public Transform zombieTrans;

    public int id;
    public ZombieState[] states;
    public int curState = 0;

    private float walkingSpeed;
    public float WalkingSpeed
    {
        get { return walkingSpeed; }
        set
        {
            walkingSpeed = value;
            if (walkingSpeed < 0)       walkingSpeed = 0;
            else if(walkingSpeed > 200) walkingSpeed = 200;
        }
    }
    private bool isWalking = false;
    private float stopTime;
    private float curTime;
    private int totalStates;

    private AnimatorClipInfo[] curClipInfo;

    public void LoadData(GameData data)
    {
        this.transform.position = data.zombieInfos[id].position;
        this.transform.rotation = data.zombieInfos[id].rotation;
        curState = data.zombieInfos[id].curState;
        WalkingSpeed = data.zombieInfos[id].walkingSpeed;
        isWalking = data.zombieInfos[id].isWalking;
    }

    public void SaveData(ref GameData data)
    {
        data.zombieInfos[id].position = this.transform.position;
        data.zombieInfos[id].rotation = this.transform.rotation;
        data.zombieInfos[id].curState = curState;
        data.zombieInfos[id].walkingSpeed = WalkingSpeed;
        data.zombieInfos[id].isWalking = isWalking;
    }

    private void Start()
    {
        stopTime = Time.time;
        totalStates = states.Length;
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            zombieRigid.velocity = transform.forward * WalkingSpeed * Time.deltaTime;
        }
    }

    private void Update()
    {
        curTime = Time.time;
        curClipInfo = zombieAnimator.GetCurrentAnimatorClipInfo(0);

        if (!isWalking)
        {
            if (!string.Equals(curClipInfo[0].clip.name, "Idle"))
            {
                zombieAnimator.ResetTrigger(curClipInfo[0].clip.name);
                zombieAnimator.SetTrigger("Idle");
            }
            if (curTime - stopTime >= 2)
            {
                zombieTrans.Rotate(0, states[curState].rotatingDegree, 0);
                isWalking = true;
            }
        }
        if (isWalking)
        {
            if (!string.Equals(curClipInfo[0].clip.name, "Run"))
            {
                zombieAnimator.ResetTrigger(curClipInfo[0].clip.name);
                zombieAnimator.SetTrigger("Run");
            }
            if (zombieTrans.position.x * states[curState].xDirection >= states[curState].goal.x * states[curState].xDirection &&
                zombieTrans.position.z * states[curState].zDirection >= states[curState].goal.z * states[curState].zDirection)
            {
                isWalking = false;
                zombieTrans.position = new Vector3(states[curState].goal.x, transform.position.y, states[curState].goal.z);
                stopTime = Time.time;
                curState = (curState + 1)%totalStates;
            }
        }
    }
}
