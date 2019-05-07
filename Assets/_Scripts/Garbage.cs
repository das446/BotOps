using System;
using UnityEngine;

public class Garbage : MonoBehaviour, IWorkerCanMoveTo {

    public static event Action<Garbage> OnClick;

    public void OnWorkerReach(Worker w) {
        if (w.HasItem()) {
           IPickupable dropped = w.DropItem();
           Destroy(dropped.transform.gameObject);
        }
    }

    public void OnMouseDown()
    {
        OnClick(this);
    }

}