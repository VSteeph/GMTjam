using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public BaseConfig config;
    protected float direction;

    public float Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    public bool IsInAction { get; set; }
}
