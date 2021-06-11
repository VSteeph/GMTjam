using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : IAction
{
    public void UseAction(BaseController controller, Transform player, float duration, float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, player.forward,out hit, range))
        {

        }
    }
}
