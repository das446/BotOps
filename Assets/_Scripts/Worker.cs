﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    IPickupable held;
    [SerializeField] float speed;
    [SerializeField] bool moving;
    public enum Op {
        ADD,
        SUB,
        MULT,
    }
    public Op op;
    [SerializeField] Player player;
    [SerializeField] float waitTime;
    Vector2 startPos;

    [SerializeField] Sprite front, back;
    [SerializeField] SpriteRenderer sr;

    void Start() {
        NumberBox.OnDiscard += DropBox;
        startPos = transform.position;
        Player.Lose += OnLose;
    }

    private void OnLose() {
        NumberBox.OnDiscard -= DropBox;
        Player.Lose -= OnLose;
    }

    private void DropBox(IPickupable obj) {
        if (obj == held) {
            DropItem();
        }
    }

    public void MoveToTarget(IWorkerCanMoveTo t) {
        StopAllCoroutines();
        StartCoroutine(GotoTarget(t));
    }

    public void MoveToTarget(IWorkerCanMoveTo t, Vector2 offset) {
        StopAllCoroutines();
        StartCoroutine(GotoTarget(t, offset));
    }

    public bool HasItem() {
        return held != null;
    }

    public IPickupable DropItem() {
        IPickupable temp = held;
        held.transform.parent = null;
        held = null;
        return temp;
    }

    /// <summary>
    /// returns whether the item was picked up
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool PickupItem(IPickupable item) {

        if (held == null) {
            held = item;
            held.transform.position = transform.position + Vector3.up * 8;
            held.transform.parent = transform;
            Audio.PlaySound("Pickup");
            return true;
        } else if (item.IsNumber && held.IsNumber) {
            NumberBox itemBox = (NumberBox) item;
            NumberBox heldBox = (NumberBox) held;
            heldBox.Modify(itemBox, this);
            itemBox.Discard();
            Audio.PlaySound("Pickup");
            return true;
        }
        return false;
    }

    IEnumerator GotoTarget(IWorkerCanMoveTo t, Vector3 offset) {
        Vector2 target = t.transform.position + offset;
        if (target.y > transform.position.y) {
            sr.sprite = back;
        } else {
            sr.sprite = front;
        }
        while (Vector2.Distance(transform.position, t.transform.position + offset) > 5) {
            transform.position = Vector2.MoveTowards(transform.position, t.transform.position + offset, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        t.OnWorkerReach(this);
        if (held == null) {
            yield return new WaitForSeconds(waitTime);
            while (Vector2.Distance(transform.position, startPos) > 0.1f) {
                transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator GotoTarget(IWorkerCanMoveTo t) {
        yield return GotoTarget(t, Vector2.zero);
    }

    private void OnMouseDown() {
        player.SetCurrentWorker(this);
        Debug.Log("click");
    }

}