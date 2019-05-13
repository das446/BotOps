using System;
using UnityEngine;

public class BoxDropoff : MonoBehaviour, IWorkerCanMoveTo {
    public Transform transform => base.transform;
    [SerializeField] ConveyerBelt conveyerBelt;
    public static event Action<BoxDropoff> OnClick;

    private void OnMouseDown() {
        if (!conveyerBelt.Full()) {
            OnClick(this);
        }
    }

    public void OnWorkerReach(Worker w) {
        if (w.HasItem()) {
            IPickupable i = w.DropItem();
            conveyerBelt.AddToQueu(i);
            i.transform.position = transform.position;

        }
    }
}