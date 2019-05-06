using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberBox : MonoBehaviour, IWorkerCanMoveTo, IPickupable {

    [SerializeField] int _number;

    public static event Action<IPickupable> Leave;
    public static event Action<IPickupable> OnDiscard;
    public static event Action<IPickupable> OnClick;

    public static List<IPickupable> pool = new List<IPickupable>();

    public void OnWorkerReach(Worker w) {

     }
    private void OnMouseDown()
    {
        OnClick(this);
    }

    public int number => _number;
    public void SetNumber(int x) {
        _number = x;
        SetDisplay(_number);
        if (_number <= 0) {
            Discard();
        }
    }

    public void Discard(){
        SmokeEffect();
        Destroy(gameObject);
    }

    private void _Discard() {
        IPickupable pickupable = this;
        OnDiscard(pickupable);
        Discard();
    }

    public static IPickupable MakeRandom()
    {
        throw new NotImplementedException();
    }

    private void SmokeEffect() {
        throw new NotImplementedException();
    }

    private void SetDisplay(int number) {
        throw new NotImplementedException();
    }

    public void Enter(ConveyerBelt c)
    {
        throw new NotImplementedException();
    }

    public void Exit(ConveyerBelt c)
    {
        throw new NotImplementedException();
    }

    public new Transform transform => base.transform;

    public void Modify(NumberBox box, Worker qw){

    }

    public void ReachGoal(NumberGoal g)
    {
        g.Recieve(this);
    }
}