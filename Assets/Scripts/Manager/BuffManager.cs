using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager buffManager;

    public GameObject AttackRateBuffIndicator;
    public GameObject DamageBuffIndicator;
    public GameObject RangeBuffIndicator;

    void Start()
    {
        if (buffManager != null)
        {
            Debug.Log("MULTIPLE BUFF MANAGERS!!!");
            return;
        }
        buffManager = this;
    }

    public IBuff GetBuffObject(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.ATTACK_RATE_UP:
                return new AttackRateBuff();
            case BuffType.DAMAGE_UP:
                return new DamageBuff();
            case BuffType.RANGE_UP:
                return new RangeBuff();
            default:
                return null;
        }
    }

    public GameObject GetIndicatorPrefab(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.ATTACK_RATE_UP:
                return AttackRateBuffIndicator;
            case BuffType.DAMAGE_UP:
                return DamageBuffIndicator;
            case BuffType.RANGE_UP:
                return RangeBuffIndicator;
            default:
                return null;
        }
    }
}
