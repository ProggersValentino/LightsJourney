using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun type", menuName = "ScriptableObjects/New Gun")]
public class GStatsSO : ScriptableObject
{
    [Header("type of Gun")] 
    public bool projectileBased;
    public bool rayBased;
    
    [Header("bullet prefab")]
    public GameObject bullet;

    [Header("bullet forces")]
    public float shootForce, upwardForce;

    [Header("Gun Stats")]
    public float TBShooting, spread, reloadT, TBShots;
    public int magSize, bulletsPerTap;
    public bool allowButtonHold;
    
    [Header("Raycast related stats")]
    public float range;
    public float dmg;
    

    [Header("recoil")]
    public float recoilForce;
    
    
    
#if UNITY_EDITOR
    [HideInInspector] public bool showBulletVariables = true;
    [HideInInspector] public bool showRaycastVariables = true;
#endif

    void OnValidate()
    {
        if (projectileBased)
        {
            // Disable raycast variables
            showRaycastVariables = false;

#if UNITY_EDITOR
            // Hide raycast variables in the editor
            UnityEditor.EditorGUIUtility.fieldWidth = 20;
            UnityEditor.EditorGUIUtility.labelWidth = 160;
#endif
        }
        else if (rayBased)
        {
            // Disable bullet variables
            showBulletVariables = false;

#if UNITY_EDITOR
            // Hide bullet variables in the editor
            UnityEditor.EditorGUIUtility.fieldWidth = 20;
            UnityEditor.EditorGUIUtility.labelWidth = 160;
#endif
        }
    }
}

