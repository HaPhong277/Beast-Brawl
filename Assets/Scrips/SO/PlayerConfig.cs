using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float MoveSpeed;
    public float JumpForce;

    public int CURRENT_DAME = 0;
    public int CURRENT_BLOOD = 0;

    public bool doubleJump;
    public bool MODE_ATTACK;
}
