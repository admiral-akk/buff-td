using UnityEngine;

public abstract class IBullet : MonoBehaviour
{
    protected float speed = 30f;

    protected EnemyLife target;

    public ParticleSystem bulletHit;

    public void Fire(EnemyLife enemy)
    {
        target = enemy;
    }

    virtual protected void Update()
    {
        if (target == null)
        {
            TargetNull();
            return;
        }

        Vector3 dir = target.transform.position - transform.position;

        if (dir.magnitude < 0.5f)
        {
            Damage();
            return;
        }
        transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

    protected virtual void Damage() {
        if (bulletHit != null)
        {
            ParticleSystem hitEffect = Instantiate(bulletHit, transform.position, Quaternion.LookRotation(transform.position - target.transform.position));
            Destroy(hitEffect.gameObject, 2f);
        }
    }

    virtual public void TargetTeleport()
    {
        Die();
    }

    virtual protected void TargetNull()
    {
        Die();
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
