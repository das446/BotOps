using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Worker currentWorker;
    [SerializeField] int points;
    [SerializeField] int lives;
    [SerializeField] Text text;
    [SerializeField] Image timeBar;
    private float currTime;
    public float maxShiftTime;

    void Start() {
        NumberBox.OnClick += MoveWorkerToBox;
        Garbage.OnClick += MoveWorker;
        TempSpot.OnClick += MoveWorker;
        BoxDropoff.OnClick += MoveWorker;
        NumberGoal.BombExplodes += LoseLife;
        NumberGoal.ScorePoints += GetPoints;
        Bomb.OnClick += MoveWorkerToBox;
        currTime = maxShiftTime;
    }

    void Update() {
        if (currTime > 0f)
        {
            currTime -= Time.deltaTime;
        }
        else
        {
            currTime = 0f;
        }
        float scale = currTime / maxShiftTime;
        scale = scale > 1 ? 1 : scale;
        timeBar.transform.localScale = new Vector3(currTime / maxShiftTime, 1f, 1f);

        if (Input.GetKey("escape"))
            Application.Quit(); 
    }

    private void GetPoints(int x) {
        Debug.Log("Get " + x + " points");
        points += x;
        text.text = "Score: " + points;
    }

    private void MoveWorker(IWorkerCanMoveTo target) {

        if (currentWorker != null) {
            Debug.Log("MoveWorker");
            currentWorker.MoveToTarget(target);
        }
    }

    private void MoveWorkerToBox(IWorkerCanMoveTo box) {

        if (currentWorker != null) {
            Debug.Log("MoveWorker");
            int o = 5;
            Vector2 offset = new Vector2(0, 2);
            if (transform.position.y < box.transform.position.y) {
                offset.y = -o;
            }
            currentWorker.MoveToTarget(box, offset);
        }
    }

    private void LoseLife() {
        lives--;
        if (lives <= 0) {
            LoseGame();
        }
    }

    private void LoseGame() {
        throw new NotImplementedException();
    }

    public void SetCurrentWorker(Worker worker) {
        currentWorker = worker;
    }
}