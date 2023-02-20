using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuOption : MonoBehaviour
{
    public Transform dropDown;
    public GameObject _light;

    void Update() //UI를 다룰 때는 FixedUpdate, StartCorutine을 쓰지 않는다.
    {
        if (dropDown.GetComponent<Dropdown>().value == 0) //드롭다운 UI 클릭에 따른 그림자 조정
        {
            _light.GetComponent<Light>().shadows = LightShadows.Soft;
        }
        if (dropDown.GetComponent<Dropdown>().value == 1)
        {
            _light.GetComponent<Light>().shadows = LightShadows.Hard;
        }
        if (dropDown.GetComponent<Dropdown>().value == 2)
        {
            _light.GetComponent<Light>().shadows = LightShadows.None;
        }
    }
}
