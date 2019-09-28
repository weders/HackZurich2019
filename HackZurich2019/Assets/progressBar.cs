using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBar : MonoBehaviour
{
    // Start is called before the first frame update
    double Co2State = 0;

    void OnCO2StatusReceived(double newState)
    {
        Co2State = newState;
    }    
}
