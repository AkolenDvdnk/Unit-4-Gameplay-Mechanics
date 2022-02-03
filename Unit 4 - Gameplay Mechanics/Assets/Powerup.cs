using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    None,
    Push,
    Rocket
}
public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
}
