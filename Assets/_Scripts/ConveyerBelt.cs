using System;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour {
    List<IPickupable> boxes;
    [SerializeField] float speed;
    IPickupable queue;
    [SerializeField] Transform start;
    [SerializeField] NumberGoal goal;
    [SerializeField] float respawnRate;
    [SerializeField] float respawnTimer;

    void Start() {

    }

    void Update() {
        MoveBoxes();
        MakeBox();
    }

    public void AddToQueu(IPickupable box) {
        queue = box;
    }

    private void MakeBox() {
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0) {
            respawnTimer = respawnRate;
            IPickupable next = queue;
            if (next == null) {
                next = MakeRandomPickupable();
            }
            queue = null;
            next.transform.position = start.position;
            boxes.Insert(0, next);
        }
    }

    private IPickupable MakeRandomPickupable()
    {
        throw new NotImplementedException();
    }

    private void MoveBoxes() {
        for (int i = 0; i < boxes.Count; i++) {
            boxes[i].transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        IPickupable last = boxes[boxes.Count - 1];
        if (last.transform.position.x >= goal.transform.position.x) {
            last.ReachGoal(goal);
            boxes.Remove(last);
        }
    }

    private void ScoreBox(IPickupable last) {

    }

    private void CheckRemoveBox(IPickupable box) {
        if (boxes.Contains(box)) {
            boxes.Remove(box);
        }
    }
}