using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    public override SteeringOutput getSteering()
    {
        float targetAngle = 0;
        float wanderOffset = 1;
        float wanderRadius = 1;

        float wanderRate= 10;
        float maxAcceleration = 10;
        float wanderOrientation = Random.Range(0, wanderRate);
        targetAngle= wanderOrientation + character.angularVelocity;
        Vector3 targetWander = character.transform.position + (wanderOffset * character.transform.forward);
        targetWander += wanderRadius * target.transform.forward;
        targetWander.Normalize();
        //target.transform.position = targetWander;
        SteeringOutput result = base.getSteering();
        result.linear = -targetWander *maxAcceleration;
        //result.angular = targetAngle;
        return result;
    }
}
