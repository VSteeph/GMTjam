using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour, IAction
{
    public LayerMask possessionLayer;
    public void UseAction(BaseController playerController, Transform player, float duration, float range)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.right, range, possessionLayer);
        if (hit.collider != null)
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
