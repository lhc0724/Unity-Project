using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public Color _color = Color.red;
    public float _radius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.color = _color;   //gizmo color setup
        Gizmos.DrawSphere(transform.position, _radius); //view on circle gizmo
    }

}
