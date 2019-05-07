using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public Worker currentWorker;
    int points;
    int lives;

    void Start() {
        NumberBox.OnClick += MoveWorkerToBox;
        TempSpot.OnClick += MoveWorker;
        NumberGoal.BombExplodes += LoseLife;
        NumberGoal.ScorePoints += GetPoints;
        Bomb.OnClick += MoveWorker;
    }

    private void GetPoints(int x) {
        throw new NotImplementedException();
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