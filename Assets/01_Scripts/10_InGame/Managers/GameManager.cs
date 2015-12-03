using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
  public static GameManager gm;
  public GameObject gameOverUI;
  private int status = 0;

  void Awake() {
    gm = this;
  }

  public bool IsOver() {
    return status > 0;
  }

  public void GameOver() {
    status++;
    Player.pl.GetComponent<Rigidbody>().isKinematic = true;
    gameOverUI.SetActive(true);
    TimeManager.tm.stopTime();
  }
}
