using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyManager : MonoBehaviour {
  public static EnergyManager em;
  public float maxEnergy;
  public int ignoreDamage = 50;
  public Text energyText;
  private float energy;

  void Awake() {
    em = this;
    getFullHealth();
  }

  public void getFullHealth() {
    setEnergy(maxEnergy);
  }

  public void getEnergy(float amount) {
    setEnergy(Mathf.Min(energy + amount, maxEnergy));
    // Player.pl.getEnergy.Play();
    // Player.pl.getEnergy.GetComponent<AudioSource>().Play();
  }

  public void loseEnergy(float amount) {
    if (amount < ignoreDamage) return;

    setEnergy(Mathf.Max(energy - amount, 0));
    // takeDamageSound.Play();
  }

  void setEnergy(float val) {
    energy = val;
    energyText.text = "ENERGY: " + energy.ToString("0");
    if (energy == 0) GameManager.gm.GameOver("BUSTED");
  }

  public float currentEnergy() {
    return energy;
  }
}

