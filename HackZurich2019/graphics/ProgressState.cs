using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressState : MonoBehaviour
{
    public ProgressBar Pb;

    // Start is called before the first frame update
    void Start()
    {
        Pb.BarValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCO2CostReceived()

}
