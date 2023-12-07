using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBoxController : MonoBehaviour
{
    public void ToggleHitBox()
    {
        GetComponentInChildren<WeaponHitBox>().hitBox.enabled = !GetComponentInChildren<WeaponHitBox>().hitBox.enabled;
    }
}
