using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad_E : MonoBehaviour
{
    [SerializeField]
    private RectTransform touchPad;
    private int _touchID = -1;
    private Vector3 _StartPos = Vector3.zero;
    private float _dragRadius = 90f;
    private bool _buttonPressed;
    private PlayerMovement_E playerMovementE;
    void Start()
    {
        _buttonPressed = false;
        touchPad = GetComponent<RectTransform>();
        _StartPos = touchPad.position;
        playerMovementE = GameObject.FindWithTag("Player").GetComponent<PlayerMovement_E>();
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

    void HandleTouchInput()
    {
        int i = 0;
        if(Input.touchCount > 0)
        {
            foreach(Touch _touch in Input.touches)
            {
                i++;
                Vector3 touchPos = new Vector3(_touch.position.x, _touch.position.y);
                if (_touch.phase == TouchPhase.Began)
                {
                    if (_touch.position.x <= (_StartPos.x + _dragRadius))
                        _touchID = i;

                    if (_touch.position.y <= (_StartPos.y + _dragRadius))
                        _touchID = i;
                }
                if(_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Stationary)
                {
                    if(_touchID == i)
                        HandleInput(touchPos);
                }
                if(_touch.phase == TouchPhase.Ended)
                {
                    if(_touchID == i)
                        _touchID = -1;
                }
            }
        }
    }
    
    void HandleInput(Vector3 input)
    {
        if(_buttonPressed)
        {
            Vector3 diffVector = (input - _StartPos);
            if (diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                diffVector.Normalize();
                touchPad.position = _StartPos + diffVector * _dragRadius;
                Debug.Log(_StartPos + diffVector);
            }
            else
                touchPad.position = input;

        }
        else
        {
            touchPad.position = _StartPos;
        }
        Vector3 diff = touchPad.position - _StartPos;
        Vector3 normalDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);
        if(playerMovementE != null)
        {
            playerMovementE.OnStickChanged(normalDiff);
        }
    }
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            HandleTouchInput();
        }
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            HandleInput(Input.mousePosition);
        }
    }
}
