using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour, IAction
{
    public void UseAction(BaseController playerController, Transform player, float duration, float range)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.right, range, playerController.targetableLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag =="Capturable")
            {
                BaseController targetController = hit.collider.gameObject.GetComponent<BaseController>();
                playerController.DisablEntityPhysic();
                playerController.DisableEntityVisual();
                hit.collider.tag = "EnCapture";
                playerController.StartEntityChange(targetController, duration);
            }
        }
    }



}
