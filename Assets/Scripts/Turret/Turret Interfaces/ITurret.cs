using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITurret : MonoBehaviour
{
    public Node node { get; protected set; }

    protected EnemyLife target = null;
    protected float range = 4f;

    public GameObject canvas;

    public void SetNode(Node node_)
    {
        node = node_;
    }

    protected void UpdateTargetAndFire()
    {
        if (!TargetInRange())
        {
            TargetLost();
            target = null;
        }

        if (target == null)
        {
            GetTarget();
            if (target != null)
            {
                TargetFound();
            }
        }

        if (target != null && CanFire())
            Fire();

        if (target == null)
            UpdateNoTarget();
    }

    protected bool TargetInRange()
    {
        return target != null && (target.transform.position - transform.position).magnitude < range;
    }

    protected void GetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                MaybeUpdateTarget(collider);
            }
        }
    }

    void MaybeUpdateTarget(Collider collider)
    {
        float distance = (collider.transform.position - transform.position).magnitude;
        if (distance > range)
        {
            return;
        }

        if (target == null || distance < (target.transform.position - transform.position).magnitude)
        {
            target = collider.gameObject.GetComponent<EnemyLife>();
        }
    }
    virtual protected void TargetLost() { }
    virtual protected bool CanFire() { return true; }

    abstract protected void Fire();

    virtual protected void UpdateNoTarget() { }
    virtual protected void TargetFound() { }

    private void OnMouseEnter()
    {
        canvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        canvas.SetActive(false);
    }
}
