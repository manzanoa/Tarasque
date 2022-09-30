using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //TODO:
    //-Make a setup method to setup UI at the start of the battle
    [SerializeField] Slider UIBar;

    public void Setup(int max, int v)
    {
        UIBar.maxValue = max;
        UIBar.value = v;
    }


}