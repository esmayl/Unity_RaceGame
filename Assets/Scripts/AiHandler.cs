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
        Vector3 localDir = currentNode.Position() - (transform.position + (transform.forward * carController.velocity * 0.1f));

        translatedDirection.x = Utils.CustomNormalize(Vector3.Dot(transform.right, localDir.normalized),-1,1);
        translatedDirection.y = Utils.CustomNormalize(Vector3.Dot(transform.forward, localDir.normalized),-1,1);

        carController.playerInput = translatedDirection;

        if (Vector3.Dot(transform.forward, localDir) < 0)
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
