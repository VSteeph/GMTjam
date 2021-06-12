using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IAction
{
    public void UseAction(BaseController controller, Transform player, float duration, float range)
    {
        Debug.Log("Pew");
        var spawnPosition = transform.position + transform.right;
        var bullet = Instantiate(controller.projectileModel, spawnPosition, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(transform.right, controller.Config.bulletVelocity, controller.targetableLayer, controller.tagTarget);

    }
}
