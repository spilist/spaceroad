using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
  public static GameManager gm;
  public GameObject gameOverUI;
  public Text gameOverMessage;
  private int status = 0;

  void Awake() {
    gm = this;
  }

  public bool IsOver() {
    return status > 0;
  }

  public void GameOver(string reason) {
    status++;
    Player.pl.GetComponent<Rigidbody>().isKinematic = true;
    gameOverUI.SetActive(true);
    gameOverMessage.text = reason;
    TimeManager.tm.stopTime();
  }
}
