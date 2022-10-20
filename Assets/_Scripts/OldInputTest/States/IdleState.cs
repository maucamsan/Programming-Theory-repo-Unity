using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    IDLE,
    MOVING,
    SHOOTING
}
public class IdleState : StateMachineBehaviour
{
    public const string transitionParameter = "State";
    JoystickV2 joystickV2;

    // GameObject enemy;
    private void Awake()
    {
        joystickV2 = JoystickV2.joystickV2Instance;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("In idle state");
        if (JoystickV2.joystickV2Instance.LastDirection != Vector3.zero)
        {
            animator.SetInteger(transitionParameter, (int)Transition.MOVING);

        }
        // else if (enemy != null)
        // {
        //     animator.SetInteger(transitionParameter, (int)Transition.SHOOTING);
        // }
    }
}
