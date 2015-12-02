using UnityEngine;
using System.Collections;

public class PlayerAngle : MonoBehaviour {
  public int followingSpeed = 360;
  public float maxTiltAmount = 20;
  public float tiltDuration = 0.3f;
  public float tiltBackDuration = 0.1f;
  private float tiltAmount;
  private float currentTilt;
  private bool tilting = false;
  private bool tiltingBack = false;

  public float currentAngle = 0;
  bool dirChanging = false;
  float targetAngle;

  void Start () {
    currentAngle = ContAngle(Vector3.forward, Player.pl.getDirection());
    transform.localEulerAngles = new Vector3(0, currentAngle, 0);
    tiltAmount = 0;
    currentTilt = 0;
  }

  void Update () {
    // if (dirChanging) {
    //   currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, Time.deltaTime * followingSpeed);

    //   transform.localEulerAngles = new Vector3(0, currentAngle, 0 + currentTilt);
    //   if (Mathf.Abs(currentAngle - targetAngle) < 1f) dirChanging = false;
    // }

    if (tilting) {
      currentTilt = Mathf.MoveTowards(currentTilt, tiltAmount, Time.deltaTime * maxTiltAmount / tiltDuration);
      transform.localEulerAngles = new Vector3(0, currentAngle, 0 + currentTilt);

      if (currentTilt == tiltAmount) {
        tilting = false;
      }
    }

    if (tiltingBack) {
      currentTilt = Mathf.MoveTowards(currentTilt, 0, Time.deltaTime * maxTiltAmount / tiltBackDuration);
      transform.localEulerAngles = new Vector3(0, currentAngle, 0 + currentTilt);

      if (currentTilt == 0) {
        tiltingBack = false;
      }
    }
  }

  public void tilt(int sign) {
    tiltAmount = sign * maxTiltAmount;
    tilting = true;
    tiltingBack = false;
  }

  public void tiltBack() {
    if (tiltingBack) return;
    tilting = false;
    tiltingBack = true;
  }

  public void setDirection(Vector3 dir) {
    dirChanging = true;
    targetAngle = ContAngle(Vector3.forward, dir);
    currentAngle = transform.localEulerAngles.y;
    if (currentAngle > 180) currentAngle -= 360.0f;

    if ((targetAngle - currentAngle) > 180) targetAngle -= 360;
    else if ((currentAngle - targetAngle) > 180) currentAngle -= 360;
  }

  float ContAngle(Vector3 fwd, Vector3 targetDir) {
    float angle = Vector3.Angle(fwd, targetDir);

    if (AngleDir(fwd, targetDir, Vector3.up) == -1) {
        angle = 360.0f - angle;
        if( angle > 359.9999f ) angle -= 360.0f;
    }

    if (angle > 180) angle -= 360.0f;
    else if (angle < -180) angle += 360.0f;

    return angle;
  }

  int AngleDir( Vector3 fwd, Vector3 targetDir, Vector3 up) {
    Vector3 perp = Vector3.Cross(fwd, targetDir);
    float dir = Vector3.Dot(perp, up);

    if (dir > 0.0) return 1;
    else if (dir < 0.0) return -1;
    else return 0;
  }
}
