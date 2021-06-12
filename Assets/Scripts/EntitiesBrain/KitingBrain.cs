using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitingBrain : BaseBrain
{
    public GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == controlller.tagTarget)
        {
            isPatrolling = false;
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == controlller.tagTarget)
        {
            isPatrolling = true;
            target = null;
        }
    }

    void Update()
    {
        CheckPatrolling();
        CheckKiting();
    }

    private void CheckKiting()
    {
        if (!isPatrolling)
        {
            var dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist < controlller.Config.MinRange)
            {
                controlller.Direction = -transform.right.x;
            }
            else if (dist > controlller.Config.MinRange)
            {
                controlller.Direction = transform.right.x;
            }
            else
            {
                controlller.IsInAction = true;
            }
        }      
    }
}
