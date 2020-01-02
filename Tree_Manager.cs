using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tree_Manager : MonoBehaviour
{
    AVL tree = new AVL();
    public List<int> intNums = new List<int>();
    public bool Auto_Balance = true;
    public GameObject PhyNode_Pref;
    public GameObject PhyEdge_Pref;

    public InputField inputfield;
    public Toggle toggle;
    void Awake()
    {
        Init();
    }
    private void Init()
    {
        tree.autobalance = Auto_Balance;
        tree.fatherPhyNodes = gameObject;
        tree.PhyNode_Pref = PhyNode_Pref;
        tree.PhyEdge_Pref = PhyEdge_Pref;
        toggle.isOn = Auto_Balance;
        foreach (int n in intNums)
        {
            tree.Add(n);
        }
        tree.DisplayTree();
        tree.OrganizeNodesTrigger();
        Invoke("AfastarTriggerLocal", 0.1f);
    }
    void ClearTree()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        foreach(Transform t in GameObject.Find("Edge_Case").transform)
        {
            Destroy(t.gameObject);
        }
        foreach(Transform t in GameObject.Find("TextMeshs_Case").transform)
        {
            Destroy(t.gameObject);
        }
    }
    public void Insert()
    {
        if(inputfield.text== null) { return; }
        ClearTree();
        intNums.Add(int.Parse(inputfield.text));
        Invoke("Init", 0.1f);
    }
    public void Remove()
    {
        if (inputfield.text == null) { return; }
        intNums.Remove(int.Parse(inputfield.text));
        tree.Delete(int.Parse(inputfield.text));
        ClearTree();
        Invoke("Init", 0.1f);
    }
    public void Find()
    {
        if (inputfield.text == null) { return; }
        tree.Find(int.Parse(inputfield.text));
    }
    public void Balance()
    {
        foreach(int num in intNums)
        {
            tree.Delete(num);
        }
        ClearTree();
        Auto_Balance = true;
        Invoke("Init", 0.1f);
    }
    void AfastarTriggerLocal()
    {
        tree.AfasterTrigger();
    }
    public void AutBal_Toggle()
    {
        Auto_Balance = toggle.isOn;
        tree.autobalance = Auto_Balance;
    }

}
