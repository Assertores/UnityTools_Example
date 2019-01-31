using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public interface IWaightedAIDesition {

        /// <summary>
        /// evaluates the importance of the Behavior
        /// </summary>
        /// <returns>the importance as a float value</returns>
        float Evaluate();

        /// <summary>
        /// Executes one tick of the AI Behavior
        /// </summary>
        void Execute();
    }
}