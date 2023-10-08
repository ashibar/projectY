using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Player player;
    private PlayerMovement playermovement;
    private AimPoint aimpoint;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        playermovement = GetComponentInChildren<PlayerMovement>();
        aimpoint = GetComponentInChildren<AimPoint>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
