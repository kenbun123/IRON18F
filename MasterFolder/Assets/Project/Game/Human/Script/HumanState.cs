using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanState : MonoBehaviour {

    /// <summary>
    /// 待機
    /// </summary>
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
                   HumanInfo.HumanFiniteStatus.DASH,
                   HumanInfo.HumanFiniteStatus.PUT_CANDLE,
                   HumanInfo.HumanFiniteStatus.PICK_UP_CANDY,
                   HumanInfo.HumanFiniteStatus.USE_CANDY,
                   HumanInfo.HumanFiniteStatus.DEATH,
                   HumanInfo.HumanFiniteStatus.ACTION
                  
                };
            }
        }
        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            return true;
            
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

    /// <summary>
    /// 歩く
    /// </summary>
    public class HumanStatusWalk : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.WALK; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs {
            get { return new List<HumanMain.HumanFiniteStatus> {
            HumanInfo.HumanFiniteStatus.WAITING,
            HumanInfo.HumanFiniteStatus.DASH,
            HumanInfo.HumanFiniteStatus.PICK_UP_CANDLE,
            HumanInfo.HumanFiniteStatus.PUT_CANDLE,
            HumanInfo.HumanFiniteStatus.PICK_UP_CANDY,
            HumanInfo.HumanFiniteStatus.USE_CANDY,
            HumanInfo.HumanFiniteStatus.DEATH,
            HumanInfo.HumanFiniteStatus.ACTION};
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID && entity.CanChangeStatus== true)
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

    /// <summary>
    /// 走る
    /// </summary>
    public class HumanStatusDash : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.DASH; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus> {
                HumanInfo.HumanFiniteStatus.WAITING,
                HumanInfo.HumanFiniteStatus.WALK,
                HumanInfo.HumanFiniteStatus.PICK_UP_CANDLE,
                HumanInfo.HumanFiniteStatus.PUT_CANDLE,
                HumanInfo.HumanFiniteStatus.PICK_UP_CANDY,
                HumanInfo.HumanFiniteStatus.USE_CANDY,
                HumanInfo.HumanFiniteStatus.DEATH,
                HumanInfo.HumanFiniteStatus.ACTION};
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage != StateID) return false;

            if (entity.CanChangeStatus != true) return false;

            if (entity.Stamina <= 0) return false;

            return true;


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

            entity.Stamina -= Time.deltaTime;

            if (entity.Stamina <= 0)
            {
                entity.Stamina = 0;

            }
        }
        public override void Exit()
        {

        }

    }

    /// <summary>
    /// ロウソクを拾う
    /// </summary>
    public class HumanStatusPickUpCandle : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.PICK_UP_CANDLE; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING,
                    HumanInfo.HumanFiniteStatus.DASH,
                    HumanInfo.HumanFiniteStatus.WALK,
                    HumanInfo.HumanFiniteStatus.DEATH
                };
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID )
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
            entity.HumanAnimation.SetInteger("Act", 2);
            entity.CanChangeStatus = false;
            entity.CandleStock += 1;

        }
        public override void Execute()
        {
            if (CAnimetionController.IsMotionEnd(entity.HumanAnimation, "Item"))
            {

                entity.CanChangeStatus = true;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
            }
        }
        public override void Exit()
        {
            entity.HumanAnimation.SetInteger("Act", 0);
        }

    }

    /// <summary>
    /// ロウソクを置く
    /// </summary>
    public class HumanStatusPutCandle : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {

        private GameObject candlePrefab;
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.PUT_CANDLE; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING,
                    HumanInfo.HumanFiniteStatus.DASH,
                    HumanInfo.HumanFiniteStatus.WALK,
                    HumanInfo.HumanFiniteStatus.DEATH
                };
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID &&
                entity.CandleStock > 0 )
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
       
            candlePrefab = (GameObject)Resources.Load("Prefab/Candle");
            //ロウソクを生成
            var candle = Instantiate(candlePrefab);
            candle.GetComponent<CandleMain>().SetStatus(CandleMain.CandleStatus.FIRE);
            candle.transform.SetX(entity.transform.position.x);
            candle.transform.SetZ(entity.transform.position.z);

            //アニメーション
            entity.HumanAnimation.SetInteger("Act", 2);
            entity.CanChangeStatus = false;
            entity.CandleStock -= 1;


        }
        public override void Execute()
        {
            if (CAnimetionController.IsMotionEnd(entity.HumanAnimation, "Item"))
            {
                entity.CanChangeStatus = true;
            }
        }
        public override void Exit()
        {
            entity.HumanAnimation.SetInteger("Act", 0);
        }

    }

    /// <summary>
    /// キャンディを拾う
    /// </summary>
    public class HumanStatusPickUpCandy : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {

        private GameObject candlePrefab;
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.PICK_UP_CANDY; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING,
                    HumanInfo.HumanFiniteStatus.DASH,
                    HumanInfo.HumanFiniteStatus.WALK,
                    HumanInfo.HumanFiniteStatus.DEATH
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
        public override void Enter()
        {

            entity.HumanAnimation.SetInteger("Act", 2);
            entity.CanChangeStatus = false;
            entity.CandyStock += 1;

        }
        public override void Execute()
        {
            if (CAnimetionController.IsMotionEnd(entity.HumanAnimation, "Item"))
            {
                entity.CanChangeStatus = true;
            }
        }
        public override void Exit()
        {
            entity.HumanAnimation.SetInteger("Act", 0);
        }

    }

    /// <summary>
    /// ロウソクを置く
    /// </summary>
    public class HumanStatusUseCandy : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {

        private GameObject candyPrefab;
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.USE_CANDY; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING,
                    HumanInfo.HumanFiniteStatus.DASH,
                    HumanInfo.HumanFiniteStatus.WALK,
                    HumanInfo.HumanFiniteStatus.DEATH
                };
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID &&
                entity.CandyStock > 0)
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
            //キャンディを生成
            candyPrefab = (GameObject)Resources.Load("Prefab/Candy");
            var candy = Instantiate(candyPrefab);
            candy.transform.position = entity.transform.position;
            candy.GetComponent<CandyMain>().Direction = entity.transform.forward;

            //アニメーション
            entity.HumanAnimation.SetInteger("Act", 4);
            entity.CanChangeStatus = false;
            entity.CandyStock -= 1;


        }
        public override void Execute()
        {
            if (CAnimetionController.IsMotionEnd(entity.HumanAnimation, "Use"))
            {
                entity.CanChangeStatus = true;
            }
        }
        public override void Exit()
        {
            entity.HumanAnimation.SetInteger("Act", 0);
        }

    }

    /// <summary>
    /// デス
    /// </summary>
    public class HumanStatusDeath : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {


        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.DEATH; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING
                };
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID &&
                entity.Hp <= 0)
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

            entity.HumanAnimation.SetInteger("State", 3);

        }
        public override void Exit()
        {

            entity.Hp = 1;

        }

    }

    /// <summary>
    /// アクション
    /// </summary>
    public class HumanStatusAction: FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        private ParticleSystem surprised;
        private GameObject actionPrefab;


        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.ACTION; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING,
                    HumanInfo.HumanFiniteStatus.DASH,
                    HumanInfo.HumanFiniteStatus.WALK
                };
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID )
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
            //最初にインスタンスするときのみ使用
            if (actionPrefab == null)
            {
                actionPrefab = (GameObject)Resources.Load("Prefab/ExclamationMark");
                var action = Instantiate(actionPrefab);
                action.transform.parent = entity.transform;
                action.transform.position = entity.transform.position;
                action.transform.AddY(1.0f);

                surprised = action.GetComponent<ParticleSystem>();
            }
            else {//二重再生を回避
                surprised.Play();
            }
            
            entity.HumanStateMachine.RevertToPreviousState();

        }
        public override void Execute()
        {

        }
        public override void Exit()
        {

        }

    }



    public class HumanStatusRescue : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {


        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.RESCUE; } }

        public override List<HumanMain.HumanFiniteStatus> NextStateIDs
        {
            get
            {
                return new List<HumanMain.HumanFiniteStatus>
                {
                    HumanInfo.HumanFiniteStatus.WAITING
                };
            }
        }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            if (entity.HumanStatusMessage == StateID )
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


        }
        public override void Exit()
        {


        }

    }
}
