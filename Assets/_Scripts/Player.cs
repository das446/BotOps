using System;
using UnityEngine;

public class Player : MonoBehaviour {
    Worker currentWorker;
    int points;
    int lives;

    void Start() {
        NumberBox.OnClick += MoveWorker;
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
            currentWorker.MoveToTarget(target);
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
}