using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Worker currentWorker;
    [SerializeField] int points;
    [SerializeField] int lives;
    [SerializeField] Text ScoreText;
    [SerializeField] Text GoalText;
    [SerializeField] Image timeBar;
    private float currTime;
    public float maxShiftTime;
    public float levelGoal;

    public static event Action Lose;

    void Start() {
        NumberBox.OnClick += MoveWorkerToBox;
        Garbage.OnClick += MoveWorker;
        TempSpot.OnClick += MoveWorker;
        BoxDropoff.OnClick += MoveWorker;
        NumberGoal.ScorePoints += GetPoints;
        NumberGoal.OnClick += MoveWorker;
        Bomb.OnClick += MoveWorkerToBox;
        currTime = maxShiftTime;
        GoalText.text = "Goal: " + levelGoal;
    }

    void Update() {
        if (currTime > 0f) {
            currTime -= Time.deltaTime;
        } else {
            currTime = 0f;
        }
        float scale = currTime / maxShiftTime;
        scale = scale > 1 ? 1 : scale;
        timeBar.transform.localScale = new Vector3(currTime / maxShiftTime, 1f, 1f);

        if (Input.GetKey("escape"))
            Application.Quit();

        if (points >= levelGoal) {
            IncreaseLevel();
        }
    }

    private void GetPoints(int x) {
        points += x;
        ScoreText.text = "Score: " + points;
    }

    private void MoveWorker(IWorkerCanMoveTo target) {

        if (currentWorker != null) {
            currentWorker.MoveToTarget(target);
        }
    }

    private void MoveWorkerToBox(IWorkerCanMoveTo box) {

        if (currentWorker != null) {
            int o = 5;
            Vector2 offset = new Vector2(0, 2);
            if (transform.position.y < box.transform.position.y) {
                offset.y = -o;
            }
            currentWorker.MoveToTarget(box, offset);
        }
    }

    private void LoseLife(Bomb b) {
        lives--;
        if (lives <= 0) {
            LoseGame();
        }
    }

    private void IncreaseLevel() {
        points = 0;
        levelGoal += 10;
        currTime = maxShiftTime;

        GoalText.text = "Goal: " + levelGoal;
        ScoreText.text = "Score: " + points;
    }

    private void LoseGame() {
        Lose();
        NumberBox.OnClick -= MoveWorkerToBox;
        Garbage.OnClick -= MoveWorker;
        TempSpot.OnClick -= MoveWorker;
        BoxDropoff.OnClick -= MoveWorker;
        NumberGoal.ScorePoints -= GetPoints;
        NumberGoal.OnClick -= MoveWorker;
        Bomb.OnClick -= MoveWorkerToBox;
        currTime = maxShiftTime;
        PlayerPrefs.SetInt("Score", points);
        Menus.LoadSceneStatic(2);
    }

    public void SetCurrentWorker(Worker worker) {
        currentWorker = worker;
    }
}