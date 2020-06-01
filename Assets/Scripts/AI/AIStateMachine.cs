using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.AI
{
    public abstract class AIStateMachine : MonoBehaviour
    {

        protected AIState AIState;

        public void SetAIState(AIState aIState)
        {
            if (LevelManager.Instance.GetState() == State.End)
            {
                StopAllCoroutines();
                return;
            }
            AIState = aIState;
            StartCoroutine(AIState.Start());
        }

        private void Update()
        {
            if (LevelManager.Instance.GetState() == State.End)
            {
                StopAllCoroutines();
            }
        }
    }
}
