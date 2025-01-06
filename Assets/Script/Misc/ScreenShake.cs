using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine ;

public class ScreenShakeManager : MonoBehaviour{
    private CinemachineImpulseSource impulseSource;


    private void Awake() {
        impulseSource = GetComponent<CinemachineImpulseSource>();

        if (impulseSource == null){
            Debug.LogError("No CinemachineImpulseSource found on this object");
        }
    }

    public void ShakeScreen(){
        impulseSource.GenerateImpulse();
    }
}
