using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public Kinematic character;
    public float maxAcceleration = 10;
    public List<Kinematic> targets;
    public float radius = .1f;

    public override SteeringOutput getSteering()
    {
        float shortestTime = Mathf.Infinity;
        Kinematic firstTarget = null;
        float firstMinSeperation = Mathf.Infinity;
        float firstDistance = Mathf.Infinity;
        Vector3 firstRelativePos = Vector3.positiveInfinity;
        Vector3 firstRelativevel = Vector3.zero;
        Vector3 relativePos = Vector3.positiveInfinity;
        Vector3 relativeVel = Vector3.zero;
        float relativeSpeed;
        foreach (Kinematic target in targets)
        {
            relativePos = target.transform.position - character.transform.position;
            relativeVel = character.linearVelocity - target.linearVelocity;
            relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel)/(relativeSpeed * relativeSpeed);
            float distance = relativePos.magnitude;
            float minSeperation = distance - relativeSpeed * timeToCollision;
            if(minSeperation > 2 * radius)
            {
                continue;
            }
            if(timeToCollision >0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeperation = minSeperation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativevel = relativeVel;
            }
        }
        if (firstTarget == null)
        {
            return null;
        }
        SteeringOutput result = new SteeringOutput();
        float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
        if (dotResult < -.9)
        {
            relativePos = -firstTarget.transform.right;
        }
        else
        {
            relativePos = -firstTarget.linearVelocity;
        }
        relativePos.Normalize();
        result.linear = relativePos * maxAcceleration;
        result.angular = 0;
        return result;
    }
}
