using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {
  public static TimeManager tm;
  public int now = 0;
  public Text timeText;

  void Awake() {
    tm = this;
  }

  public void startTime() {
    // resetProgress(false);
    // CubeManager.cm.startCount();
    StartCoroutine("startElapse");
  }

  // public void resetProgress(bool nextPhase = true) {
  //   if (nextPhase) {
  //     PhaseManager.pm.nextPhase();
  //   }
  //   progressChanging = true;

  //   addGuageAmount = 0;
  //   currentProgressCount = 0;

  //   requiredProgress = PhaseManager.pm.reqProgressPerLevel[PhaseManager.pm.phase()];

  //   GameObject obj = (GameObject) Instantiate(phaseStarPrefab);
  //   obj.transform.SetParent(phaseStars, false);
  //   obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(PhaseManager.pm.phase() * distanceBetweenStars, 0);
  //   phaseStar = obj.GetComponent<Image>();
  //   popStatus = 1;
  //   starScale = startScale;
  // }

  // Vector3 screenToWorld(Vector3 screenPos) {
  //   return new Vector3(screenPos.x + Player.pl.transform.position.x, Player.pl.transform.position.y, screenPos.y + Player.pl.transform.position.z);
  // }

  // public void addProgressByCube(int cubes) {
  //   addProgress(cubes * progressPerCube);
  // }

  // void addProgress(float val) {
  //   addGuageAmount += val;
  // }

  // void Update() {
  //   if (progressChanging) {
  //     if (currentProgressCount <= requiredProgress) {
  //       currentProgressCount = Mathf.MoveTowards(currentProgressCount, requiredProgress, Time.deltaTime * progressPerSecond);
  //       currentProgressCount = Mathf.MoveTowards(currentProgressCount, currentProgressCount + addGuageAmount, Time.deltaTime * requiredProgress / progressChangeSpeed);
  //       addGuageAmount = Mathf.MoveTowards(addGuageAmount, 0, Time.deltaTime * requiredProgress / progressChangeSpeed);
  //     }

  //     if (currentProgressCount >= requiredProgress) {
  //       resetProgress();
  //     }
  //   }

  //   if (popStatus > 0) {
  //     if (popStatus == 1) {
  //       changeScale(largeScale, largeScale - startScale);
  //     } else if (popStatus == 2) {
  //       changeScale(smallScale, smallScale - largeScale);
  //     } else if (popStatus == 3) {
  //       changeScale(1, 1 - smallScale);
  //     } else if (popStatus == 4) {
  //       popStatus = 0;
  //     }

  //     phaseStar.transform.localScale = starScale * Vector3.one;
  //   }
  // }

  IEnumerator startElapse() {
    while(true) {
      yield return new WaitForSeconds(1);
      now++;
      timeText.text = now.ToString();
      // CubeManager.cm.addPointsByTime();

      // asm.respawn();
      // sam.respawn();
      // npm.respawn();
      // if (spawnDangerousEMP) dem.respawn();
      // if (spawnBlackhole) blm.respawn();
    }
  }

  public void stopTime() {
    StopCoroutine("startElapse");
    // mtm.stopSpawn();
    // idm.stopSpawn();
  }

  // public void startSpawnDangerousEMP() {
  //   spawnDangerousEMP = true;
  // }

  // public void startBlackhole() {
  //   spawnBlackhole = true;
  // }

  // void changeScale(float targetScale, float difference) {
  //   starScale = Mathf.MoveTowards(starScale, targetScale, Time.deltaTime * Mathf.Abs(difference) / changeTime);
  //   if (starScale == targetScale) popStatus++;
  // }
}
