using UnityEngine;

public interface IPaperList
{
    GameObject this[int index1, int index2] { get; set; }

    int Count { get; set; }
}