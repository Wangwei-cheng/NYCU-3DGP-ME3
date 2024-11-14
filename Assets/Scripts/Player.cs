using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
    public Character[] characters;
    
    public Rigidbody player_rigid;
    public float walk_speed, walkback_speed, jog_speed, sprint_speed, rotate_speed;
    public bool walking;
    public Transform player_trans;
    public bool isDead = false;

    private int curCharacterNum;
    private Animator player_anima;

    private static Player instance;
    public static Player Instance { get { return instance; } }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.transform.rotation = data.playerRotation;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerRotation = this.transform.rotation;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        curCharacterNum = GameMgr.Instance.GetCurCharacter();
        ChangeCharacter(curCharacterNum);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            player_rigid.velocity = transform.forward * walk_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player_rigid.velocity = -transform.forward * walk_speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player_anima.SetTrigger("Walk");
            player_anima.ResetTrigger("Idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            player_anima.ResetTrigger("Walk");
            player_anima.SetTrigger("Idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player_anima.SetTrigger("Walk Backward");
            player_anima.ResetTrigger("Idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            player_anima.ResetTrigger("Walk Backward");
            player_anima.SetTrigger("Idle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            player_trans.Rotate(0, -rotate_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player_trans.Rotate(0, rotate_speed * Time.deltaTime, 0);
        }
        if(walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walk_speed += sprint_speed;
                player_anima.SetTrigger("Run");
                player_anima.ResetTrigger("Walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walk_speed = jog_speed;
                player_anima.ResetTrigger("Run");
                player_anima.SetTrigger("Walk");
            }
        }
    }

    public void ChangeCharacter(int characterNum)
    {
        characters[curCharacterNum].model.SetActive(false);
        curCharacterNum = characterNum;
        characters[curCharacterNum].model.SetActive(true);
        player_anima = characters[curCharacterNum].animator;
    }
}
