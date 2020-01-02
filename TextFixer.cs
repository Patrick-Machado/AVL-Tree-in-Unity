using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFixer : MonoBehaviour
{
    GameObject go;
    public TextMesh t;
    private void Awake()
    {

        go = new GameObject();
        t = go.AddComponent<TextMesh>();
        t.color = new Color(0f,0f,0f,255f);
        t.text = "w";
        t.fontSize = 16;
        go.transform.parent = GameObject.Find("TextMeshs_Case").transform;

    }
    private void FixedUpdate()
    {
        t.transform.localEulerAngles = this.gameObject.transform.position - new Vector3(0.80f, 0.80f, 0f);//+= new Vector3(90, 0, 0);
        t.transform.localPosition = this.gameObject.transform.position - new Vector3(0.80f, 0.80f, 0f);
    }
}
