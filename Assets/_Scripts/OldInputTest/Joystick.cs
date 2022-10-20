using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float playerSpeed = 5.0f;
    private bool touchStart = false;
    private Vector3 pointA;
    private Vector3 pointB;

    public GameObject circle;
    public GameObject outerCircle;

    private Vector3 joystickDefaultPosition;
    private Vector3 innerJoystickDefaultPosition;

    private CharacterController characterController;
	// Update is called once per frame
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        joystickDefaultPosition = outerCircle.transform.position;
        innerJoystickDefaultPosition = circle.transform.position;
    }
	void Update () {




        if(Input.GetMouseButtonDown(0))
        {
            pointA = (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            //circle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            
        }
        if(Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        
        else
        {
            touchStart = false;
        }
        
        if(touchStart)
        {
            if (pointA != Vector3.zero)
            {
                Vector3 offset = pointB - pointA;
                Vector3 move = new Vector3(offset.x, 0, offset.y);
                Vector3 direction = Vector3.ClampMagnitude(move, 20.0f);
                moveCharacter(direction);
            
                m_latestDirection = Quaternion.AngleAxis(90, Vector3.right) * offset;
                m_latestDirection.y = transform.position.y;
                //RotateCharacter(m_latestDirection * -1);
                transform.LookAt(m_latestDirection, Vector3.up);


            // outerCircle.transform.position = new Vector2(pointA.x , pointA.y - 50);
                circle.transform.position = new Vector2(pointA.x + direction.x , pointA.y + direction.z);
                //circle.GetComponent<RectTransform>().position = new Vector2(pointA.x + direction.x, pointA.y + direction.y) ;
            }
            else
            {
                m_latestDirection = Vector3.zero;
            }
        }
        else
        {
            outerCircle.gameObject.transform.position = joystickDefaultPosition;
            circle.transform.position = innerJoystickDefaultPosition;
        }
	}
    Vector3 m_latestDirection;
	
	void moveCharacter(Vector3 direction)
    {
        
       
        characterController.Move(direction.normalized * Time.deltaTime * playerSpeed);
    }

    protected void RotateCharacter(Vector3 lookAt)
    {

        Vector3 forwardVector = this.transform.forward;
        
        Vector3 differenceVector = (transform.position - lookAt).normalized;

      
        float unityAngle = Vector3.SignedAngle(forwardVector, differenceVector, this.transform.up);

       
        this.transform.Rotate(0.0f, unityAngle, 0.0f);

   
    }

}
