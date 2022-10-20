using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToLook : MonoBehaviour
{
    void Update()
    {
        transform.position = JoystickV2.joystickV2Instance.Direction;
    }
}
