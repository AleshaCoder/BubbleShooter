using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public float Strength;
    public Vector3[] Trajection;

    public AttackData(float strength, Vector3[] trajection)
    {
        Strength = strength;
        Trajection = trajection;
    }
}
