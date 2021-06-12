using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public Object NastyInspectorAccess;
    public IdentityController identity;
    public IAction action;

    private void Awake()
    {
        action = (IAction)NastyInspectorAccess;
    }
}
