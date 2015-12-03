using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
  public Collider boundary;
  public GameObject hideAfterStart;
  public GameObject inGameUI;

  private bool gameStarted = false;
  private bool react = true;
  private bool dragging = false;
  private Vector3 direction;
  private float lastMousePosition_y;
  private float endMousePosition_y;
  private float lastMousePosition_x;
  private float endMousePosition_x;
  private Vector3 lastDraggablePosition;

  void Update() {
    // if (Application.platform == RuntimePlatform.Android) {
    //   if (Input.GetKeyDown(KeyCode.Escape)) {
    //     if (gameStarted) {
    //       pause.activateSelf();
    //     } else if (menus.isMenuOn()) {
    //       menus.toggleMenuAndUI();
    //     } else {
    //       Application.Quit();
    //     }
    //     return;
    //   }
    // }

    #if UNITY_EDITOR
    if (Input.GetAxis("Horizontal") != 0) {
      startGame();
      float horiz = Input.GetAxis("Horizontal");
      string dirStr = horiz < 0 ? "Left" : "Right";
      Player.pl.setPerpDirection(dirStr);
    } else {
      Player.pl.tiltBack();
    }
    #endif

    // if (Input.GetMouseButtonDown(0)) {
    //   Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //   RaycastHit hit;
    //   if ( Physics.Raycast( ray, out hit, 100 ) ) {
    //     string hitTag = hit.transform.tag;
    //     Debug.Log(hitTag);
    //   }
    // }

    // if (reactAble() && Input.GetMouseButtonDown(0)) {
    //   if (pause.isResuming()) return;

    //   string result = menus.touched();
    //   if (menus.isMenuOn()) return;

    //   if (result == "nothing" && pause.isPaused()) {
    //     pause.resume();
    //     return;
    //   }

    //   if (Player.pl.uncontrollable()) return;

    //   if ((result == "nothing" || result == "ChangeBehavior") && !gameStarted) {
    //     TimeManager.time.startTime();
    //     EnergyManager.em.turnEnergy(true);
    //     SuperheatManager.sm.startGame();
    //     beforeIdle.moveTitle();
    //     menus.gameStart();
    //     spawnManager.run();
    //     SkillManager.sm.startGame();
    //     AudioManager.am.changeVolume("Main", "Max");

    //     DataManager.dm.increment("play_" + PlayerPrefs.GetString("SelectedCharacter"));

    //     boundary.enabled = true;
    //     gameStarted = true;
    //     return;
    //   }
    // }

    for (var i = 0; i < Input.touchCount; ++i) {
      Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
      RaycastHit hit;
      if ( Physics.Raycast(ray, out hit) ) {
        GameObject hitObject = hit.transform.gameObject;

        if (hitObject.tag != "MoveArea") return;
        else startGame();

        if (Input.GetTouch(i).phase == TouchPhase.Began) {
          hitObject.SendMessage("OnPointerDown");
        }

        if (Input.GetTouch(i).phase == TouchPhase.Ended) {
          hitObject.SendMessage("OnPointerUp");
        }
      }
    }
  }

  void startGame() {
    if (GameManager.gm.IsOver()) {
      Application.LoadLevelAsync(Application.loadedLevel);
    }

    if (gameStarted) return;
    Player.pl.gameStart();
    gameStarted = true;
    hideAfterStart.SetActive(false);
    inGameUI.SetActive(true);
    SpawnManager.sm.run();
    TimeManager.tm.startTime();
  }

  // void OnMouseDown() {
  //   if (menus.isMenuOn() && menus.draggableDirection() != "") {
  //     lastMousePosition_x = Input.mousePosition.x;
  //     lastMousePosition_y = Input.mousePosition.y;
  //     lastDraggablePosition = Camera.main.WorldToScreenPoint(menus.draggable().transform.position);
  //     dragging = true;
  //   }
  // }

  // void OnMouseDrag() {
  //   if (menus.isMenuOn() && menus.draggableDirection() != "") {
  //     Vector3 movement = Vector3.zero;
  //     if (menus.draggableDirection() == "LeftRight") {
  //       float positionX = menus.draggable().transform.localPosition.x;
  //       if (positionX == menus.dragEnd("left") || positionX == menus.dragEnd("right")) {
  //         endMousePosition_x = Input.mousePosition.x;
  //       }
  //       if (positionX > menus.dragEnd("left") || positionX < menus.dragEnd("right")) {
  //         movement = new Vector3((Input.mousePosition.x - endMousePosition_x)/5f + (endMousePosition_x - lastMousePosition_x), 0, 0);
  //       } else {
  //         movement = new Vector3(Input.mousePosition.x - lastMousePosition_x, 0, 0);
  //       }
  //     } else {
  //       float positionY = menus.draggable().transform.localPosition.y;
  //       if (positionY == menus.dragEnd("top") || positionY == menus.dragEnd("bottom")) {
  //         endMousePosition_y = Input.mousePosition.y;
  //       }
  //       if (positionY > menus.dragEnd("top") || positionY < menus.dragEnd("bottom")) {
  //         movement = new Vector3(0, (Input.mousePosition.y - endMousePosition_y)/5f + (endMousePosition_y - lastMousePosition_y), 0);
  //       } else {
  //         movement = new Vector3(0, Input.mousePosition.y - lastMousePosition_y, 0);
  //       }
  //     }

  //     menus.draggable().transform.position = Camera.main.ScreenToWorldPoint(lastDraggablePosition + movement);
  //   }
  // }

  // void OnMouseUp() {
  //   if (menus.isMenuOn() && dragging) {
  //     dragging = false;

  //     if (menus.draggableDirection() == "LeftRight") {
  //       float positionX = menus.draggable().transform.localPosition.x;
  //       if (positionX > menus.dragEnd("left")) {
  //         menus.returnToEnd("left");
  //       } else if (positionX < menus.dragEnd("right")) {
  //         menus.returnToEnd("right");
  //       }
  //     } else {
  //       float positionY = menus.draggable().transform.localPosition.y;
  //       if (positionY > menus.dragEnd("top")) {
  //         menus.returnToEnd("top");
  //       } else if (positionY < menus.dragEnd("bottom")) {
  //         menus.returnToEnd("bottom");
  //       }
  //     }
  //   }

  // }

  public void stopReact() {
    react = false;
  }

  // bool reactAble() {
  //   return !beforeIdle.isLoading() && react;
  // }
}

