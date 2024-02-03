using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    float lookAhead = 10f;
    float avoidDistance = 16f;
    float rotateDistance = 45;
    protected override Vector3 getTargetPosition()
    {
        Transform rotation = character.transform;
        Vector3 Forward = checkDirection(character.linearVelocity, lookAhead);
        rotation.Rotate(0, rotateDistance, 0);
        Vector3 rotated = checkDirection(rotation.forward, lookAhead/3);
        rotation.Rotate(0, -rotateDistance*2, 0);
        Vector3 inverseRotated = checkDirection(rotation.forward, lookAhead/3);
        rotation.Rotate(0, rotateDistance, 0);
        Vector3 bacwards = checkDirection(-character.linearVelocity, lookAhead*2);
        bool targetBackwards = bacwards != Vector3.zero;
        bool targetComingright = rotated != Vector3.zero;
        bool targetComingleft = inverseRotated != Vector3.zero;
        bool targetComingStraight = Forward != Vector3.zero;
        bool doubleSided = targetBackwards && targetComingStraight;
        bool justLeft = targetComingleft && !targetComingright && !targetComingStraight;
        bool justRight = targetComingright && !targetComingleft && !targetComingStraight;
        bool justStraight = targetComingStraight && !targetComingright && !targetComingleft;
        bool justbackwards = targetBackwards && !targetComingright && !targetComingleft && !targetComingStraight;
        bool bothAngles = targetComingleft && targetComingright;
        bool leftStraight = targetComingleft && targetComingStraight;
        bool rightStraight = targetComingright && targetComingStraight;
        bool all = bothAngles && targetComingStraight;
        if (all || bothAngles)
        {
            return -character.transform.forward * avoidDistance;
        }
        else if(leftStraight || doubleSided || justbackwards)
        {
            return character.transform.right* avoidDistance;
        }
        else if (rightStraight)
        {
            return -character.transform.right * avoidDistance;
        }
        else if (justStraight)
        {
            return Forward;
        }
        else if (justRight)
        {
            return rotated;
        }
        else if (justLeft)
        {
            return inverseRotated;
        }
        else
        {
            return base.getTargetPosition();
        }
    }
    Vector3 Shootray(Vector3 position, Vector3 direction, float rayDistance)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, rayDistance))
        {
            Vector3 something = hit.point + (hit.normal * avoidDistance);
            something = new Vector3(something.x, something.y, something.z);
            if (!hit.collider.tag.Equals("apple"))
            {
                Debug.DrawRay(position, direction * hit.distance, Color.red, 0.5f);
                return something *avoidDistance;
            }
            else
            {
                Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.green, 0.5f);
                //Debug.Log("apple");
                return base.getTargetPosition();
            }
        }
        else 
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.green, 0.5f);
            return Vector3.zero; 
        }
    }
    Vector3 checkDirection(Vector3 direction, float rayDistance)
    {
        Vector3 linear = Shootray(character.transform.position, direction, rayDistance);
        if (linear == Vector3.zero)
        {
            return Vector3.zero;
        }
        else
        {
            //roation.Rotate(0, -rotateDistance, 0);
            return linear *avoidDistance;
        }
    }
}
