using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StatSystem/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private StatData[] stats;

    public void Setup()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = ScriptableObject.Instantiate(stats[i]);
        }
    }

    public float GetStatValue(string statName)
    {
        StatData selectedStat = GetStat(statName);
        
        if (selectedStat != null)
        {
            return selectedStat.CalculateValue();
        }

        return 0f;
    }

    public void ApplyModifier(string statName, StatModifier mod)
    {
        StatData selectedStat = GetStat(statName);

        if (selectedStat != null)
        {
            selectedStat.AddModifier(mod);
        }
    }

    public void RemoveModifier(string statName, StatModifier mod)
    {
        StatData selectedStat = GetStat(statName);

        if (selectedStat != null)
        {
            selectedStat.RemoveModifier(mod);
        }
    }

    // To get the statdata, used in other public functions
    private StatData GetStat(string statName)
    {
        StatData selectedStat = null;

        foreach (StatData stat in stats)
        {
            if (stat.GetName() == statName)
            {
                selectedStat = stat;
                break;
            }
        }

        return selectedStat;
    }

    // Get the names of all the stats
    public string[] GetStatNames()
    {
        string[] names = new string[stats.Length];

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = stats[i].GetName();
        }

        return names;
    }
}
