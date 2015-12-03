using UnityEngine;
using System.Collections;

public class UniformObjectMover : ObjectMover {
  protected float speed;
  protected float tumble;
  protected Vector3 direction;
  protected UniformObjectsManager manager;
  protected bool isPositive;

  override protected void Awake() {
    base.Awake();
    manager = (UniformObjectsManager) _manager.GetComponent(getManager());
    speed = getSpeed();
    tumble = getTumble();
    isPositive = manager.isPositive;
  }

  override protected void OnEnable() {
    base.OnEnable();

    direction = getDirection();
    rb.angularVelocity = Random.onUnitSphere * tumble;
    rb.velocity = direction * speed;
  }

  virtual protected float getSpeed() {
    return manager.speed;
  }

  virtual protected float getTumble() {
    return manager.tumble;
  }

  virtual protected Vector3 getDirection() {
    Vector2 randomV = Random.insideUnitCircle;
    return new Vector3(randomV.x, 0, randomV.y).normalized;
  }

  virtual protected void FixedUpdate() {}

  virtual protected void OnTriggerEnter(Collider other) {
    if (isPositive && other.tag != "Boundary") destroyObject();
  }
}
