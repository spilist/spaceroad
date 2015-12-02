using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
  public static Player pl;
  public ForceMode forceMode;
  public Transform characters;
  public string character;
  public PlayerAngle playerAngle;

  public Text speedText;
  public float baseSpeed = 80;
  public float maxSpeed = 200;
  public float acceleration = 40;
  public float deceleration = 20;
  public float curveSensitivity = 2;
  public float turnSpeed = 180;
  private float speedScale;
  private string moreSpeedCondition;

  private float speed;
  private Vector3 direction;

  private Rigidbody rb;
  private bool gameStarted;
  private bool decelerating;

  private float m_MovementInputValue;         // The current value of the movement input.
  private float m_TurnInputValue;             // The current value of the turn input.

  // public PlayerDirectionIndicator dirIndicator;

  void Awake() {
    pl = this;
    playerAngle = GetComponent<PlayerAngle>();

    Transform cha = characters.Find(character);
    GetComponent<MeshFilter>().sharedMesh = cha.GetComponent<MeshFilter>().sharedMesh;
    cha.GetChild(0).gameObject.SetActive(true);
    cha.GetChild(0).SetParent(transform, false);
    //GetComponent<BoxCollider>().size = cha.GetComponent<BoxCollider>().size;
  }

  void Start () {
    // Vector2 randomV = Random.insideUnitCircle;
    // randomV.Normalize();
    // direction = new Vector3(randomV.x, 0, randomV.y);
    // setDirection(direction);

    rb = GetComponent<Rigidbody>();
    SpawnManager.sm.run();
  }

  void FixedUpdate () {
    // speed = caculateSpeed();
    // rb.velocity = direction * speed;

    // if (rb.velocity.magnitude < maxSpeed) {
    //   rb.AddForce(direction * acceleration, forceMode);
    // }

    // rb.AddRelativeForce(0f, 0f, powerInput * 90);
    // rb.AddRelativeTorque(0f, turnInput * 5, 0f);

    Move();
    Turn();

    speedText.text = "SPEED: " + rb.velocity.magnitude.ToString("0");
  }

  void Move () {
      // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
      //Vector3 movement = transform.forward * speed * Time.deltaTime;

		if (rb.velocity.magnitude < maxSpeed) {

			if (decelerating) {
				rb.velocity = rb.velocity - transform.forward * Time.fixedDeltaTime * deceleration;
				Debug.Log ("decel");

			} else {
				rb.velocity = rb.velocity + transform.forward * Time.fixedDeltaTime * acceleration;
				Debug.Log ("accel");
			}

		}

  }

  void Turn () {

		if (m_TurnInputValue != 0) {
			decelerating = true;
			Quaternion deltaAngle =
			Quaternion.AngleAxis (Mathf.Rad2Deg * m_TurnInputValue * turnSpeed * Time.deltaTime, transform.up);
			transform.Rotate (deltaAngle.eulerAngles);
		
			Vector3 v = rb.velocity;
			v = deltaAngle * v;
			rb.velocity = v;
		} else {
			decelerating = false;
		}

      // Determine the number of degrees to be turned based on the input, speed and time between frames.
      //float turn = m_TurnInputValue * turnSpeed * Time.deltaTime;

      // Make this into a rotation in the y axis.
      //Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

      // Apply this rotation to the rigidbody's rotation.
      //rb.MoveRotation (rb.rotation * turnRotation);

      // Debug.Log(rb.rotation);
      // if (tilting) {
      //   currentTilt = Mathf.MoveTowards(currentTilt, tiltAmount, Time.deltaTime * maxTiltAmount / tiltDuration);
      //   transform.localEulerAngles = new Vector3(0, currentAngle, 0 + currentTilt);

      //   if (currentTilt == tiltAmount) {
      //     tilting = false;
      //   }
      // }

      // if (tiltingBack) {
      //   currentTilt = Mathf.MoveTowards(currentTilt, 0, Time.deltaTime * maxTiltAmount / tiltBackDuration);
      //   transform.localEulerAngles = new Vector3(0, currentAngle, 0 + currentTilt);

      //   if (currentTilt == 0) {
      //     tiltingBack = false;
      //   }
      // }
  }

  public void tiltBack() {
    decelerating = false;
    playerAngle.tiltBack();
    m_TurnInputValue = 0;
  }

  public void setPerpDirection(string dir) {
    decelerating = true;
    int sign = (dir == "Left") ? -1 : 1;
    m_TurnInputValue = sign;
    rb.angularVelocity = Vector3.zero;
    // Vector3 perp = new Vector3(-direction.z, 0, direction.x) * sign * Time.fixedDeltaTime * curveSensitivity;
    // playerAngle.tilt(sign);
    // playerAngle.setDirection(dir);

    // setDirection((direction + perp).normalized);
  }

  float caculateSpeed() {
    if (!gameStarted) {
      return baseSpeed;
    } else if (decelerating) {
      return Mathf.MoveTowards(speed, baseSpeed, Time.fixedDeltaTime * deceleration);
    } else {
      return Mathf.MoveTowards(speed, maxSpeed, Time.fixedDeltaTime * acceleration);
    }
  }

  public void processCollision(ObjectMover mover, Collision collision) {
    speed = Mathf.Max(0, speed - 100);
    Vector3 relVelocity = collision.relativeVelocity;
    // Debug.Log(relVelocity);
    float objMass = mover.GetComponent<Rigidbody>().mass;
    Vector3 collisionForce = relVelocity * objMass;

    // Vector3 normal = collision.contacts[0].normal;
    // Vector3 dir = Vector3.Reflect(direction, -normal).normalized;
    // dir.y = 0;
    // dir.Normalize();

    // speed -= 200;
    // if (speed < 0) {
      // direction = dir;
      // speed = -speed;
    // }
    // setDirection(dir);
    // rb.AddForceAtPosition(collisionForce, collision.contacts[0].point, ForceMode.Impulse);
  }

  public float getSpeed() {
    return rb.velocity.magnitude;
  }

  public void gameStart() {
    gameStarted = true;
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
