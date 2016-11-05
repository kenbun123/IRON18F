using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanState : MonoBehaviour {


    public class HumanStatusWaiting : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get {
                return HumanMain.HumanFiniteStatus.WAITING; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus> {
                   HumanInfo.HumanFiniteStatus.WALK,
                   HumanInfo.HumanFiniteStatus.DASH
                };
            }
        }
        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        // public override List<HumanMain.HumanFiniteStatus> NextStateIDs { get { return new List<HumanMain.HumanFiniteStatus> { HumanMain.HumanFiniteStatus.DASH }; } }

        public override void Enter()
        {
            entity.HumanAnimation.SetInteger("State", 0);
            entity.HumanAnimation.SetInteger("Act",0);
        }
        public override void Execute()
        {

            entity.HumanAnimation.SetInteger("State", 0);
            entity.HumanAnimation.SetInteger("Act", 0);
        }
        public override void Exit()
        {

        }
    }

    public class HumanStatusWalk : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.WALK; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs {
            get { return new List<HumanMain.HumanFiniteStatus> {
            HumanInfo.HumanFiniteStatus.WAITING,
            HumanInfo.HumanFiniteStatus.DASH};
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override void Enter()
        {

        }
        public override void Execute()
        {

            // 移動する向きとスピードを代入する
            // 移動する向きとスピードを代入する
            entity.transform.position += new Vector3(entity.MoveDirection.x * entity.NormalSpeed, 0, entity.MoveDirection.z * entity.NormalSpeed );

            if (entity.MoveDirection.magnitude > 0.1f)
            {
                Quaternion tmpRotation = Quaternion.LookRotation(entity.MoveDirection);
                entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation,tmpRotation,Time.deltaTime * 10.0f);
            }
            entity.HumanAnimation.SetInteger("State", 1);
        }
        public override void Exit()
        {

        }

    }

    public class HumanStatusDash : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.DASH; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus> {
            HumanInfo.HumanFiniteStatus.WAITING,
                HumanInfo.HumanFiniteStatus.WALK};
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override void Enter()
        {

        }
        public override void Execute()
        {

            // 移動する向きとスピードを代入する
            // 移動する向きとスピードを代入する
            entity.transform.position += new Vector3(entity.MoveDirection.x * entity.DashSpeed, 0, entity.MoveDirection.z * entity.DashSpeed);

            if (entity.MoveDirection.magnitude > 0.1f)
            {
                Quaternion tmpRotation = Quaternion.LookRotation(entity.MoveDirection);
                entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, tmpRotation, Time.deltaTime * 10.0f);
            }
            entity.HumanAnimation.SetInteger("State", 2);
        }
        public override void Exit()
        {

        }

    }


}
