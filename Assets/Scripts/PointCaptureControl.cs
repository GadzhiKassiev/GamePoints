using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCaptureControl {

    static int minX, minY;
    static int maxX, maxY;
    List<Point> outOfCapture = new List<Point>();
    List<Point> inOfCapture = new List<Point>();
    static List<Vector2> capturePointList;

    //public PointCaptureControl (List<Vector2> listVec)
    //{
    //    foreach (Vector2 v in listVec)
    //    {
    //        if (v.x > maxX)
    //            maxX = (int) v.x;
    //        if (v.y > maxY)
    //            maxY = (int) v.y;
    //        if (v.x<minX)
    //            minX = (int) v.x;
    //        if (v.y<minY)
    //            minY = (int) v.y;
    //    }
    //}

    public List<Point> DetectCapturePoints()
    {
        Graph Gr = new Graph(maxX / 10 + 1, maxY / 10 + 1, minX / 10, minY / 10);
        //DepthFirstPath dfs = new DepthFirstPath(StaticGameObject.pointPoligonColliderList, maxX / 10 + 1, maxY / 10 + 1);
        DepthFirstPath dfs = new DepthFirstPath(capturePointList, maxX / 10 + 1, maxY / 10 + 1);
        for (int x = minX / 10; x < maxX/10 + 1; x++)
            for (int y = minY / 10; y < maxY/10 + 1; y++)
            {
                if (!dfs[x, y])
                {
                    dfs.FindAllVertics(Gr, x, y);
                    if (Gr.crossing)
                        outOfCapture.AddRange(dfs.pointList);
                    else
                        inOfCapture.AddRange(dfs.pointList);
                    Gr.crossing = false;
                    dfs.pointList = null;
                }
            }
        return inOfCapture;
    }

    private static void MinMaxPoint(Vector2 v)
    {
        if (v.x > maxX)
            maxX = (int)v.x;
        if (v.y > maxY)
            maxY = (int)v.y;
        if (v.x < minX)
            minX = (int)v.x;
        if (v.y < minY)
            minY = (int)v.y;
    }

    private static void AddingToPointCaptureList(Vector2 v)
    {
        capturePointList.Add(v);
    }

    public static void ScaleDetection(Vector2 v)
    {
        MinMaxPoint(v);
        AddingToPointCaptureList(v);
    }

    private static void MinMaxInitialize()
    {
         minX = int.MaxValue; 
         minY = int.MaxValue;
         maxX = int.MinValue;
         maxY = int.MinValue;
    }

    private static void CapturePointListInitialize()
    {
        capturePointList = new List<Vector2>();
    }

    public static void InitializePointCaptureControl()
    {
        MinMaxInitialize();
        CapturePointListInitialize();
    }
}
