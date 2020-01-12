using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite {

    private GameObject gameGameObject;

    [SetUp]
    public void Setup()
    {
         gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Paper"));
    }

    [UnityTest]
    public IEnumerator CheckCountIfAddOnePoint()
    {
        gameGameObject.GetComponent<Player>().CreatePoint(new Vector3 { x = 20, y = 30, z = 1 });
        yield return null;
        Assert.AreEqual(1, gameGameObject.GetComponent<PaperList>().Count);
    }
}
