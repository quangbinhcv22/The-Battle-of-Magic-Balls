using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    protected Rigidbody rb;

    [Space]
    public MoveSpeed moveSpeed;
    public ThrustForce thrustForce;
    public Resistance resistance;

    protected string tagTarget = "Untagged";

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        UpdateStrengthStat();
        DetectOutBound();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            CreateThrust(collision.gameObject.GetComponent<Ball>());
        }
    }

    public void CreateThrust(Ball targetBall)
    {
        GameObject target = targetBall.gameObject;

        if (target.tag == this.tagTarget)
        {
            Rigidbody targetRigibody = target.GetComponent<Rigidbody>();

            float ThrustForce = this.thrustForce.Current * (1 - targetBall.resistance.Current);

            targetRigibody.AddForce(ThrustForce * GetDirectionToTarget(target.gameObject));
        }

    }

    protected Vector3 GetDirectionToTarget(GameObject target, bool isNormalized = false)
    {
        Vector3 positionTarget = target.transform.position;
        Vector3 positionMyself = this.transform.position;

        // Direction vector to target is difference between position coordinates of target and self
        Vector3 directionToTarget = positionTarget - positionMyself;

        if (isNormalized)
        {
            return directionToTarget.normalized;
        }
        else
        {
            return directionToTarget;
        }
    }

    protected void UpdateStrengthStat()
    {
        this.moveSpeed.Current = this.moveSpeed.Start;
        this.thrustForce.Current = this.thrustForce.Start;
        this.resistance.Current = this.resistance.Start;
    }


    protected virtual void DetectOutBound()
    {
        if (IsOutBond())
        {
            Destroy(gameObject);
        }
    }

    protected bool IsOutBond()
    {
        float positionBound = 15;
        bool isOutBound = Mathf.Abs(this.transform.position.x) > positionBound || Mathf.Abs(this.transform.position.y) > positionBound || Mathf.Abs(this.transform.position.z) > positionBound;

        return isOutBound;
    }

    protected void MoveTo(GameObject target, bool isNormalizedVelocity)
    {
        rb.AddForce(GetDirectionToTarget(target, isNormalizedVelocity) * this.moveSpeed.Current);
    }

}


[Serializable]
public struct MoveSpeed
{
    [SerializeField, Min(0)]
    private float start;
    public float Start
    {
        get
        {
            return start;
        }

        set
        {
            if (value <= 0)
            {
                start = 0;
            }
            else
            {
                start = value;
            }
        }
    }

    private float current;
    public float Current
    {
        get
        {
            return current;
        }

        set
        {
            if (value <= 0)
            {
                current = 0;
            }
            else
            {
                current = value;
            }
        }
    }
}

[Serializable]
public struct ThrustForce
{
    [SerializeField, Min(0), Tooltip("When ball collides with enemy, it applies this force to that ball")]
    private float start;
    public float Start
    {
        get
        {
            return start;
        }

        set
        {
            if (value <= 0)
            {
                start = 0;
            }
            else
            {
                start = value;
            }
        }
    }

    private float current;
    public float Current
    {
        get
        {
            return current;
        }

        set
        {
            if (value <= 0)
            {
                current = 0;
            }
            else
            {
                current = value;
            }
        }
    }


    public ThrustForce(float start)
    {
        this.start = start;
        this.current = 0;
    }
}

[Serializable]
public struct Resistance
{
    [SerializeField, Range(0, 1), Tooltip("Reduces negative forces acting on the ball")]
    private float start;
    public float Start
    {
        get
        {
            return start;
        }

        set
        {
            if (value <= 0)
            {
                start = 0;
            }
            else if (value >= 1)
            {
                start = 1;
            }
            else
            {
                start = value;
            }
        }
    }

    private float current;
    public float Current
    {
        get
        {
            return current;
        }

        set
        {
            if (value <= 0)
            {
                current = 0;
            }
            else if (value >= 1)
            {
                current = 1;
            }
            else
            {
                current = value;
            }
        }
    }


    public Resistance(float start)
    {
        this.start = start;
        this.current = 0;
    }
}

[Serializable]
public struct Mana
{
    [SerializeField, Min(0)]
    private float max;
    public float Max
    {
        get
        {
            return max;
        }

        set
        {
            if (Max <= 0)
            {
                max = 0;
            }
            else
            {
                max = value;
            }
        }
    }

    private float current;
    public float Current
    {
        get
        {
            return current;
        }

        set
        {
            if (value >= max)
            {
                current = max;
            }
            else if (value <= 0)
            {
                current = 0;
            }
            else
            {
                current = value;
            }
        }
    }


    public Mana(float max)
    {
        this.max = max;
        this.current = 0;
    }
}
