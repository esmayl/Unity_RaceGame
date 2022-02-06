using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AiNodes : MonoBehaviour
{
    public Node nodes;
    public static AiNodes instance;

    void Awake()
    {
        Node tempNode;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            tempNode = transform.GetChild(i).GetComponent<Node>();
            tempNode.previousNode = nodes;

            if (i < transform.childCount - 1)
            {
                tempNode.nextNode = transform.GetChild(i + 1).GetComponent<Node>();
            }
            else
            {
                tempNode.nextNode = transform.GetChild(0).GetComponent<Node>();
            }
            nodes = tempNode;
        }

        instance = this;
    }

    public Node GetFirst()
    {
        Node temp = nodes;

        while(temp.previousNode != null)
        {
            temp = temp.previousNode;
        }

        return temp;
    }
}
