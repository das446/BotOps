using UnityEngine;

public interface IWorkerCanMoveTo {
    void OnWorkerReach(Worker w);
    Transform transform { get; }
}