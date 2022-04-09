using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHandler : MonoBehaviour
{
    public Node currentNode;
    CarController carController;
    Vector3 direction;
    Vector2 translatedDirection;

    RaycastHit frontHit, frontRightHit, frontLeftHit;
    int layer;

    void Awake()
    {
        carController = GetComponent<CarController>();
        translatedDirection = new Vector2();
        layer = LayerMask.GetMask("Car");
        frontHit = new RaycastHit();
        frontLeftHit = new RaycastHit();
        frontRightHit = new RaycastHit();
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

        if(Physics.Raycast(transform.position + transform.up, transform.forward, out frontHit, 3f, layer))
        {
            Debug.Log("Hit car in front " + frontHit.transform.gameObject.name);
            translatedDirection.y = -1 / frontHit.distance;
        }


        if(Physics.Raycast(transform.position + transform.up, transform.forward + transform.right, out frontRightHit, 3f, layer))
        {
            Debug.Log("Hit car right " + frontRightHit.transform.gameObject.name);

            translatedDirection.x = -1 / frontRightHit.distance;
        }

        if (Physics.Raycast(transform.position + transform.up, transform.forward - transform.right, out frontLeftHit, 3f, layer))
        {
            Debug.Log("Hit car in left " + frontLeftHit.transform.gameObject.name);

            translatedDirection.x = 1 / frontLeftHit.distance;
        }

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

        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawRay(transform.position, transform.forward - transform.right);
        Gizmos.DrawRay(transform.position, transform.forward + transform.right);

    }
}
