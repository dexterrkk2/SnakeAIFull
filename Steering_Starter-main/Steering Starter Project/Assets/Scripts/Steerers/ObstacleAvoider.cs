using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoider : Kinematic
{
    ObstacleAvoidance myMoveType;
    public bool flee = false;
    Face mySeekRotateType;
    LookWhereGoing myFleeRotateType;
    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new ObstacleAvoidance();
        myMoveType.character = this;
        mySeekRotateType = new Face();
        mySeekRotateType.character = this;

        myFleeRotateType = new LookWhereGoing();
        myFleeRotateType.character = this;
    }

    // Update is called once per frame
    protected override void Update()
    {
        myMoveType.target = myTarget;
        mySeekRotateType.target = myTarget;
        myFleeRotateType.target = myTarget;
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : mySeekRotateType.getSteering().angular;
        base.Update();
    }
}
