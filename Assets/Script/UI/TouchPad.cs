using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    [SerializeField]
    private RectTransform touchPad;//캔버스 안에 잇는 유아이는 모두 RectTransform임 위치,좌표,크기
    private int _touchID = -1; //터치를 했는가 ? -1 : NO. 그 이외의 0~양수는 터치를 했다고 판단
    private Vector3 _StartPos = Vector3.zero;//터치패드 시작 지점
    public float _dragRadius = 90f;//반경을 90으로잡음
    private bool _buttonPressed = false;//버튼을 눌렀는가?
    private PlayerMovement_E playerMovement;//오브젝트의 스크립트 중 OnStickChanged를 넘겨야함
    void Start()
    {
        touchPad = GetComponent<RectTransform>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement_E>();
        _StartPos = touchPad.position;
    }

    public void ButtonDown()
    {
        _buttonPressed = true;
    }
    public void ButtonUp()
    {
        _buttonPressed = false;
        HandleInput(_StartPos);
    }
    void HandleTouchInput()//모바일터치용 함수
    {
        int i = 0;//몇 번 카운트 되는 지 자동으로 카운트가 됨.
        if(Input.touchCount > 0)//손가락 터치를 한 번이라도 했다면.
        {//카운트 된 것을 배열에 담음
            foreach(Touch _touch in Input.touches)
            {
                i++;
                Vector3 touchPos = new Vector3(_touch.position.x, _touch.position.y);
                if(_touch.phase == TouchPhase.Began) //터치 유형이 시작과 같다면
                {
                    if(_touch.position.x <= (_StartPos.x + _dragRadius))
                    {
                        _touchID = i;
                    }
                    if (_touch.position.y <= (_StartPos.y + _dragRadius))
                    {
                        _touchID = i;
                    }
                }
                if(_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary)
                {       //터치유형이 움직이고 있거나 멈춰있다면.(어쨌든간에 누르고는있음)
                    if(_touchID == i)
                    {
                        HandleInput(touchPos);
                    }
                }
                if(_touch.phase == TouchPhase.Ended)
                {
                    if(_touchID == i)
                    {
                        _touchID = -1;
                    }
                }
            }
        }
    }
    void HandleInput(Vector3 input)//마우스 클릭으로 터치하는 함수
    {
        if(_buttonPressed)
        {
            Vector3 diffVector = (input - _StartPos); //거리가 나옴
            if(diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {   //터치 지점이 원의 넓이를 넘어갔다면 ?
                diffVector.Normalize(); //정규화 작업을 가져야 그 방향으로 움직일 수 있다.
                                        //방향을 유니티에 등록한다.
                touchPad.position = _StartPos + diffVector * _dragRadius;//터치패드가 넘어가도 유지. 원 밖으로 안나감.
                Debug.Log(_StartPos + diffVector);
            }
            else
            {
                touchPad.position = input;
            }
        }
        else
        {
            touchPad.position = _StartPos;
        }
        Vector3 diff = touchPad.position - _StartPos;
        Vector3 normalDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);
                        //거리를 구한 값에서 원의 반지름을 나누면 방향이 구해짐.
        if(playerMovement != null) //playerMovement가 잇다면.
        {
            playerMovement.OnStickChanged(normalDiff);//플레이무브먼트 스크립트로 인자 전달
            //그 뒤에 FixedUpdata 실행
        }
    }

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)//실시간 플랫폼이 안드로이드와 같다면
        {
            HandleTouchInput();
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor)//윈도우에 있는 응용프로그램을 통해서. 중요!!!
        {
            HandleInput(Input.mousePosition);//마우스의 움직임을 봐야하니까.
        }


    }
}
