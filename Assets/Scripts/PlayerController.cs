using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ball
{
    public Mana mana;

    [Header("Reference")]
    public Camera referenceCameraToMove;


    new void Start()
    {
        base.Start();

        tagTarget = "Enemy";
    }

    new void FixedUpdate()
    {
        if (GameManager.Instance.IsActive())
        {
            base.FixedUpdate();

            MovementControl();
        }
    }


    new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        GameObject target = collision.gameObject;

        //if (target.CompareTag(this.tagTarget))
        //{
        //    Ball targetBall = target.GetComponent<Ball>();

        //    // Player duoc tang mana khi va cham voi enemy, cong thuc tang mana: Luc day hien tai cua target +- 10%.
        //    //this.CurrentMana += targetBall.CurrentThrustForce / 100;
        //}
    }


    void MovementControl()
    {
        // Get value axis of vertical and horizontal from keyboard
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move player according to value axis input
        rb.AddForce(Vector3.forward * verticalInput * moveSpeed.Current);
        rb.AddForce(Vector3.right * horizontalInput * moveSpeed.Current);
    }

    protected override void DetectOutBound()
    {
        if (IsOutBond())
        {
            GameManager.Instance.SetStateGame(GameState.Over);
            Destroy(gameObject);
        }
    }
}
