using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WastedArea : MonoBehaviour {
  public int radius = 100;
  public float wasteSpeed = 50;
  public float wasteIn = 3;
  public Text wasteCountText;
  private float wasteCount;
  private int policeCount;

	void Start () {
    transform.localScale = radius * Vector3.one;
	}

	void OnTriggerEnter(Collider other) {
    if (other.tag == "Police") {
      policeCount++;
    }
  }

  void OnTriggerStay(Collider other) {
    if (other.tag == "Police" && Player.pl.getSpeed() < wasteSpeed) {
      SetWasteCount(Time.deltaTime);
    }
  }

  void OnTriggerExit(Collider other) {
    if (other.tag == "Police") policeCount--;
    if (policeCount <= 0) ResetWasteCount();
  }

  public void ResetWasteCount() {
    if (Player.pl.getSpeed() >= wasteSpeed) SetWasteCount(0);
  }

  void SetWasteCount(float val) {
    if (GameManager.gm.IsOver()) return;

    if (val == 0) wasteCount = 0;
    else wasteCount += val;

    if (wasteCount >= wasteIn) {
      GameManager.gm.GameOver();
    } else {
      wasteCountText.text = "WASTE IN: " + (wasteIn - wasteCount).ToString("0.00");
    }
  }
}
