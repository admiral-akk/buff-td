using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [HideInInspector]
    public static BuildManager buildManager;

    public GameObject buildUIObject;

    [Header("Turrets")]
    [SerializeField]
    public Blueprint attackRateBuff;
    [SerializeField]
    public Blueprint basicTurret;
    [SerializeField]
    public Blueprint damageBuff;
    [SerializeField]
    public Blueprint rangeBuff;

    public GameObject[] turretUpgradePrefabs;

    private Blueprint selectedTower;
    private BuildUI buildUI;

    void Start()
    {
        if (buildManager != null)
        {
            Debug.Log("MULTIPLE BUILD MANAGERS!!!");
            return;
        }
        buildManager = this;
        buildUI = buildUIObject.GetComponent<BuildUI>();
    }

    public void SelectAttackRateBuff()
    {
        selectedTower = attackRateBuff;
        buildUI.AttackRateIsSelected();
    }

    public void SelectBasicTurret()
    {
        selectedTower = basicTurret;
        buildUI.TurretIsSelected();
    }

    public void SelectDamageBuff()
    {
        selectedTower = damageBuff;
        buildUI.DamageIsSelected();
    }

    public void SelectRangeBuff()
    {
        selectedTower = rangeBuff;
        buildUI.RangeIsSelected();
    }

    public void Unselect()
    {
        selectedTower = null;
    }

    public bool CanBuild()
    {
        return selectedTower != null &&
            PlayerManager.playerManager.GetMoney() >= selectedTower.cost;
    }

    public GameObject BuildSelectedTurret()
    {
        PlayerManager.playerManager.TurretBuilt(selectedTower.cost);
        return selectedTower.towerPrefab;
    }

    public bool IsAttackTower()
    {
        return selectedTower.towerPrefab == basicTurret.towerPrefab;
    }

    public GameObject GetPreview()
    {
        return selectedTower.towerBlueprintPrefab;
    }

    public GameObject GetTurret(ISet<BuffType> buffs)
    {
        int index = 0;
        if (buffs.Contains(BuffType.ATTACK_RATE_UP))
            index += 1;
        if (buffs.Contains(BuffType.DAMAGE_UP))
            index += 2;
        if (buffs.Contains(BuffType.RANGE_UP))
            index += 4;
        return turretUpgradePrefabs[index - 1];
    }
}
