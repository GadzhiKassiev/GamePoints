using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTest{

	[Test]
    public void CheckEmptyPaper()
    {
        var pl = Substitute.For<IPaperList>();
        Assert.AreEqual(0, pl.Count);
    }

    [Test]
    public void CheckNeighborhoodPoint()
    {

        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Paper"));
        gameGameObject.GetComponent<Player>().LastPointPos = new Vector2(30, 30);
        Vector2 vec1 = new Vector2(10, 10);
        Vector2 vec2 = new Vector2(20, 20);
        Vector2 vec3 = new Vector2(30, 20);
        Vector2 vec4 = new Vector2(40, 20);
        Vector2 vec5 = new Vector2(20, 30);
        Vector2 vec6 = new Vector2(10, 30);
        Vector2 vec7 = new Vector2(30, 10);
        Vector2 vec8 = new Vector2(20, 40);
        Vector2 vec9 = new Vector2(30, 40);
        Vector2 vec10 = new Vector2(40, 30);
        Vector2 vec11 = new Vector2(40, 40);


        Assert.IsFalse(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec1));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec2));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec3));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec4));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec5));
        Assert.IsFalse(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec6));
        Assert.IsFalse(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec7));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec8));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec9));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec10));
        Assert.IsTrue(gameGameObject.GetComponent<Player>().NeighborhoodPoint(vec11));

    }
}
