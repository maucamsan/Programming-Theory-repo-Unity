using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    private CharacterController characterController;
    public float playerSpeed;

    float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
	// Update is called once per frame
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        if (JoystickV2.joystickV2Instance.TouchStart)
        {   
            moveCharacter(JoystickV2.joystickV2Instance.Direction);
            //JoystickV2.joystickV2Instance.LastDirectionY = transform.position.y;
            
            if (JoystickV2.joystickV2Instance.LastDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(JoystickV2.joystickV2Instance.LastDirection.x, JoystickV2.joystickV2Instance.LastDirection.z) 
                                    * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    void moveCharacter(Vector3 direction)
    {
        characterController.Move(direction.normalized * Time.deltaTime * playerSpeed);
    }
}
