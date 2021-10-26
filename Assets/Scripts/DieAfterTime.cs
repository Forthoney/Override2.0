using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
  // Start is called before the first frame update

  public float lifeTime;
  float timer;
  void Start()
  {
    timer = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if (timer >= lifeTime)
    {
      Destroy(gameObject);
    }
    else
    {
      timer += Time.deltaTime;
    }
  }
}
