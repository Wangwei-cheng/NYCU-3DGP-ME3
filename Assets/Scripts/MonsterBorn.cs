using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBorn : MonoBehaviour
{
    public GameObject target_monster;
    public int monster_max_num = 50;
    public float interval_time = 3;
    private int monster_num = 0;
    private GameObject target_player;

    void Start()
    {
        target_player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("CreateMonster", 1f, interval_time);
    }


    void Update()
    {

    }

    void CreateMonster()
    {
        if (target_player.GetComponent<Player>().isDead)
        {
            CancelInvoke();
        }
        else
        {
            Instantiate(target_monster, this.transform.position, Quaternion.identity);
            monster_num++;

            if (monster_num >= monster_max_num)
            {
                CancelInvoke();
            }
        }
    }
}