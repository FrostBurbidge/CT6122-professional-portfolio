using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(stats.GetStatValue("Speed"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
