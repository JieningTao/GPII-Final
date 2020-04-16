using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float ReadMovement()
    {
        if (Input.GetKey(KeyCode.A))
            return -1;
        else if (Input.GetKey(KeyCode.D))
            return 1;
        return 0;
    }

    public bool ReadJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return true;
        return false;
    }

    public bool ReadAttack()
    {
        if (Input.GetKey(KeyCode.K))
            return true;
        return false;
    }

    public bool ReadSecondary()
    {
        if (Input.GetKey(KeyCode.L))
            return true;
        return false;
    }

}
