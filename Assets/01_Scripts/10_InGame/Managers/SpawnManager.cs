using UnityEngine;
using System.Collections;
using System.Linq;

public class SpawnManager : MonoBehaviour {
  public static SpawnManager sm;
  public int minSpawnRadius = 200;
  public int maxSpawnRadius = 400;

  void Awake() {
    sm = this;
  }

  public void run() {
    GetComponent<AsteroidManager>().enabled = true;

    // GetComponent<NormalPartsManager>().enabled = true;

    // GetComponent<FollowTarget>().enabled = false;

    // GetComponent<SmallAsteroidManager>().enabled = true;

    // GetComponent<GoldenCubeManager>().enabled = true;

    // GetComponent<ComboPartsManager>().enabled = true;
    // GetComponent<ComboPartsManager>().adjustForLevel(1);
  }

  // public void runManager(string objName) {
  //   (GetComponent(objName + "Manager") as MonoBehaviour).enabled = true;
  //   ObjectsManager obm = (ObjectsManager)GetComponent(objName + "Manager");
  //   obm.run();
  // }

  // public void runManagerAt(string objName, Vector3 pos) {
  //   ObjectsManager obm = (ObjectsManager)GetComponent(objName + "Manager");
  //   obm.runByTransform(pos);
  // }

  public Vector3 getSpawnPosition(GameObject target) {
    float posX, posY;
    Vector3 spawnPosition;
    int count = 0;

    LayerMask mask = (int) Mathf.Pow(2, target.gameObject.layer);
    float radius = target.GetComponent<ObjectMover>().boundingSize;

    Vector2 screenPos = Random.insideUnitCircle;
    screenPos.Normalize();

    do {
      posX = Random.Range(minSpawnRadius, maxSpawnRadius);
      posY = Random.Range(minSpawnRadius, maxSpawnRadius);
      spawnPosition = new Vector3(screenPos.x * posX + Player.pl.transform.position.x, Player.pl.transform.position.y, screenPos.y * posY + Player.pl.transform.position.z);
    } while(Physics.OverlapSphere(spawnPosition, radius, mask).Length > 0 && count++ < 100);

    if (count >= 100) Debug.Log(target.name + " is overlapped");

    return spawnPosition;
  }
}
