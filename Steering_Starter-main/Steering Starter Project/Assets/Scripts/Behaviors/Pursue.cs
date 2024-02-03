using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue :Seek
{
    float maxPrediction =1f;
    protected override Vector3 getTargetPosition()
    {
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;
        float speed = character.linearVelocity.magnitude;
        float prediction =0;
        if(speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }
        Kinematic myMovingTarget = target.GetComponent<Kinematic>();
        if (myMovingTarget == null)
        {
            return base.getTargetPosition();
        }
        return (target.transform.position + myMovingTarget.linearVelocity * prediction); ;
    }
}
