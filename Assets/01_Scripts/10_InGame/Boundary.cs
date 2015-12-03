using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {
  void OnTriggerExit(Collider other) {
    ObjectMover mover = other.GetComponent<ObjectMover>();
    if (mover != null) mover.destroyObject(false);
  }
}
