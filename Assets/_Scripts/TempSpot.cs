using System;
using UnityEngine;

public class TempSpot : MonoBehaviour, IWorkerCanMoveTo {

    IPickupable box;

    public new Transform transform => base.transform;
    public static event Action<TempSpot> OnClick;

    private void OnMouseDown() {
        OnClick(this);
    }

    public void OnWorkerReach(Worker w) {
        if (w.HasItem()) {
            box = w.DropItem();
        } else if (box != null) {
            w.PickupItem(box);
            box = null;
        }

        if (box != null) {
            box.transform.position = transform.position;
        }
    }

}