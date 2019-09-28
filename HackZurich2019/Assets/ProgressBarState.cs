using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MagicLeap
{
    public class ProgressBarState : MonoBehaviour
    {
        #region Public Variables
        public double Co2State = 0;
        #endregion

        #region Event Handlers
        public void OnCO2StatusReceived(double newState)
        {
            Co2State = newState;
        }
        #endregion
    }
}
