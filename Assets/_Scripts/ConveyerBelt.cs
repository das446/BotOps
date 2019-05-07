using System;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour {
    List<IPickupable> items = new List<IPickupable>();
    [SerializeField] float speed;
    IPickupable queue;
    [SerializeField] Transform start;
    [SerializeField] NumberGoal goal;
    [SerializeField] float respawnRate;
    [SerializeField] float respawnTimer;

    [SerializeField] NumberBox boxPrefab;
    [SerializeField] Bomb bombPrefab;

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
            items.Insert(0, next);
        }
    }

    private IPickupable MakeRandomPickupable()
    {
        NumberBox box = Instantiate(boxPrefab,start.position,Quaternion.identity);
        box.SetNumber(UnityEngine.Random.Range(1,10));
        return box;
    }

    private void MoveBoxes() {

        if(items.Count==0){return;}

        for (int i = 0; i < items.Count; i++) {
            items[i].transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        
        IPickupable last = items[items.Count - 1];
        if (last.transform.position.x >= goal.transform.position.x) {
            last.ReachGoal(goal);
            items.Remove(last);
        }
    }

    private void ScoreBox(IPickupable last) {

    }

    private void CheckRemoveBox(IPickupable box) {
        if (items.Contains(box)) {
            items.Remove(box);
        }
    }
}