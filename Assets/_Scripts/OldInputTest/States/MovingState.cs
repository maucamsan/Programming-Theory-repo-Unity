using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : StateMachineBehaviour
{
   JoystickV2 joystickV2;

    public float playerSpeed = 5;
    float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [SerializeField] private CharacterController characterController;

    //CharacterController characterController;
   private void Awake()
    {
      
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (JoystickV2.joystickV2Instance.LastDirection == Vector3.zero)
        {
            animator.SetInteger(IdleState.transitionParameter, (int)Transition.IDLE);
        }
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = JoystickV2.joystickV2Instance.LastDirection;

        if (JoystickV2.joystickV2Instance.TouchStart)
        {   
           // moveCharacter(JoystickV2.joystickV2Instance.Direction, animator);
            
            // animator.gameObject.GetComponent<CharacterController>().Move(  (JoystickV2.joystickV2Instance.Direction.normalized * 
            //    Time.deltaTime * playerSpeed));

           

            //animator.transform.position = animator.transform.position + (JoystickV2.joystickV2Instance.Direction.normalized * Time.deltaTime * playerSpeed);
            MoveCharacter(direction, animator);
            
            if (JoystickV2.joystickV2Instance.LastDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(JoystickV2.joystickV2Instance.LastDirection.x, JoystickV2.joystickV2Instance.LastDirection.z) 
                                    * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(animator.gameObject.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                animator.gameObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Debug.Log("moving");
            }
        }
    }

    void MoveCharacter(Vector3 direction, Animator animator)
    {
        animator.GetComponent<CharacterController>().Move(direction.normalized * Time.deltaTime * playerSpeed);
        // animator.transform.position = animator.transform.position + (direction.normalized * Time.deltaTime * playerSpeed);
    }
}
