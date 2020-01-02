using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysNode_Ref : MonoBehaviour
{
    public AVL.Node Node;

    public void ColorNode()
    {
        GetComponent<MeshRenderer>().material.color =
            Color.red;
        Invoke("UncolorNode",2f);
    }
    void UncolorNode()
    {
        GetComponent<MeshRenderer>().material.color =
            Color.white;
    }
}
