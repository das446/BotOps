using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class NumberGoal : MonoBehaviour, IWorkerCanMoveTo
{

    [SerializeField] int goal;
    public static event Action<int> ScorePoints;
    public static event Action<Bomb> BombExplodes;
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] Image timeBar;
    private float currGoalTime;
    public float maxTime;

    public static event Action<NumberGoal> OnClick;

    public Transform transform => base.transform;

    void Start()
    {
        ChangeNumber();
        currGoalTime = maxTime;
    }

    void Update()
    {
        if (currGoalTime > 0f)
        {
            currGoalTime -= Time.deltaTime;
        }
        else
        {
            ChangeNumber();
            currGoalTime = maxTime;
            ScorePoints(-10);
        }
        float scale = currGoalTime / maxTime;
        scale = scale > 1 ? 1 : scale;
        timeBar.transform.localScale = new Vector3(1f, currGoalTime / maxTime, 1f);
    }

    private void ChangeNumber()
    {
        int r = UnityEngine.Random.Range(10, 200);
        goal = r;
        text.text = "" + goal;
    }

    public void Recieve(NumberBox box)
    {
        if (box.number == goal)
        {
            ScoreBox(box);
            ChangeNumber();
            currGoalTime = maxTime;
        }
        else{
            box.Discard();
        }
    }

    private void ScoreBox(NumberBox box)
    {
        ScorePoints(goal);
        Destroy(box);
    }

    public void Recieve(Bomb bomb)
    {
        BombExplodes(bomb);
        Debug.Log("Destroy Bomb");
    }


    private void OnMouseDown()
    {
        OnClick(this);
    }


    public void OnWorkerReach(Worker w)
    {
        
        if (w.HasItem())
        {
            IPickupable p = w.DropItem();

            if(p.IsNumber){
                Debug.Log("recieve number box");
                Recieve((NumberBox)p);
            }
            else{
                Debug.Log("recieve bomb");
                Recieve((Bomb)p);
            }
        }
    }
}