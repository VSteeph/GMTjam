using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour, IAction
{
    public void UseAction(BaseController playerController, Transform player, float duration, float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, player.right, out hit, range))
        {
            if (hit.collider.tag =="Capturable")
            {
                BaseController targetController = hit.collider.gameObject.GetComponent<BaseController>();
                playerController.DisablEntityPhysic();
                playerController.DisableEntityVisual();
                playerController.StartEntityChange(targetController, duration);
            }
        }
    }



}
