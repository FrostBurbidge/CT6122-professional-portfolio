using System;
using UnityEngine;

public class StatPickup : MonoBehaviour
{
    [Tooltip("The stat modifier.")]
    [SerializeField]
    StatModifier modifier;

    [Tooltip("The stat name (Case Sensitive).")]
    [SerializeField]
    string statName;

    [Tooltip("Whether the pickup can be used multiple times.")]
    [SerializeField]
    bool repeatable = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject) {
            GameObject go = collision.gameObject;
            if (go.GetComponent<PlayerMovement>()) {
                go.GetComponent<PlayerMovement>().ApplyModifier(statName, modifier);
                if (!repeatable) {
                    Destroy(gameObject);
                }
            }
        }
    }
}
