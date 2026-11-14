using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [Tooltip("The textbox to use to display the stats.")]
    [SerializeField]
    private TextMeshProUGUI text;

    public void DisplayStats(PlayerStats stats)
    {
        string newText = "";

        // Iterating through the stats
        foreach (string stat in stats.GetStatNames())
        {
            float value = stats.GetStatValue(stat);

            newText += stat + ": " + value.ToString() + "\n";
        }

        text.text = newText;
    }
}
