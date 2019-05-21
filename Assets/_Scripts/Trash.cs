using System;
using UnityEngine;

public class Trash : MonoBehaviour, IWorkerCanMoveTo {

    public static event Action<Trash> OnClick;

    public void OnWorkerReach(Worker w) {
        if (w.HasItem()) {
            Destroy(w.DropItem().transform.gameObject);
            Audio.PlaySound("Trash");
        }
    }

    private void OnMouseDown() {
        OnClick(this);
    }
}