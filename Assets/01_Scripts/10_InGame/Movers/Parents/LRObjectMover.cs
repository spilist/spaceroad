using UnityEngine;
using System.Collections;

public class LRObjectMover : ObjectMover {
  protected float acceleration;
  protected float deceleration;
  protected float baseSpeed;
  protected float maxSpeed;
  protected float turnSpeed;
  protected LRObjectsManager manager;

  protected bool decelerating;
  protected int turnInput;

	override protected void Awake() {
    base.Awake();
    manager = (LRObjectsManager) _manager.GetComponent(getManager());
    acceleration = manager.acceleration;
    deceleration = manager.deceleration;
    baseSpeed = manager.baseSpeed;
    maxSpeed = manager.maxSpeed;
    turnSpeed = manager.turnSpeed;
  }

  override protected void OnEnable() {
    base.OnEnable();
    decelerating = false;
    turnInput = 0;
  }

  virtual protected void FixedUpdate() {
    Move();
    SetInputByAI();
    Turn();
  }

  virtual protected void Move () {
    if (rb.velocity.magnitude < maxSpeed) {
      if (decelerating) {
        if (rb.velocity.magnitude > Time.fixedDeltaTime * deceleration)
          rb.velocity = rb.velocity - transform.forward * Time.fixedDeltaTime * deceleration;
      } else {
        rb.velocity = rb.velocity + transform.forward * Time.fixedDeltaTime * acceleration;
      }
    }
    // Debug.Log(rb.velocity.magnitude);
  }

  virtual protected void SetInputByAI() {}

  virtual protected void Turn () {
    if (turnInput != 0) {
      decelerating = true;
      Quaternion deltaAngle =
      Quaternion.AngleAxis (Mathf.Rad2Deg * turnInput * turnSpeed * Time.fixedDeltaTime, transform.up);
      transform.Rotate (deltaAngle.eulerAngles);

      Vector3 v = rb.velocity;
      v = deltaAngle * v;
      rb.velocity = v;
    } else {
      decelerating = false;
    }
  }
}
