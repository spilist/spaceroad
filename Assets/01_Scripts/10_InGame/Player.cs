using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
  public static Player pl;
  public PlayerAngle playerAngle;
  public float sensitivity;
  public bool stopping;

  public int stoppingSpeed = 10;
  public float baseSpeed = 80;
  private float speedScale;
  private string moreSpeedCondition;

  private float speed;
  private Vector3 direction;

  private Rigidbody rb;

  // public PlayerDirectionIndicator dirIndicator;

  void Awake() {
    pl = this;
    playerAngle = GetComponent<PlayerAngle>();
  }

  void Start () {
    Vector2 randomV = Random.insideUnitCircle;
    randomV.Normalize();
    direction = new Vector3(randomV.x, 0, randomV.y);
    direction = new Vector3(1, 0, 0);
    setDirection(direction);
    speed = baseSpeed;

    rb = GetComponent<Rigidbody>();
    rb.velocity = direction * speed;
  }

  void FixedUpdate () {
    if (stopping) {
      speed = Mathf.MoveTowards(speed, 0, Time.fixedDeltaTime * stoppingSpeed);
    } else {
      speed = baseSpeed;
    }

    rb.velocity = direction * speed;
  }

  public float getSpeed() {
    return rb.velocity.magnitude;
  }

  public void setPerpDirection(string dir) {
    int sign = (dir == "Left") ? 1 : -1;

    Vector3 perp = new Vector3(-direction.z, 0, direction.x) * sign * Time.fixedDeltaTime * sensitivity;
    playerAngle.tilt(sign);
    setDirection((direction + perp).normalized);
  }

  // void OnTriggerEnter(Collider other) {
  //   string tag = other.tag;

  //   ObjectsMover mover = other.gameObject.GetComponent<ObjectsMover>();

  //   if (mover == null || mover.dangerous()) return;

  //   if (ridingMonster && tag != "MiniMonster" && tag != "RainbowDonut") {
  //     generateMinimon(mover);
  //     return;
  //   }

  //   if (tag == "IceDebris" || tag == "PhaseMonster") {
  //     mover.destroyObject();
  //     return;
  //   }

  //   if (tag == "MiniMonster") {
  //     if (!absorbMinimon(mover)) return;
  //   }

  //   if (tag == "CubeDispenser") {
  //     if (!unstoppable && !isUsingRainbow()) return;
  //   }

  //   goodPartsEncounter(mover, mover.cubesWhenEncounter(), mover.bonusCubes());
  // }

  // public void loseEnergy(int amount, string tag) {
  //   Camera.main.GetComponent<CameraMover>().shake(shakeDuring, shakeBase * amount / 100);
  //   EnergyManager.em.loseEnergy(amount, tag);
  // }

  // public void processCollision(Collision collision) {
  //   ContactPoint contact = collision.contacts[0];
  //   Vector3 normal = contact.normal;
  //   direction = Vector3.Reflect(direction, -normal).normalized;
  //   direction.y = 0;
  //   direction.Normalize();
  // }

  public Vector3 getDirection() {
    return direction;
  }


  public void setDirection(Vector3 dir, float magnitude = 1) {
    // if (magnitude < stopSphereRadius) {
      // stopping = true;
      // stickSpeedScale = 1;
    // } else {
      direction = dir;
      // stickSpeedScale = magnitude > 1 ? 1 : magnitude;
      // stopping = false;
    // }
    // dirIndicator.setDirection(dir);
    playerAngle.setDirection(dir);
  }

  // void Update() {
  //   if (isRotatingByRainbow) {
  //     Vector3 dir = (rainbowPosition - transform.position).normalized;
  //     transform.Translate(dir * Time.deltaTime * 30, Space.World);
  //     transform.Rotate(-Vector3.forward * Time.deltaTime * rdm.rotateAngularSpeed, Space.World);
  //   }

  //   if (afterStrengthen) {
  //     if (afterStrengthenCount < afterStrengthenDuration) {
  //       afterStrengthenCount += Time.deltaTime;
  //     } else {
  //       afterStrengthen = false;
  //     }
  //   }

  //   if (isBouncing()) {
  //     if (bounceDuration > 0) {
  //         bounceDuration -= Time.deltaTime;
  //     } else {
  //       if (reboundingByBlackhole) {
  //         reboundingByBlackhole = false;
  //         if (isRidingRainbowRoad) isRidingRainbowRoad = false;
  //         Camera.main.GetComponent<CameraMover>().stopShake();
  //         afterStrengthenStart();
  //       } else if (bouncingByDispenser) {
  //         bouncingByDispenser = false;
  //       } else if (bouncing) {
  //         bouncing = false;
  //       }
  //     }
  //   }

  //   if (iced) {
  //     if (icedDuration > 0) {
  //       icedDuration -= Time.deltaTime;
  //       icedSpeedFactor = Mathf.MoveTowards(icedSpeedFactor, 1, Time.deltaTime * (1 - icm.playerSpeedReduceTo) / icm.speedRestoreDuring);
  //     } else {
  //       iced = false;
  //       icm.strengthenPlayerEffect.SetActive(false);
  //     }
  //   }
  // }

  // public void destroyObject(string tag, int gaugeGain = 0) {
  //   numDestroyObstacles++;
  //   DataManager.dm.increment("TotalNumDestroyObstacles");

  //   if (tag == "Obstacle_small") {
  //     DataManager.dm.increment("NumDestroySmallAsteroids");
  //     SuperheatManager.sm.addGuageWithEffect(gaugeGain);
  //   }
  //   else if (tag == "Obstacle_big") {
  //     DataManager.dm.increment("NumDestroyAsteroids");
  //     SuperheatManager.sm.addGuageWithEffect(gaugeGain);
  //   }
  //   else if (tag == "Obstacle") {
  //     DataManager.dm.increment("NumDestroyMeteroids");
  //     SuperheatManager.sm.addGuageWithEffect(gaugeGain);
  //   }
  //   else if (tag == "Blackhole") DataManager.dm.increment("NumDestroyBlackholes");
  //   else if (tag == "Monster") DataManager.dm.increment("NumDestroyMonsters");
  //   else if (tag == "DangerousEMP") DataManager.dm.increment("NumDestroyDangerousEMP");

  //   if (!usingEMP) Camera.main.GetComponent<CameraMover>().shake();
  // }

  // public void encounterObject(string tag) {
  //   numUseObjects++;
  //   DataManager.dm.increment("TotalNumUseObjects");

  //   if (tag == "Jetpack") DataManager.dm.increment("NumUseJetpack");
  //   else if (tag == "Metal") DataManager.dm.increment("NumUseMetal");
  //   else if (tag == "Monster") DataManager.dm.increment("NumRideMonster");
  //   else if (tag == "Dopple") DataManager.dm.increment("NumMeetDopple");
  //   else if (tag == "ComboPart") DataManager.dm.increment("NumGenerateIllusion");
  //   else if (tag == "CubeDispenser") DataManager.dm.increment("NumBumpCubeDispenser");
  //   else if (tag == "SummonPart") DataManager.dm.increment("NumSummon");
  //   else if (tag == "RainbowDonut") DataManager.dm.increment("NumRideRainbow");
  //   else if (tag == "EMP") DataManager.dm.increment("NumGenerateForcefield");
  //   else if (tag == "Magnet") DataManager.dm.increment("Magnet");
  //   else if (tag == "Transformer") DataManager.dm.increment("Transformer");
  //   else Debug.LogError("Exception? " + tag);
  // }

  // void OnDisable() {
  //   DataManager.dm.setBestInt("BestBoosters", numBoosters);
  //   DataManager.dm.setBestInt("BestNumDestroyObstacles", numDestroyObstacles);
  //   DataManager.dm.setBestInt("BestNumUseObjects", numUseObjects);
  // }

  // public bool isInvincible() {
  //   return afterStrengthen || ridingMonster || unstoppable || isRebounding() || isUsingRainbow() || changeManager.isTeleporting();
  // }
}
