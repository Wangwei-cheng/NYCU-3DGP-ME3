using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public int curCharacter;
    private static GameMgr instance;
    public static GameMgr Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        curCharacter = 0;
    }

    public void ChangeCurCharacter(int num)
    {
        curCharacter = num;
        Player.Instance.ChangeCharacter(curCharacter);
    }

    public int GetCurCharacter()
    {
        return curCharacter;
    }
}
