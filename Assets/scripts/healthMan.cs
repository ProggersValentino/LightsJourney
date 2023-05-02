using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "stats", menuName = "ScriptableObjects/Stats")]
public class healthMan : ScriptableObject
{
    public float maxHealth;
}
