﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class SpawnManager : MonoBehaviour {
  public static SpawnManager sm;
  public float generateSpaceRadius = 0.9f;
  public float generateOffset = 0.2f;

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
    float screenX, screenY;
    Vector3 spawnPosition;
    int count = 0;

    LayerMask mask = (int) Mathf.Pow(2, target.gameObject.layer);
    float radius = target.GetComponent<ObjectMover>().boundingSize;

    float offset_ = generateOffset;

    do {
      do {
        screenX = Random.Range(-generateSpaceRadius, 1 + generateSpaceRadius);
        screenY = Random.Range(-generateSpaceRadius, 1 + generateSpaceRadius);
      } while(-offset_ < screenX && screenX < offset_ + 1 && -offset_ < screenY && screenY < offset_ + 1);

      spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(screenX, screenY, Camera.main.transform.position.y));
    } while(Physics.OverlapSphere(spawnPosition, radius, mask).Length > 0 && count++ < 100);

    if (count >= 100) Debug.Log(target.name + " is overlapped");

    return spawnPosition;
  }
}