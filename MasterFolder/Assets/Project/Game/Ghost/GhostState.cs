using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostState  {

    /// <summary>
    /// 待機
    /// </summary>
    public class GhostStatusWait : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.WAITING;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                   GhostMain.GhostFiniteStatus.WALK,
                   GhostMain.GhostFiniteStatus.DASH,
                   GhostInfo.GhostFiniteStatus.SLOW,
                   GhostInfo.GhostFiniteStatus.ATTACK,
                   GhostInfo.GhostFiniteStatus.STAN,
                   GhostInfo.GhostFiniteStatus.DUMMY

                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }


        public override void Enter()
        {
            entity.GhostAnimation.SetInteger("State", 0);
            entity.GhostAnimation.SetInteger("Act", 0);
        }
        public override void Execute()
        {


        }
        public override void Exit()
        {

        }
    }

    public class GhostStatusWalk : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.WALK;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                   GhostMain.GhostFiniteStatus.DASH,
                   GhostInfo.GhostFiniteStatus.SLOW,
                   GhostInfo.GhostFiniteStatus.WAITING,
                   GhostInfo.GhostFiniteStatus.ATTACK,
                   GhostInfo.GhostFiniteStatus.DUMMY,
                   GhostInfo.GhostFiniteStatus.STAN

                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }


        // public override List<HumanMain.HumanFiniteStatus> NextStateIDs { get { return new List<HumanMain.HumanFiniteStatus> { HumanMain.HumanFiniteStatus.DASH }; } }

        public override void Enter()
        {
            entity.CanView = false;
        }
        public override void Execute()
        {
            entity.transform.position += new Vector3(entity.Direction.x * entity.NormalSpeed, 0, entity.Direction.z * entity.NormalSpeed);

            if (entity.Direction.magnitude > 0.1f)
            {
                Quaternion tmpRotation = Quaternion.LookRotation(entity.Direction);
                entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, tmpRotation, Time.deltaTime * 10.0f);
            }

            entity.GhostAnimation.SetInteger("State", 1);

        }
        public override void Exit()
        {

        }
    }

    public class GhostStatusDash : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.DASH;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                   GhostInfo.GhostFiniteStatus.SLOW,
                   GhostInfo.GhostFiniteStatus.WALK,
                   GhostInfo.GhostFiniteStatus.WAITING,
                   GhostInfo.GhostFiniteStatus.ATTACK


                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }

        public override void Enter()
        {
            entity.CanView = true;
        }
        public override void Execute()
        {
            entity.transform.position += new Vector3(entity.Direction.x * entity.DashSpeed, 0, entity.Direction.z * entity.DashSpeed);

            if (entity.Direction.magnitude > 0.1f)
            {
                Quaternion tmpRotation = Quaternion.LookRotation(entity.Direction);
                entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, tmpRotation, Time.deltaTime * 10.0f);
            }

            entity.GhostAnimation.SetInteger("State", 2);

        }
        public override void Exit()
        {

        }
    }

    public class GhostStatusSlow : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.SLOW;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                    GhostInfo.GhostFiniteStatus.DASH,
                   GhostInfo.GhostFiniteStatus.WALK,
                   GhostInfo.GhostFiniteStatus.WAITING,
                   GhostInfo.GhostFiniteStatus.ATTACK,
                   GhostInfo.GhostFiniteStatus.STAN,
                   GhostInfo.GhostFiniteStatus.DUMMY


                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }

        public override void Enter()
        {

        }
        public override void Execute()
        {


        }
        public override void Exit()
        {

        }
    }

    public class GhostStatusAttack : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {

        GameObject atkParticlePrefab;
        private float count;
        GameObject atkParticle;
        
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.ATTACK;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                   GhostInfo.GhostFiniteStatus.WAITING,
                   GhostInfo.GhostFiniteStatus.STAN
                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }

        public override void Enter()
        {
            entity.CanChangeStatus = false;
            if (atkParticlePrefab == null)
            {
                atkParticlePrefab = (GameObject)Resources.Load("Prefab/HandAttckParticle");
            }
            count = 0f;
            entity.GhostAnimation.SetInteger("Act", 1);
            entity.CanView = true;
            entity.AttackCollider.enabled = true;
        }
        public override void Execute()
        {
            count += Time.deltaTime;
            if (count > 0.15f)
            {
                if (atkParticle == null)
                {
                    atkParticle = (GameObject)MonoBehaviour.Instantiate(atkParticlePrefab, entity.transform.position, entity.transform.rotation);
                    atkParticle.transform.parent = entity.transform;
                    //CSoundManager.Instance.PlaySE(EAudioList.SE_HumanScream);

                }

                entity.transform.Translate(entity.transform.forward * entity.NormalSpeed * Time.deltaTime * 10, Space.World);
            }

            if (CAnimetionController.IsMotionEnd(entity.GhostAnimation,"Atk"))
            {
                entity.GhostStatusMessage = GhostInfo.GhostFiniteStatus.WAITING;
                entity.CanChangeStatus = true;
                entity.GhostStateMachine.ChangeState(entity.GhostStateMachine.GetRegisterState(GhostInfo.GhostFiniteStatus.WAITING));


            }
        }
        public override void Exit()
        {
            entity.GhostAnimation.SetInteger("Act", 0);
            Object.Destroy(atkParticle);
            atkParticle = null;
            entity.CanView = false;
            entity.AttackCollider.enabled = false;
        }
    }

    public class GhostStatusDummy : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.DUMMY;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                   GhostInfo.GhostFiniteStatus.WAITING,
                   GhostInfo.GhostFiniteStatus.STAN
                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }

        public override void Enter()
        {
            entity.CanChangeStatus = false;
        }
        public override void Execute()
        {


        }
        public override void Exit()
        {

        }
    }

    public class GhostStatusStan : FSMState<GhostMain, GhostMain.GhostFiniteStatus>
    {
        public override GhostMain.GhostFiniteStatus StateID
        {
            get
            {
                return GhostMain.GhostFiniteStatus.STAN;
            }
        }

        public override List<GhostMain.GhostFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<GhostMain.GhostFiniteStatus> {
                   GhostInfo.GhostFiniteStatus.WAITING
                };
            }
        }

        public override bool CanEnter(FSMState<GhostMain, GhostMain.GhostFiniteStatus> currentState)
        {
            if (entity.GhostStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;

        }

        public override void Enter()
        {
            entity.CanChangeStatus = false;
        }
        public override void Execute()
        {


        }
        public override void Exit()
        {

        }
    }


}
