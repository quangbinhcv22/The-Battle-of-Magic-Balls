using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Ball
{
    [Space]
    public bool isNormalizedVelocity;
    public bool isCanMove;

    [Space]
    public int scoreWhenDie;

    private GameObject player;


    new void Start()
    {
        base.Start();

        tagTarget = "Player";

        player = GameObject.Find("Player");

    }

    new void FixedUpdate()
    {
        if (GameManager.Instance.IsActive())
        {
            base.FixedUpdate();

            if (isCanMove && player != null)
            {
                MoveTo(player, isNormalizedVelocity);
            }
        }
    }


    protected override void DetectOutBound()
    {
        if (IsOutBond())
        {
            GameManager.Instance.UpdateScore(scoreWhenDie);
            Destroy(gameObject);
        }
    }
}
