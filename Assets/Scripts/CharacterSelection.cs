using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public void ChangeCharacter(int characterNum)
    {
        GameMgr.Instance.ChangeCurCharacter(characterNum);
    }
}
