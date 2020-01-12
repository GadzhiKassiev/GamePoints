using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstPath {

    private bool[,] marked;
    public List<Point> pointList;

    public bool this[int x,int y]
    {
        get { return marked[x, y]; }
    }    

    public DepthFirstPath(List<Vector2> listV, int maxX, int maxY)
    {
        marked = new bool[maxX, maxY];
        foreach (Vector2 v in listV)
            marked[(int)v.x / 10, (int)v.y / 10] = true;
    }

    public void FindAllVertics(Graph G, int w, int h)
    {
        pointList = new List<Point>();
        dfs(G, w, h);
    }

    private void dfs(Graph G, int w, int h)
    {
        marked[w, h] = true;
        pointList.Add(new Point { x = w, y = h });
        foreach (Point p in G.adj(w,h))
        {
            if (!marked[p.x,p.y])
            {
                dfs(G, p.x, p.y);
            }
        }
    }
}
