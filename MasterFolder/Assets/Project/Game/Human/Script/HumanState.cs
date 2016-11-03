using UnityEngine;
using System.Collections;

public class HumanState : MonoBehaviour {



    public delegate void HumanStatusCallback(FSMState<HumanMain, HumanMain.HumanFiniteStatus> myStatus);

   // private FiniteStateMachine<HumanMain, HumanMain.HumanFiniteStatus> humanStateMachine;

 
    public class HumanStatusWaiting : FSMState<HumanMain, HumanMain.HumanFiniteStatus>
    {
        public override HumanMain.HumanFiniteStatus StateID { get { return HumanMain.HumanFiniteStatus.WAITING; } }

        public override bool CanEnter(FSMState<HumanMain, HumanMain.HumanFiniteStatus> currentState)
        {
            
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


}
