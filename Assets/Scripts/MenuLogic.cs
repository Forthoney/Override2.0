using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogic : MonoBehaviour
{

  public GameObject healthBar;

  public bool debugMode = false;

  Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    if (debugMode)
    {
      GameManager.PlayerShip = GameObject.FindWithTag("Player");
      animator.SetTrigger("Started");
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void onClick()
  {
    GameManager.PlayerShip = GameObject.FindWithTag("Player");
    animator.SetTrigger("Started");
  }



}
