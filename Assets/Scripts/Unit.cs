using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
  [SerializeField] public NavMeshAgent agent;
  [SerializeField] private SpriteRenderer indicator;
  [SerializeField] private Animator unitAnimator;


  private void Update()
  {
    if (agent.velocity == new Vector3(0,0,0))
    {
      unitAnimator.SetBool("isWalking", false);
    }
    else
    {
      unitAnimator.SetBool("isWalking", true);
    }
  }
  public void SetDestinationPoint(Vector3 destPos)
  {
    agent.SetDestination(destPos);
    unitAnimator.SetBool("isWalking", true);
    
  }

  public void OnSelected()
  {
    indicator.enabled = true;
  }
  public void OnDropped()
  {
    indicator.enabled = false;
  }

 
}
