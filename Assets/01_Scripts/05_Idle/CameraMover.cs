using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
  public Vector3 crossyPos;
  public bool slowly = false;

  private Vector3 velocity = Vector3.zero;
  public float smoothTime = 0.1f;
  public float speed = 20f;

  private bool shaking = false;
  public float shakeDuring = 0.5f;
  private float shakeCount = 0;
  public float shakeAmount = 0.7f;
  private bool shakeContinuously = false;
  private Vector3 originalPos;

  private Vector3 pastTargetPosition, pastFollowerPosition;
  private Vector3 pastPosition;
  private bool paused = false;

  void Start () {
  }

  void Update() {
    if (shaking && !paused) {
      if (slowly) {
        originalPos = Vector3.SmoothDamp(originalPos, new Vector3 (Player.pl.transform.position.x, transform.position.y, Player.pl.transform.position.z), ref velocity, smoothTime, Mathf.Infinity, Time.smoothDeltaTime);
      } else {
        originalPos = newPos();
      }

      transform.position = new Vector3(originalPos.x + Random.insideUnitSphere.x * shakeAmount, originalPos.y, originalPos.z + Random.insideUnitSphere.z * shakeAmount);
      shakeCount -= Time.deltaTime;

      if (!shakeContinuously && shakeCount < 0) stopShake();
    } else if (slowly) {
      transform.position = Vector3.SmoothDamp(transform.position, newPos(), ref velocity, smoothTime, Mathf.Infinity, Time.smoothDeltaTime);
    } else {
        transform.position = newPos();
    }
  }

  Vector3 newPos() {
    return new Vector3 (crossyPos.x + Player.pl.transform.position.x, transform.position.y, Player.pl.transform.position.z + crossyPos.z);
  }

  public void setPaused(bool val) {
    paused = val;
  }

  public void setSlowly(bool val) {
    slowly = val;
  }

  public void shake(float duration = 0, float amount = 4) {
    if (duration == 0) {
      shakeCount = shakeDuring;
    } else {
      shakeCount = duration;
    }

    shakeAmount = amount;
    shaking = true;
    originalPos = transform.position;
  }

  public void shakeUntilStop(float amount = 4) {
    shaking = true;
    shakeContinuously = true;
    shakeAmount = amount;
    originalPos = transform.position;
  }

  public void stopShake() {
    shaking = false;
    shakeContinuously = false;
    transform.position = originalPos;
  }
}
