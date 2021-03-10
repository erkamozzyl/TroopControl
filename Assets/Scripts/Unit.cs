using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
  [SerializeField]
  private NavMeshAgent agent;
  [SerializeField] private SpriteRenderer indicator;
  
  public void SetDestinationPoint(Vector3 destPos)
  {
    
    // ses calındı 
    agent.SetDestination(destPos);
  }

  public void OnSelected()
  {
    indicator.enabled = true;
    // outlıne acıldı
  }

  public void OnDropped()
  {
    // unıt sıl
    // outlıne kapandı
    indicator.enabled = false;
    

  }
}
