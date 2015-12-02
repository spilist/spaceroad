using UnityEngine;
using System.Collections;

public class PoliceMover : LRObjectMover {
  protected float angleGoStraight;
  protected PoliceManager pm;

  override protected void Awake() {
    base.Awake();
    pm = (PoliceManager)manager.GetComponent(getManager());
    angleGoStraight = pm.angleGoStraight;
  }

  override protected void SetInputByAI() {
    // float targetAngle = ContAngle(transform.forward, Player.pl.transform.position - transform.position);
    // float currentAngle = transform.eulerAngles.y;
    // if (currentAngle > 180) currentAngle -= 360.0f;

    // if ((targetAngle - currentAngle) > 180) targetAngle -= 360;
    // else if ((currentAngle - targetAngle) > 180) currentAngle -= 360;

    // float angle = targetAngle - currentAngle;

    Vector3 targetRotVector = Quaternion.LookRotation(Player.pl.transform.position - transform.position) * Vector3.forward;
    Vector3 curRotVector = transform.rotation * Vector3.forward;
    float targetAngle = Mathf.Atan2(targetRotVector.x, targetRotVector.z) * Mathf.Rad2Deg;
    float currentAngle = Mathf.Atan2(curRotVector.x, curRotVector.z) * Mathf.Rad2Deg;
    float angle = Mathf.DeltaAngle(targetAngle, currentAngle);

    // Debug.Log(targetAngle + " " + currentAngle + " " + angle);

    // float angle = Quaternion.Angle(transform.rotation, targetRot);
    // Debug.Log(angle);
    if (Mathf.Abs(angle) < angleGoStraight) turnInput = 0;
    // if (Mathf.Abs(Mathf.Abs(angle) - 180) < angleGoStraight) turnInput = 0;
    else turnInput = angle > 0 ? -1 : 1;
    // Debug.Log(turnInput);
  }
}
