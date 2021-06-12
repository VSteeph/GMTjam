using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour, IAction
{
    public void UseAction(BaseController controller, Transform player, float duration, float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, player.right, out hit, range))
        {
            if (hit.collider.tag =="Capturable")
            {
                GameObject target = hit.collider.gameObject;
                Controllable controllable = target.GetComponent<Controllable>();
                player.position = target.transform.position;
                controller.canMove = false;
                StartCoroutine(StartIdentityChange(duration, controller, controllable));
            }
        }
    }

    public IEnumerator StartIdentityChange(float duration, BaseController controller, Controllable controllable)
    {
        yield return new WaitForSeconds(duration);
        controller.ChangeIdentity(controllable);
    }

}
