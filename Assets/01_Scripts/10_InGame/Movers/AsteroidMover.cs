using UnityEngine;
using System.Collections;

public class AsteroidMover : UniformObjectMover {
  override public string getManager() {
    return "AsteroidManager";
  }
}
