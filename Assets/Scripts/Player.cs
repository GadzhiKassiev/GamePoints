using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public  GameObject point;
    [SerializeField]
    private Material[] materials;
    public bool isFirstPlayer;

    public Material Material { get; set; }
    public Vector3 LastPointPos { get; set; }
    public Vector3 LastCapturePointPos { get; set; }

    private void Awake()
    {
        isFirstPlayer = true;
    }

    private void ChangeColor()
    {
        if (isFirstPlayer)
            Material = materials[0];
        else
            Material = materials[1];
    }

    public void ChangePlayer()
    {
        isFirstPlayer = !isFirstPlayer;
        ChangeColor();
    }

    public  void CreatePoint(Vector3 pointPos)
    {
        GameObject temp = Instantiate(point, pointPos, gameObject.transform.rotation);
        temp.GetComponent<SpriteRenderer>().material = Material;
        gameObject.GetComponent<PaperList>()[(int)(pointPos.x / 10), (int)(pointPos.y / 10)] = temp;
    }

    public bool MaterialCompare(Material mat)
    {
        if (mat.name.Contains(Material.name))
            return true;
        return false;
    }

    public void CreatePlayerObjectsByBeginCapture(GameObject gameObj)
    {
        StaticGameObject.endPoint = gameObj;
        StaticGameObject.newLineGem = new GameObject("MyGameObject");
        StaticGameObject.LRend = StaticGameObject.newLineGem.AddComponent<LineRenderer>();
        //StaticGameObject.pCol = StaticGameObject.newLineGem.AddComponent<PolygonCollider2D>();
        //StaticGameObject.pointPoligonColliderList = new List<Vector2>();
        StaticGameObject.LRend.material = Material;
        StaticGameObject.LRend.startWidth = 3;
        StaticGameObject.LRend.positionCount = 2;
        StaticGameObject.LRend.SetPosition(0, gameObj.transform.position);
        LastPointPos = gameObj.transform.position;
    }

    public bool NeighborhoodPoint(Vector3 vector)
    {
        Vector3 temp = vector - LastPointPos;
        if (Mathf.Abs(temp.x) > 10 || Mathf.Abs(temp.y) > 10)
            return false;
        return true;
    }
}
