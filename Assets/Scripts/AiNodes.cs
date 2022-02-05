using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AiNodes : MonoBehaviour
{
    public List<Transform> nodes;

    void Awake()
    {
        nodes = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            nodes.Add(transform.GetChild(i));
        }
    }
}
