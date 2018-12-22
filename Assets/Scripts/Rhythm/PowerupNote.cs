using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupNote : Note
{
    [SerializeField]
    public Powerup powerup;

    PowerupController powerupController;

    private void Start()
    {
        powerupController = FindObjectOfType<PowerupController>();    
    }

    public override void ProcessInput()
    {
        powerupController.ActivatePowerup(this);
    }
}
