using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHandler : MonoBehaviour
{
    public Node currentNode;
    CarController carController;
    Vector3 direction;
    Vector2 translatedDirection;

    void Awake()
    {
        carController = GetComponent<CarController>();
        translatedDirection = new Vector2();
    }

    void Start()
    {
        currentNode = AiNodes.instance.GetFirst();
    }

    void Update()
    {
        translatedDirection.x = Vector3.Dot(transform.right, currentNode.Position() - transform.position);
        translatedDirection.y = Vector3.Dot(transform.forward, currentNode.Position() - transform.position) / Vector3.Distance(transform.position,currentNode.Position());

        carController.playerInput = translatedDirection.normalized;

        if (Vector3.Distance(transform.position, currentNode.Position()) < 2)
        {
            currentNode = currentNode.nextNode;
        }
    }

    void OnDrawGizmos()
    {
        if (!currentNode) { return; }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector3(translatedDirection.x,0,translatedDirection.y));
    }
}
