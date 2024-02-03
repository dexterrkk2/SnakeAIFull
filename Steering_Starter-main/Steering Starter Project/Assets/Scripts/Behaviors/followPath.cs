using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPath : Seek
{
    public List<GameObject> path;
    int counter = 0;
    float damageRadius = 10;
    protected override Vector3 getTargetPosition()
    {
        if((target.transform.position - character.transform.position).magnitude <=damageRadius)
        {
            counter++;
            if (counter >= path.Count)
            {
                counter = 0;
            }
            target = path[counter];
        }
        return base.getTargetPosition();
    }
}
