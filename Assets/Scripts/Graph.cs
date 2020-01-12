using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Graph  {

    private readonly int W , H, WMin, HMin;
    public bool crossing = false;
    List<Point>[,] adjArr;
    public Graph(int W, int H, int WMin, int HMin)
    {
        this.W = W;
        this.H = H;
        this.WMin = WMin;
        this.HMin = HMin;
        adjArr = new List<Point>[W, H];
        for (int w = WMin; w < W; w++)
        {
            for(int h = HMin; h < H; h++)
            {
                adjArr[w, h] = new List<Point>();
            }
        }      
    }

    public IEnumerable<Point> adj(int w, int h)
    {
        if (CrossScale(w + 1, h))
            crossing = true;
        else
            adjArr[w, h].Add(new Point { x = w + 1, y = h });
        if (CrossScale(w - 1, h))
            crossing = true;
        else
            adjArr[w, h].Add(new Point { x = w - 1, y = h });
        if (CrossScale(w, h + 1))
            crossing = true;
        else
            adjArr[w, h].Add(new Point { x = w, y = h + 1});
        if (CrossScale(w, h - 1))
            crossing = true;
        else
            adjArr[w, h].Add(new Point { x = w, y = h - 1});
        return adjArr[w, h];
    }

    private bool CrossScale(int w, int h)
    {
        if (w >= W || w < WMin || h >= H || h < HMin)
            return true;
        return false;
    }
}
