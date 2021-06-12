using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBrain : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    [SerializeField]
    protected BaseController controlller;
    protected bool isPatrolling = true;
    protected Vector3 currentDestination;
    protected float dist;
    protected bool sensDirection = true;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        GotoNextPoint();
        controlller.ignoreRotation = true;
    }


    protected void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        currentDestination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

        Vector3 right = controlller.Direction != 0 ? new Vector3(controlller.Direction, 0, 0) : transform.right;
        Vector3 desti = currentDestination - transform.position;
        float sens = Vector3.Dot(right.normalized, desti.normalized);
        if (sens > 0)
        {
            if (controlller.Direction == 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                controlller.Direction = 1;
                sensDirection = true;
            }
        }
        else if (sens < 0)
        {
            if (sensDirection)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
                controlller.Direction = -1;
                sensDirection = false;
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                controlller.Direction = 1;
                sensDirection = true;
            }
        }

    }

    protected void CheckPatrolling()
    {
        if (isPatrolling)
        {
            dist = Mathf.Abs((currentDestination.x - transform.position.x));
            if (dist < 3f)
                GotoNextPoint();
        }
    }

}
