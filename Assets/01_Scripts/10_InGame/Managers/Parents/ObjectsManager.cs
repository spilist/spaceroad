using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsManager : MonoBehaviour {
  public int objAmount;
  public List<GameObject> objPool;
  public List<GameObject> objDestroyEffectPool;
  public List<GameObject> objEncounterEffectPool;
  public GameObject objPrefab;
  public GameObject objDestroyEffect;
  public GameObject objEncounterEffect;
  public ParticleSystem objEncounterEffectForPlayer;
  public float minSpawnInterval;
  public float maxSpawnInterval;

  public float mass;
  public float energy;
  public bool isFixedObject;

  protected bool skipInterval = false;

  public GameObject instance;

  virtual protected void beforeInit() {}

  void OnEnable() {
    beforeInit();

    objPool = new List<GameObject>();
    for (int i = 0; i < objAmount; ++i) {
      GameObject obj = (GameObject) Instantiate(objPrefab);
      obj.SetActive(false);
      obj.transform.parent = transform;
      objPool.Add(obj);
    }

    if (objDestroyEffect != null) {
      objDestroyEffectPool = new List<GameObject>();
      for (int i = 0; i < objAmount; ++i) {
        GameObject obj = (GameObject) Instantiate(objDestroyEffect);
        obj.SetActive(false);
        objDestroyEffectPool.Add(obj);
      }
    }

    if (objEncounterEffect != null) {
      objEncounterEffectPool = new List<GameObject>();
      for (int i = 0; i < objAmount; ++i) {
        GameObject obj = (GameObject) Instantiate(objEncounterEffect);
        obj.SetActive(false);
        objEncounterEffectPool.Add(obj);
      }
    }

    initRest();
  }

  public void spawnPooledObjs(List<GameObject> list, GameObject prefab, int count) {
    for (int i = 0; i < count; i++) {
      GameObject obj = getPooledObj(list, prefab, SpawnManager.sm.getSpawnPosition(prefab));
      obj.SetActive(true);
    }
  }

  public GameObject getPooledObj(List<GameObject> list, GameObject prefab, Vector3 pos) {
    GameObject obj = getPooledObj(list, prefab);
    obj.transform.position = pos;
    return obj;
  }

  public GameObject getPooledObj(List<GameObject> list, GameObject prefab) {
    for (int i = 0; i < list.Count; i++) {
      if (!list[i].activeInHierarchy) {
        return list[i];
      }
    }

    GameObject obj = (GameObject) Instantiate(prefab);
    obj.transform.parent = transform;
    list.Add(obj);
    return obj;
  }

  virtual public void initRest() {}

  virtual public void run(bool val) {
    skipInterval = val;
    StartCoroutine(respawnRoutine());
  }

  protected IEnumerator respawnRoutine() {
    // if (ScoreManager.sm.isGameOver()) yield break;

    yield return new WaitForSeconds(spawnInterval());

    skipInterval = false;

    spawn();
    afterSpawn();
  }

  virtual public void respawn() {
    // if (ScoreManager.sm.isGameOver()) return;

    int count = objAmount - GameObject.FindGameObjectsWithTag(objPrefab.tag).Length;
    if (count > 0) {
      spawnPooledObjs(objPool, objPrefab, count);
    }
  }

  virtual protected float spawnInterval() {
    if (skipInterval) return 0;
    else return Random.Range(minSpawnInterval, maxSpawnInterval);
  }

  virtual protected void spawn() {
    // if (ScoreManager.sm.isGameOver()) return;

    instance = getPooledObj(objPool, objPrefab, SpawnManager.sm.getSpawnPosition(objPrefab));
    instance.SetActive(true);
  }

  virtual protected void afterSpawn() {}

  public void skipRespawnInterval() {
    skipInterval = true;
  }
}
