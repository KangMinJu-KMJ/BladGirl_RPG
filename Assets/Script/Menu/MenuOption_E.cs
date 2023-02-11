using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuOption_E : MonoBehaviour
{
    public Transform dropDown;
    public GameObject _light;

    void Update() //UI 다룰 때는 FixedUpdate, StartCorutine 안씀.
    {
        if (dropDown.GetComponent<Dropdown>().value == 0)//0 : softShadow라고 적었던 그 부분.
        {
            _light.GetComponent<Light>().shadows = LightShadows.Soft;
        }
        if (dropDown.GetComponent<Dropdown>().value == 1)//0 : softShadow라고 적었던 그 부분.
        {
            _light.GetComponent<Light>().shadows = LightShadows.Hard;
        }
        if (dropDown.GetComponent<Dropdown>().value == 2)//0 : softShadow라고 적었던 그 부분.
        {
            _light.GetComponent<Light>().shadows = LightShadows.None;
        }
    }
}
