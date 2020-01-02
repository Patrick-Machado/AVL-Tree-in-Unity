using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afaster : MonoBehaviour
{
    SphereCollider c;
    public bool colliding = false;
    public bool IsOn = false;
    void Awake()
    {
        c = GetComponent<SphereCollider>();
    }
    public void Activate()
    {
        if(colliding == true)
        {
            IsOn = true;
        }
    }
    private void FixedUpdate()
    {
        if (c != null && colliding && IsOn)
        {
            if (this.transform.position.x - transform.parent.transform.position.x < 0)
            {//a esquerda
                transform.parent.position += new Vector3(0.1f, 0f, 0f);
            }
            else
            {
                transform.parent.position -= new Vector3(0.1f, 0f, 0f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        colliding = true;
    }
    private void OnTriggerExit(Collider other)
    {
        colliding = false;
        IsOn = false;
    }

}
