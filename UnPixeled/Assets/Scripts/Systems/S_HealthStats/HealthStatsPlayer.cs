//Copyright Ex/IO 2020

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;


public class HealthStatsPlayer : MonoBehaviour
{
    FloatingText damageText;

    public bool disableDeaht = false;
    public float health = 100;
    public float healthMax = 100;
    public float healthRegeneration;

    public float expirience = 0;

    public float energy = 100;
    public float energyMax = 100;
    public float energyRegeneration = 1;

    public float energyUseForDash = 20;
    public float minimumEnergyForDash = 20;

    public float energyUsageForDefence = 20;
    public float minimumEnergyForDefence = 20;


    void Awake()
    {
        EventManager.playerStatAction.AddListener(HealthStatsAction);
    }

    private void Start()
    {
        damageText = GameManager.instance.floatingText.GetComponent<FloatingText>();
    }


    void Update()
    {
        RegenerateHealth();
        RegenerateEnergy();
        EventManager.updatePlayerStats.Invoke(health, energy, expirience);
    }


    void HealthStatsAction(String _typeOfAction, String _typeOfStat, float _value)
    {
        switch (_typeOfAction)
        {
            case "add":
                AddStat(_typeOfStat, _value);
                break;
            case "remove":
                DropStat(_typeOfStat, _value);
                break;
        }
    }

    public void DropStat(String type, float _value) //доработать Min Max
    {
        if (!disableDeaht)
            switch (type)
            {
                case "health":
                    if (!GameManager.instance.inputManager.Dash())
                    {
                        health -= _value;
                        damageText.showText(transform, _value, 0);
                        if (health < 0)
                        {
                            EventManager.playerDead.Invoke();
                        }
                    }

                    break;

                case "energy":
                    if (energy > 1)
                        energy -= _value * Time.deltaTime;
                    else
                    {
                        EventManager.disableDash.Invoke(true);
                        EventManager.disableDefence.Invoke(true);
                    }

                    break;
            }
    }

    public void AddStat(String _typeOfStat, float value) //доработать Min Max
    {
        switch (_typeOfStat)
        {
            case "health":
                if (health <= 100)
                    health += value;
                break;

            case "energy":
                if (energy <= 100)
                    energy += value;
                break;
        }
    }

    void RegenerateHealth()
    {
        if (health <= 100)
            health += healthRegeneration * Time.deltaTime;
    }

    void RegenerateEnergy()
    {
        if (!GameManager.instance.inputManager.Dash() && energy <= energyMax)
            energy += energyRegeneration * Time.deltaTime;

        if (energy > minimumEnergyForDash)
            EventManager.disableDash.Invoke(false);
        if (energy > minimumEnergyForDefence)
            EventManager.disableDefence.Invoke(false);
    }

    //SaveLoad
    public void ResetPlayer()
    {
        health = healthMax;
        energy = energyMax;
    }
}