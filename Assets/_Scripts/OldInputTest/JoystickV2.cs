using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JoystickV2 : MonoBehaviour
{
    public static JoystickV2 joystickV2Instance;
    private bool touchStart = false;
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 lastDirection;

    [SerializeField] private GameObject _buttonPosition;
    public GameObject circle;
    public GameObject outerCircle;

    private Vector3 joystickDefaultPosition;
    private Vector3 innerJoystickDefaultPosition;
    Vector3 direction;

    void Awake()
    {
        joystickV2Instance = this;
    }
    void Start()
    {
        joystickDefaultPosition = outerCircle.transform.position;
        innerJoystickDefaultPosition = circle.transform.position;
    }

    public Vector3 LastDirection
    {
        get {return lastDirection;}
    }
    public float LastDirectionY
    {

        set{lastDirection.y = value;}
    }
    public bool TouchStart
    {
       get{ return touchStart;}
    }
    public Vector3 Direction
    {
        get{return direction;}
    }

    Vector3 offset;

    Vector3 move;
    bool _touchButton = true;
    // List<touchId> _numberOfTouches = new List<touchId>();
    public List<touchId> touches = new List<touchId>();

    public class touchId
    {
        public int id;
        public Vector3 position;
        public touchId(int id, Vector3 position)
        {
            this.id = id;
            this.position = position;
        }
    }

    private int inputIndexTouch = 99;
    private bool _hasDraggedFromButton = false;
    int countTouch = 0;
    void Update () 
    {
#if UNITY_ANDROID
        countTouch = 0;
        while ( countTouch < Input.touchCount)
        {
            Debug.Log("daf");
            Touch touch = Input.GetTouch(countTouch);
                
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    
                    touches.Add(new touchId(touch.fingerId, touch.position));
                    
                    
                    touchId currentTouch = touches.Find(currTouch => currTouch.id == touch.fingerId);
                    
                    if (CheckTouchesPosition(currentTouch.position, _buttonPosition))
                    {
                        _hasDraggedFromButton = true;
                        
                        break;
                        
                    }
                    _hasDraggedFromButton = false;
                    pointA = currentTouch.position;
                    
                    outerCircle.transform.position = pointA;
                    
                    break;
                case TouchPhase.Moved:
                    
                    currentTouch = touches.Find(currTouch => currTouch.id == touch.fingerId);
                    
                    pointB = touch.position;
                    if (!CheckTouchesPosition(pointB, _buttonPosition) && !_hasDraggedFromButton)
                    {
                        
                        offset = pointB - pointA;
                        move = new Vector3(offset.x, 0, offset.y);
                        direction = Vector3.ClampMagnitude(move, 20.0f);
                        lastDirection = move.normalized;
                        circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.z);
                        touchStart = true;
                        break;
                    }
                    
                    break;
                case TouchPhase.Stationary:
                    // if (_hasDraggedFromButton)
                    // {
                    //     _hasDraggedFromButton = false;
                    //     break;
                    // }
                    break;
                case TouchPhase.Ended:
                    if (touches.Count == 1 )
                    {
                        outerCircle.transform.position = joystickDefaultPosition;
                        circle.transform.position = innerJoystickDefaultPosition;
                        lastDirection = Vector3.zero;
                        touchStart = false;

                        currentTouch = touches.Find(currTouch => currTouch.id == touch.fingerId);
                        touches.RemoveAt(touches.IndexOf(currentTouch));
                        break;
                    }
                    
                    _hasDraggedFromButton = false;
                    currentTouch = touches.Find(currTouch => currTouch.id == touch.fingerId);
                    touches.RemoveAt(touches.IndexOf(currentTouch));
                    break;

            }
            countTouch++;
        }
       
#endif
            
#if UNITY_EDITOR
            

            // if first touch is on button (in idle state):
            //     Do not move charater nor joystick
            //     Change current target
            // but if first touch is not on button:
            //       joystick and button normal behaviours
            
            // if moving and button is pressed
            //     Change target? or nothing happens
            // else if moving and new touch not on button:
            //     keep moving direction and change joystick position
            //     but if new touch has moved
            //         Change moving direction
            // if (CheckTouchesPosition(_numberOfTouches[0].position, _buttonPosition.transform.position))
            // {
            //     Debug.Log("Success?");
            //     return; // Avoids moving character and joystick
            // }
    

            
        

        // if ( Input.GetMouseButtonDown(0)) // Input.GetMouseButtonDown(0)   pointA != joystickDefaultPosition
        // {

        //     pointA = (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
           
           
        //     if (CheckTouchesPosition(pointA, _buttonPosition))
        //     {
        //         pointA = joystickDefaultPosition;
        //         _touchButton = false;

        //     }
        //     else
        //     {
        //         outerCircle.transform.position = pointA;
        //         _touchButton = true;
        //     }


            
        // }
        // else
        // {
        //     outerCircle.gameObject.transform.position = joystickDefaultPosition;
        //     circle.transform.position = innerJoystickDefaultPosition;
        //     lastDirection = Vector3.zero;

        // }
        // if(Input.GetMouseButton(0))
        // {
        //         outerCircle.transform.position = pointA;
        //         pointB = (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //         touchStart = true;
            
            
            
        // }
        // else
        // {
        //     touchStart = false;
        // }


        
        // if(touchStart && _touchButton)
        // {
        //     offset = pointB - pointA;

        //     move = new Vector3(offset.x, 0, offset.y);
        //     direction = Vector3.ClampMagnitude(move, 20.0f);
            
        //     lastDirection = move.normalized;
            
        //     circle.transform.position = new Vector2(pointA.x + direction.x , pointA.y + direction.z);
           
            
        // }
        // else
        // {
        //     outerCircle.gameObject.transform.position = joystickDefaultPosition;
        //     circle.transform.position = innerJoystickDefaultPosition;
        //     lastDirection = Vector3.zero;
        // }
# endif
    }


    private bool CheckTouchesPosition(Vector3 touchPosition, GameObject buttonPosition)
    {
        RectTransform rectTransform = buttonPosition.GetComponent<RectTransform>();
        float width = rectTransform.rect.width / 4;
        float heigth = rectTransform.rect.height / 4;
        Debug.Log(width + "   " + heigth);
        
        if (touchPosition == _buttonPosition.transform.position || (touchPosition.x <= _buttonPosition.transform.position.x + width && 
        touchPosition.x >= _buttonPosition.transform.position.x - width) 
        && (touchPosition.y <= _buttonPosition.transform.position.y + heigth && 
        touchPosition.y >= _buttonPosition.transform.position.y - heigth))
        {
           return true;
        }

        return false;
        
    }
}
