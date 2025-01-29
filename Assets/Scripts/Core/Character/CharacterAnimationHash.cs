using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterAnimationHash
{
    static CharacterAnimationHash()
    {
        PlayerInputX = Animator.StringToHash("InputX");
        PlayerInputY = Animator.StringToHash("InputY");
    }


    public static int PlayerInputX;
    public static int PlayerInputY;
}
