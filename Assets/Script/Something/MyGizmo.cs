using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color lineColor;
    public List<Transform> Nodes;


    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        Transform[] PathTransform = GetComponentsInChildren<Transform>();
        Nodes = new List<Transform>();
        for(int i=0; i<PathTransform.Length; i++)
        {
            if(PathTransform[i] != transform)
            {
                Nodes.Add(PathTransform[i]);
            }
        }
        for(int i=0; i<Nodes.Count; i++)
        {
            Vector3 CurrentNode = Nodes[i].position;
            Vector3 PrevioutNode = Vector3.zero;
            if(i>0)
            {
                PrevioutNode = Nodes[i - 1].position;
            }
            else if(i==0 && Nodes.Count>1)
            {
                PrevioutNode = Nodes[Nodes.Count - 1].position;
            }
            Gizmos.DrawLine(PrevioutNode, CurrentNode);
            Gizmos.DrawSphere(CurrentNode, 3f);
        }
    }
}
