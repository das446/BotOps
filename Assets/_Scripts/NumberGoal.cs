using System;
using UnityEngine;

public class NumberGoal : MonoBehaviour {

    [SerializeField] int goal;
    public static event Action<int> ScorePoints;
    public static event Action BombExplodes;

    public void Recieve(NumberBox box) {
        if (box.number == goal) {
            ScoreBox(box);
        } else {
            box.Discard();
        }
    }

    private void ScoreBox(NumberBox box) {
        ScorePoints(goal);
        Destroy(box);
    }

    public void Recieve(Bomb bomb) {
        BombExplodes();
    }
}