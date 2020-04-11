using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color buildOverlayColor;
    public Color cannotBuildColor;
    public GameObject rangePrefab;
    public GameObject buffIndicatorPrefab;

    private GameObject turret;
    private Vector3 offset = new Vector3(0, 0.1f, 0);
    private Color originalColor;
    private Renderer renderer;
    private IList<GameObject> blueprintObjects;

    private IList<Node> adjacentNodes;
    private ISet<BuffType> buffs;

    public void SetAdjacentNodes(IList<Node> nodes_)
    {
        adjacentNodes = nodes_;
    }

    private void Start()
    {
        buffs = new HashSet<BuffType>();
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        blueprintObjects = new List<GameObject>();
    }

    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && turret == null)
        {
            if (BuildManager.buildManager.CanBuild())
            {
                renderer.material.color = buildOverlayColor;
                blueprintObjects.Add(Instantiate(BuildManager.buildManager.GetPreview(), GetBuildPosition(), Quaternion.identity));

                if (BuildManager.buildManager.IsAttackTower())
                {
                    blueprintObjects.Add(Instantiate(rangePrefab, GetBuildPosition(), Quaternion.identity));
                } else
                {
                    foreach (Node node in adjacentNodes)
                    {
                        blueprintObjects.Add(Instantiate(buffIndicatorPrefab, node.GetBuildPosition(), Quaternion.identity));
                    }
                }
            }
            else
            {
                renderer.material.color = cannotBuildColor;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && BuildManager.buildManager.CanBuild() && turret == null)
        {
            GameObject turret_ = Instantiate(BuildManager.buildManager.BuildSelectedTurret(), GetBuildPosition(), Quaternion.identity);
            turret = turret_;

            if (turret.GetComponent<ITurret>() != null && buffs.Count > 0)
            {
                UpgradeTurret();
            }
            BuffTower buffTower = turret.GetComponent<BuffTower>();
            if (buffTower != null)
            {
                foreach (Node node in adjacentNodes)
                {
                    node.AddBuff(buffTower.buff);
                }
            }
            ResetColour();
        }
    }

    private void AddBuff(BuffType buff)
    {
        if (!buffs.Contains(buff))
        {
            buffs.Add(buff);
            if (turret != null && turret.GetComponent<ITurret>() != null)
                {
                    UpgradeTurret();
                }
        }
    }

    private void OnMouseExit()
    {
        ResetColour();
    }

    private void ResetColour()
    {
        renderer.material.color = originalColor;
        foreach (GameObject go in blueprintObjects)
        {
            Destroy(go);
        }
        blueprintObjects.Clear();
    }

    private Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }

    private void UpgradeTurret()
    {
        GameObject newTurret = BuildManager.buildManager.GetTurret(buffs);
        Destroy(turret);
        turret = Instantiate(newTurret, GetBuildPosition(), Quaternion.identity);
    }
}
