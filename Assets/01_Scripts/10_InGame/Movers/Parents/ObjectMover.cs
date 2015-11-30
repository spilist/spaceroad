using UnityEngine;
using System.Collections;
using System.Linq;

public class ObjectMover : MonoBehaviour {
  public float boundingSize = 50;
  protected float originalScale;
  protected Rigidbody rb;
  protected ObjectsManager _manager;

  virtual protected void Awake() {
    originalScale = transform.localScale.x;
    rb = GetComponent<Rigidbody>();
    _manager = (ObjectsManager) SpawnManager.sm.GetComponent(getManager());
    rb.mass = _manager.mass;
  }

  virtual public string getManager() {
    return "";
  }

  virtual protected void OnEnable() {
    transform.localScale = originalScale * Vector3.one;
  }

  virtual protected void OnCollisionEnter(Collision collision) {
    string tag = collision.collider.tag;

    if (tag == "ContactCollider") {
      Player.pl.processCollision(this, collision);
    }

    if (_manager.isFixedObject) return;

    // ContactPoint contact = collision.contacts[0];
    // Vector3 normal = contact.normal;
    // direction = Vector3.Reflect(direction, -normal).normalized;
    // direction.y = 0;
    // direction.Normalize();
  }

  virtual public void destroyObject(bool destroyEffect = true, bool byPlayer = false, bool respawn = true) {

    gameObject.SetActive(false);

    if (destroyEffect) showDestroyEffect(byPlayer);
    if (respawn) respawnObject(byPlayer);
  }

  void showDestroyEffect(bool byPlayer) {
    if (_manager.objDestroyEffect == null) return;

    _manager.getPooledObj(_manager.objDestroyEffectPool, _manager.objDestroyEffect, transform.position).SetActive(true);
  }

  virtual public void respawnObject(bool byPlayer) {
    _manager.run(!byPlayer);
  }

  virtual public void encounterPlayer(bool destroy) {
    showEncounterEffect();
    if (destroy) destroyObject(true, true, true);
  }

  void showEncounterEffect() {
    if (_manager.objEncounterEffectForPlayer != null) {
      _manager.objEncounterEffectForPlayer.Play();
      if (_manager.objEncounterEffectForPlayer.GetComponent<AudioSource>() != null) {
        _manager.objEncounterEffectForPlayer.GetComponent<AudioSource>().Play();
      }
    }

    if (_manager.objEncounterEffect != null) {
      _manager.getPooledObj(_manager.objEncounterEffectPool, _manager.objEncounterEffect, transform.position).SetActive(true);
    }
  }

  // virtual public bool dangerous() {
  //   return false;
  // }
}
