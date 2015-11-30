using UnityEngine;
using System.Collections;

public class AsteroidManager : UniformObjectsManager {
  override public void initRest() {
    spawnPooledObjs(objPool, objPrefab, objAmount);
  }

  override public void run(bool val) {}
}
