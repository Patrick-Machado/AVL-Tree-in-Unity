using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private GameObject A, B;
    public void Init(GameObject a, GameObject b)
    {
        A = a;B = b;
        AtualizaPosicaoRotacao();
    }
    public void AtualizaPosicaoRotacao()
    {
        transform.up = (A.gameObject.transform.position - B.gameObject.transform.position).normalized;
        transform.localScale = new Vector3(transform.localScale.x, Vector3.Distance(A.gameObject.transform.position, B.gameObject.transform.position) / 2, transform.localScale.z);
        transform.position = (A.gameObject.transform.position + B.gameObject.transform.position) / 2;
    }
    private void FixedUpdate()
    {
        if(A && B != null)
        {
            AtualizaPosicaoRotacao();
        }
    }
}
