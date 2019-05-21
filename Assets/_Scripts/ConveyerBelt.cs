using System;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour {
    List<IPickupable> items = new List<IPickupable>();
    [SerializeField] float speed;
    List<IPickupable> queue = new List<IPickupable>();
    [SerializeField] Transform start;
    [SerializeField] NumberGoal goal;
    [SerializeField] float respawnRate;
    [SerializeField] float respawnTimer;

    [SerializeField] NumberBox boxPrefab;
    [SerializeField] Bomb bombPrefab;
    [SerializeField] GameObject rightSprite, leftSprite;

    [Range(0, 1f)]
    [SerializeField] float bombChance;

    void Start() {
        NumberBox.Leave += CheckRemoveItem;
        Bomb.Leave += CheckRemoveItem;
        Player.Lose += OnLose;
    }

    private void OnLose() {
        NumberBox.Leave -= CheckRemoveItem;
        Bomb.Leave -= CheckRemoveItem;
        Player.Lose -= OnLose;
    }

    void Update() {
        MoveBoxes();
        MakeItem();
    }

    public void AddToQueu(IPickupable box) {
        queue.Add(box);
    }

    private void MakeItem() {
        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0) {
            respawnTimer = respawnRate;
            IPickupable next;
            if (queue.Count == 0) {
                next = MakeRandomPickupable();
            } else {
                next = queue[0];
                queue.RemoveAt(0);
            }
            next.transform.position = start.position;
            items.Insert(0, next);
        }
    }

    private IPickupable MakeRandomPickupable() {
        if (UnityEngine.Random.Range(0, 1f) < bombChance) {
            Bomb bomb = Instantiate(bombPrefab, start.position, Quaternion.identity);
            if (bombChance >= 0.1f)
                bombChance -= 0.1f;
            return bomb;
        } else {
            NumberBox box = Instantiate(boxPrefab, start.position, Quaternion.identity);
            box.SetNumber(UnityEngine.Random.Range(1, 10));
            bombChance += 0.05f;
            return box;
        }
    }

    private void MoveBoxes() {

        if (items.Count == 0) { return; }

        for (int i = 0; i < items.Count; i++) {
            if (items[i] == null) {
                items.RemoveAt(i);
                i--;
            } else {
                items[i].transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }

        IPickupable last = items[items.Count - 1];
        if (last.transform.position.x >= goal.transform.position.x) {
            last.ReachGoal(goal);
            items.Remove(last);
        }
    }

    private void ScoreBox(IPickupable last) {

    }

    private void CheckRemoveItem(IPickupable box) {
        if (items.Contains(box)) {
            items.Remove(box);
        }
    }

    public bool Full() {
        return queue.Count > 0;
    }
}