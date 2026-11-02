using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "StatSystem/StatData")]
public class StatData : ScriptableObject
{
    [Tooltip("The name of the stat. Case sensitive.")]
    [SerializeField]
    private string statName = "null";

    [Tooltip("The base value.")]
    [SerializeField]
    private float baseStatValue = 0f;


    // MAJOR ISSUE: THIS CURRENTLY AFFECTS THE ASSET NOT AN INSTANCE OF IT - MUST FIX
    [Tooltip("The current modifiers affecting the stat.")]
    [SerializeField]
    private StatModifier[] modifiers;

    public string GetName()
    {
        return statName;
    }

    public float CalculateValue()
    {
        // Adds includes both addition and subtractions
        List<float> adds = new List<float>();

        // Mults includes both multiplication and division
        List<float> mults = new List<float>();

        // Collecting and ordering the modifiers
        foreach (StatModifier mod in modifiers)
        {
            StatModifier.ModifierTypes type = mod.GetModType();

            switch (type)
            {
                case StatModifier.ModifierTypes.Add:
                    adds.Add(mod.GetValue());
                    continue;
                case StatModifier.ModifierTypes.Subtract:
                    // Negating the value so it subtracts
                    adds.Add(-mod.GetValue());
                    continue;
                case StatModifier.ModifierTypes.Multiply:
                    mults.Add(mod.GetValue());
                    continue;
                case StatModifier.ModifierTypes.Divide:
                    // Getting the reciprocal so it divides
                    mults.Add(1/mod.GetValue());
                    continue;
            }
        }


        // Total up the modifiers
        float finalValue = baseStatValue;

        foreach (float add in adds)
        {
            finalValue += add;
        }

        foreach (float mult in mults)
        {
            finalValue *= mult;
        }

        return finalValue;
    }
}
