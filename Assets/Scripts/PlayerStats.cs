using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StatSystem/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private StatData[] stats;

    public float GetStatValue(string statName)
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
        
        if (selectedStat != null)
        {
            return selectedStat.CalculateValue();
        }

        return 0f;
    }
}
