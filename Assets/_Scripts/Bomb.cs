using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IPickupable {

    public static event Action<IPickupable> OnClick;
    public void Enter(ConveyerBelt c) {
        throw new System.NotImplementedException();
    }

    public void Exit(ConveyerBelt c) {
        throw new System.NotImplementedException();
    }

    private void OnMouseDown() {
        OnClick(this);
    }

    public void OnWorkerReach(Worker w) {
        throw new System.NotImplementedException();
    }

    public void ReachGoal(NumberGoal g) {
        g.Recieve(this);
        Destroy(gameObject);
    }
}