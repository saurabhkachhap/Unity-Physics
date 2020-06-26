using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
    public float fuleMass;              // [kg]
    public float maxThrust;             // N [kg.m/s^2]
    public float thrustPercent;         //  [none]
    public Vector3 thrustUnitVector;    //  [none]

    private PhysicsEngine _phisicsEngine;

    private void Awake()
    {
        _phisicsEngine = GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        _phisicsEngine.AddForce(thrustUnitVector);
    }
}
