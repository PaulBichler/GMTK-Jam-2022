using System.Collections.Generic;
using UnityEngine;

public interface IPathTraveler
{
    void StartTravel(List<Transform> path);
    void GoToNext(int pathIndex);
}
