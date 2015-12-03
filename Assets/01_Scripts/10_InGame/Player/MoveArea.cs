using UnityEngine;
using System.Collections;

public class MoveArea : MonoBehaviour {
  public string movingDirection;
  private bool moving;

  void Start () {

	}

  void Update() {
    // if (Player.pl.uncontrollable()) return;

    if (moving && Input.touchCount == 1) {
      Player.pl.setPerpDirection(movingDirection);
    }
  }

  void OnPointerDown() {
    // if (Player.pl.uncontrollable()) return;

    if (Input.touchCount == 1) {
      moving = true;
    }
  }

  void OnPointerUp() {
    moving = false;
    Player.pl.tiltBack();
  }
}
