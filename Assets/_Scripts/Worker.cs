using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    IPickupable held;
    [SerializeField] float speed;
    [SerializeField] bool moving;
    enum Op {
        ADD,
        SUB,
        MULT,
    }
    Op op;
    [SerializeField] Player player;

    public void MoveToTarget(IWorkerCanMoveTo t) {
        StopAllCoroutines();
        StartCoroutine(GotoTarget(t));
    }

    public bool HasItem() {
        return held != null;
    }

    public IPickupable DropItem() {
        IPickupable temp = held;
        held = null;
        return temp;
    }

    public void PickupItem(IPickupable box) {
        held = box;
    }

    IEnumerator GotoTarget(IWorkerCanMoveTo t) {
        WaitForEndOfFrame f = new WaitForEndOfFrame();
        while (Vector2.Distance(transform.position, t.transform.position) < 0.1f) {
            transform.position = Vector2.MoveTowards(transform.position, t.transform.position, speed * Time.deltaTime);
            yield return f;
        }
        t.OnWorkerReach(this);
    }

    private void OnMouseDown() {
        player.SetCurrentWorker(this);
    }

}