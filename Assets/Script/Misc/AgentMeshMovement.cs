using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMeshMovement : MonoBehaviour
{
    private Vector3 target;

    NavMeshAgent agent;


    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update() {
        SetTargetPosition();
        SetAgentPosition();
    }

    private void SetTargetPosition() {
        if(Input.GetMouseButtonDown(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // RaycastHit hit;
            // if(Physics.Raycast(ray, out hit)) {
            //     target = hit.point;
            //     agent.SetDestination(target);
            // }
        }
    }

    private void SetAgentPosition() {
        agent.SetDestination(new Vector3(target.x,target.x, transform.position.z));
    }
}
