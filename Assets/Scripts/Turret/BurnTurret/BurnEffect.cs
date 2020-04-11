using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    private EnemyLife target;
    private float currentDamage = 0f;
    private float damageGainPerTurn = 20f;

    void Start()
    {
        target = GetComponent<EnemyLife>();
    }

    void Update()
    {
        currentDamage += damageGainPerTurn * Time.deltaTime;
        target.Damage(currentDamage * Time.deltaTime);
    }
}
