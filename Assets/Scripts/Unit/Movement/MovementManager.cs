using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Player player;
    public PlayerMovement playermovement;
    public AimPoint aimpoint;
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
