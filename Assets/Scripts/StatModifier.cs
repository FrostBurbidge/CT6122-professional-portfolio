using UnityEngine;


[CreateAssetMenu(menuName = "StatSystem/StatModifier")]
public class StatModifier : ScriptableObject
{
    public enum ModifierTypes { Add, Subtract, Multiply, Divide }

    [Tooltip("The type of modifier.")]
    [SerializeField]
    private ModifierTypes type;

    [Tooltip("The value of the modifier.")]
    [SerializeField]
    [Range(0, 100)]
    private float value = 1;

    [Tooltip("Whether the modifier is permanent.")]
    [SerializeField]
    private bool permanent = false;

    [Tooltip("The duration of the modifier in seconds.")]
    [SerializeField]
    [Range(0, 600)]
    private float duration = 0;

    public ModifierTypes GetModType()
    {
        return type;
    }

    public float GetValue()
    {
        return value;
    }
}
