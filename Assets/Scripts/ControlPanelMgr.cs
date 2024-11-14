using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelMgr : MonoBehaviour, IDataPersistence
{
    public Slider mSlider;
    public Zombie[] zombies;

    public void LoadData(GameData data)
    {
        mSlider.value = data.zombieInfos[0].walkingSpeed;
    }

    public void SaveData(ref GameData data)
    {

    }

    public void OnSliderValueChange(float value)
    {
        foreach (Zombie zombie in zombies)
        {
            zombie.WalkingSpeed = value;
        }
    }
}
