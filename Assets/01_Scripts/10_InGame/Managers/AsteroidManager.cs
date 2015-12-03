using UnityEngine;
using System.Collections;

public class AsteroidManager : UniformObjectsManager {
  override protected void BeforeInit() {
    objPrefab.GetComponent<ObjectMover>().boundingSize = objPrefab.GetComponent<Renderer>().bounds.extents.magnitude;
  }
  // override public void run(bool val) {}
}
