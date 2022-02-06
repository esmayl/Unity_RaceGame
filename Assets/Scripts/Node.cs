using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node previousNode;
    public Node nextNode;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 1);

        if (nextNode)
        {
            Gizmos.DrawLine(Position(), nextNode.Position());
        }

    }

    public Vector3 Position()
    {
        return transform.position;
    }
}
