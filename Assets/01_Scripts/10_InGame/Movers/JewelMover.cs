using UnityEngine;
using System.Collections;

public class JewelMover : UniformObjectMover {
  override public string getManager() {
    return "JewelManager";
  }
}
