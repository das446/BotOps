using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberBox : MonoBehaviour, IWorkerCanMoveTo, IPickupable {

    [SerializeField] int _number;

    public static event Action<IPickupable> Leave;
    public static event Action<IPickupable> OnDiscard;
    public static event Action<IPickupable> OnClick;

    public static List<IPickupable> pool = new List<IPickupable>();

    [SerializeField] TMPro.TMP_Text text;

    public void OnWorkerReach(Worker w) {
        bool success = w.PickupItem(this);
        if (success) {
            Leave(this);
        }

    }
    private void OnMouseDown() {
        OnClick(this);
    }

    public int number => _number;
    public void SetNumber(int x) {
        _number = x;
        if (_number <= 0) {
            _Discard();
        }
        text.text = x + "";
    }

    public void Discard() {
        _Discard();
        
    }

    private void _Discard() {
        IPickupable pickupable = this;
        OnDiscard(pickupable);
        Destroy(gameObject);
    }

    public static IPickupable MakeRandom() {
        throw new NotImplementedException();
    }

    private void SmokeEffect() {
        throw new NotImplementedException();
    }

    public void Enter(ConveyerBelt c) {
        throw new NotImplementedException();
    }

    public void Exit(ConveyerBelt c) {
        throw new NotImplementedException();
    }

    public new Transform transform => base.transform;

    public bool IsNumber => true;

    public void Modify(NumberBox box, Worker w) {
        Worker.Op op = w.op;
        int x = _number;
        if (op == Worker.Op.ADD) {
            x = box._number + x;
        } else if (op == Worker.Op.SUB) {
            x = x - box._number ;
        } else {
            x = box._number * x;
        }

        SetNumber(x);

    }

    public void ReachGoal(NumberGoal g) {
        g.Recieve(this);
        _Discard();
    }

    public void GetPickedUp(Worker w) {
        throw new NotImplementedException();
    }
}