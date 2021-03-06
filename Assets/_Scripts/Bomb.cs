using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IPickupable {
    public bool IsNumber => false;
    public static event Action<IPickupable> Leave;

    public static event Action<IPickupable> OnClick;
    public void Enter(ConveyerBelt c) {
        throw new System.NotImplementedException();
    }

    void Start(){
        NumberGoal.BombExplodes+=CheckExplode;
        Player.Lose+=OnLose;
    }

    private void OnLose()
    {
        NumberGoal.BombExplodes-=CheckExplode;
        Player.Lose-=OnLose;
    }

    public void Exit(ConveyerBelt c) {
        throw new System.NotImplementedException();
    }

    private void OnMouseDown() {
        OnClick(this);
    }

    public void OnWorkerReach(Worker w) {

        bool success = w.PickupItem(this);
        if (success) {
            Leave(this);
        }

    }

    public void ReachGoal(NumberGoal g) {
        g.Recieve(this);
        Destroy(gameObject);
    }

    void CheckExplode(Bomb b){
        Debug.Log("Check Explode");
        if(b==this){
            Audio.PlaySound("Explosion");
            Destroy(gameObject);
        }
    }

    public void GetPickedUp(Worker w) {
        
    }
}