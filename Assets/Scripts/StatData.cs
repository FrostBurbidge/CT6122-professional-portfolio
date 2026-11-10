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

    [Tooltip("The current modifiers affecting the stat.")]
    [SerializeField]
    private List<StatModifier> modifiers;

    public string GetName()
    {
        return statName;
    }

    public void AddModifier(StatModifier mod)
    {
        modifiers.Add(mod);
    }

    public void RemoveModifier(StatModifier mod)
    {
        modifiers.Remove(mod);
    }

    public float CalculateValue()
    {
        // Adds includes both addition and subtractions
        List<float> adds = new List<float>();

        // Mults includes both multiplication and division
        // Mults are in %
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
                    // Negating the value so it subtracts from the mod amount
                    mults.Add(-mod.GetValue());
                    continue;
            }
        }


        // Total up the modifiers
        float finalValue = baseStatValue;

        foreach (float add in adds)
        {
            finalValue += add;
        }

        // Base 1, with percentages added
        float totalMult = 100;
        foreach (float mult in mults)
        {
            totalMult += mult;
        }

        // Converting out of percentage
        totalMult /= 100;

        finalValue *= totalMult;

        return finalValue;
    }
}
