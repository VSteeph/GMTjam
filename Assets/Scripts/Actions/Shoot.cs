using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IAction
{
    public void UseAction(BaseController controller, Transform player, float duration, float range)
    {
        Debug.Log("pew");
    }
}
