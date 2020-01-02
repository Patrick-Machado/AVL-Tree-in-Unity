using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVL : MonoBehaviour
{
    public bool autobalance = true;
    public GameObject PhyNode_Pref;
    public GameObject fatherPhyNodes;
    public GameObject PhyEdge_Pref;
    public class Node
    {
        public int data;
        public Node left;
        public Node right;
        public GameObject go;
        public Node(int data)
        {
            this.data = data;
        }
    }
    Node root;
    public AVL()
    {
    }
    public void Add(int data)
    {
        Node newItem = new Node(data);
        if (root == null)
        {
            root = newItem;
        }
        else
        {
            root = RecursiveInsert(root, newItem);
        }
    }
    private Node RecursiveInsert(Node current, Node n)
    {
        if (current == null)
        {
            current = n;
            return current;
        }
        else if (n.data < current.data)
        {
            current.left = RecursiveInsert(current.left, n);
            if(autobalance) current = balance_tree(current);
        }
        else if (n.data > current.data)
        {
            current.right = RecursiveInsert(current.right, n);
            if(autobalance) current = balance_tree(current);
        }
        return current;
    }
    private Node balance_tree(Node current)
    {
        int b_factor = balance_factor(current);
        if (b_factor > 1)
        {
            if (balance_factor(current.left) > 0)
            {
                current = RotateLL(current);
            }
            else
            {
                current = RotateLR(current);
            }
        }
        else if (b_factor < -1)
        {
            if (balance_factor(current.right) > 0)
            {
                current = RotateRL(current);
            }
            else
            {
                current = RotateRR(current);
            }
        }
        return current;
    }
    public void Delete(int target)
    {//and here
        root = Delete(root, target);
    }
    private Node Delete(Node current, int target)
    {
        Node parent;
        if (current == null)
        { return null; }
        else
        {
            //left subtree
            if (target < current.data)
            {
                current.left = Delete(current.left, target);
                if (balance_factor(current) == -2)//here
                {
                    if (balance_factor(current.right) <= 0)
                    {
                        current = RotateRR(current);
                    }
                    else
                    {
                        current = RotateRL(current);
                    }
                }
            }
            //right subtree
            else if (target > current.data)
            {
                current.right = Delete(current.right, target);
                if (balance_factor(current) == 2)
                {
                    if (balance_factor(current.left) >= 0)
                    {
                        current = RotateLL(current);
                    }
                    else
                    {
                        current = RotateLR(current);
                    }
                }
            }
            //if target is found
            else
            {
                if (current.right != null)
                {
                    //delete its inorder successor
                    parent = current.right;
                    while (parent.left != null)
                    {
                        parent = parent.left;
                    }
                    current.data = parent.data;
                    current.right = Delete(current.right, parent.data);
                    if (balance_factor(current) == 2)//rebalancing
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else { current = RotateLR(current); }
                    }
                }
                else
                {   //if current.left != null
                    return current.left;
                }
            }
        }
        return current;
    }
    public void Find(int key)
    {
        if (Find(key, root).data == key)
        {
            Debug.Log(key+" was found!");
            findingnode.go.GetComponent<PhysNode_Ref>().ColorNode();
        }
        else
        {
            Debug.Log("Nothing found!");
        }
    }

    Node findingnode;
    private Node Find(int target, Node current)
    {

        if (target < current.data)
        {
            if (target == current.data)
            {
                findingnode = current;
                return current;
            }
            else
                return Find(target, current.left);
        }
        else
        {
            if (target == current.data)
            {
                findingnode = current;
                return current;
            }
            else
                return Find(target, current.right);
        }

    }
    public void DisplayTree()
    {
        if (root == null)
        {
            Debug.Log("Tree is empty");
            return;
        }
        InOrderDisplayTree(root);
        //Debug.Log("-------------------------------------------------------------");
        
    }
    private void InOrderDisplayTree(Node current)
    {
        if (current != null)
        {
            InOrderDisplayTree(current.left);
            //Debug.Log("("+ current.data + ") ");
            DrawNode(current);
            InOrderDisplayTree(current.right);
        }
    }
    private int max(int l, int r)
    {
        return l > r ? l : r;
    }
    private int getHeight(Node current)
    {
        int height = 0;
        if (current != null)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int m = max(l, r);
            height = m + 1;
        }
        return height;
    }
    private int balance_factor(Node current)
    {
        int l = getHeight(current.left);
        int r = getHeight(current.right);
        int b_factor = l - r;
        return b_factor;
    }
    private Node RotateRR(Node parent)
    {
        Node pivot = parent.right;
        parent.right = pivot.left;
        pivot.left = parent;
        return pivot;
    }
    private Node RotateLL(Node parent)
    {
        Node pivot = parent.left;
        parent.left = pivot.right;
        pivot.right = parent;
        return pivot;
    }
    private Node RotateLR(Node parent)
    {
        Node pivot = parent.left;
        parent.left = RotateRR(pivot);
        return RotateLL(parent);
    }
    private Node RotateRL(Node parent)
    {
        Node pivot = parent.right;
        parent.right = RotateLL(pivot);
        return RotateRR(parent);
    }
    ///////////////////////////////////////////////////////////////////////////////
    private void DrawNode(AVL.Node node)
    {
        GameObject go =Instantiate(PhyNode_Pref, fatherPhyNodes.transform) as GameObject;
        AVL.Node noderef = go.GetComponent<PhysNode_Ref>().Node = node;
        noderef.go = go;
        string str = (node.data >= 0 && node.data < 10)? (" " + (node.data).ToString()) : (node.data).ToString();
        go.GetComponent<TextFixer>().t.text = str;
        go.name = (node.data).ToString(); 
    }
    public void OrganizeNodesTrigger()
    {
        RecursiveOrganizer(root);
    }
    private void RecursiveOrganizer(Node current)
    {
        if (current != null)
        {
            RecursiveOrganizer(current.left);
            //Debug.Log("("+ current.data + ") ");
            OrganizeNodeVisualy(current);
            RecursiveOrganizer(current.right);
        }
    }
    void OrganizeNodeVisualy(Node node)
    {
        //Debug.Log(node.data + " " + node.left.data + " " + node.right.data);
        if (node.left != null)
        {
            node.left.go.transform.parent = node.go.transform;
            node.left.go.transform.position = node.go.transform.position;
            node.left.go.transform.position += new Vector3(-0.575f, -1.075f, 0f)*2f;
            //
        }
        if (node.right != null)
        {
            node.right.go.transform.parent = node.go.transform;
            node.right.go.transform.position = node.go.transform.position;
            node.right.go.transform.position += new Vector3(0.575f, -1.075f, 0f)*2f;
            //
        }
    }
    public void AfasterTrigger()
    {
        RecursiveAfaster(root);
    }
    private void RecursiveAfaster(Node current)
    {
        if (current != null)
        {
            RecursiveAfaster(current.left);
            //Debug.Log("("+ current.data + ") ");
            AfasterVisually(current);
            RecursiveAfaster(current.right);
        }
    }
    private void AfasterVisually(Node node)
    {
        if (node.left != null)
        {
            Afaster A = node.left.go.GetComponent<Afaster>();
            A.Activate();
            ConectEdge(node.go, node.left.go);
        }
        if (node.right != null)
        {
            Afaster A = node.right.go.GetComponent<Afaster>();
            A.Activate();
            ConectEdge(node.go, node.right.go);
        }
    }
    void ConectEdge(GameObject A, GameObject B)
    {
        GameObject edge = Instantiate(PhyEdge_Pref, GameObject.Find("Edge_Case").transform);
        edge.GetComponent<Edge>().Init(A, B);
    }
    
}