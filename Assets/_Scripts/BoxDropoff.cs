using System;
using UnityEngine;

public class BoxDropoff : MonoBehaviour, IWorkerCanMoveTo {
    public Transform transform => base.transform;
    [SerializeField] ConveyerBelt conveyerBelt;
    public static event Action<BoxDropoff> onClick;

    private void OnMouseDown() {
        OnClick();
    }

    public void OnClick() {
        onClick(this);
    }

    public void OnWorkerReach(Worker w) {
        if (w.HasItem()) {
            conveyerBelt.AddToQueu(w.DropItem());
        }
    }
}