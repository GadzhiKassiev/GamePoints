using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperList : MonoBehaviour, IPaperList
{

    //public GameObject point;
    //[SerializeField]
    //private Material[] materials;

    //public Material Material { get; set; }

    private GameObject[,] paperArray;

    private void Awake()
    {
        paperArray = new GameObject[22, 21];
    }

    public int Count { get; set; }

    public GameObject this[int index1, int index2]
    {
        get
        {
            return paperArray[index1, index2];
        }

        set
        {       
            paperArray[index1, index2] = value;
            Count++;
        }
    }

    public void ChangeCapturePointColor(Material mat, List<Point> capturePoint)
    {
        foreach (Point p in capturePoint)    
            if (paperArray[p.x, p.y] != null)
                paperArray[p.x, p.y].GetComponent<SpriteRenderer>().material = mat;
    }

    //public void ChangeMaterial()
    //{
    //    if (materials[0] == Material)
    //        Material = materials[1];
    //    else
    //        Material = materials[0];
    //}
}
