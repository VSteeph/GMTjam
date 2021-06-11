using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void UseAction(BaseController controller, Transform player,float duration, float range);
}
