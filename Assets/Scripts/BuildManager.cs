﻿using UnityEngine;

public class BuildManager : MonoBehaviour 
{
    public static BuildManager instance; // static, only want one instance of the build manager/meny that is shared to everyone else through public

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missleLauncherPrefab;

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } } // CanBuild property, checks if turretToBuild is equal to null, if yes, return true
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity); // store it into a temporary variable, so we can destroy it in next line
        Destroy(effect, 5f);

        Debug.Log("Turret built! Money left:" + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
    // sets the private turretToBuild equals to the turret paramenter from shop script, SelectTurretToBuild()
}