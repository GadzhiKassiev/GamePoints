using UnityEngine;

public interface IPlayer
{
    Vector3 LastCapturePointPos { get; set; }
    Vector3 LastPointPos { get; set; }
    Material Material { get; set; }

    void ChangeColor();
    void CreatePlayerObjectsByBeginCapture(GameObject gameObj);
    void CreatePoint(Vector3 pointPos);
    bool MaterialCompare(Material mat);
    bool NeighborhoodPoint(Vector3 pos, Vector3 vector);
}