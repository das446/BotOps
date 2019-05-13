using System;
using UnityEngine;

public class NumberGoal : MonoBehaviour {

    [SerializeField] int goal;
    public static event Action<int> ScorePoints;
    public static event Action BombExplodes;
    [SerializeField] TMPro.TMP_Text text;

    void Start() {
        ChangeNumber();
    }

    private void ChangeNumber() {
        int r = UnityEngine.Random.Range(10, 200);
        goal = r;
        text.text = "" + goal;
    }

    public void Recieve(NumberBox box) {
        if (box.number == goal) {
            ScoreBox(box);
            ChangeNumber();
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