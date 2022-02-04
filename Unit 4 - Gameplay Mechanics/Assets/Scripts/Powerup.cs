using UnityEngine;

public enum PowerupType
{
    None,
    Push,
    Rocket,
    Smash
}
public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
}
