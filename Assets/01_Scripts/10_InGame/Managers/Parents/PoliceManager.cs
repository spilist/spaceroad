using UnityEngine;
using System.Collections;

public class PoliceManager : LRObjectsManager {
  public float angleGoStraight = 5;
  // - 디텍트 거리
  // - 디텍트거리 바깥에서 쫓아오는 속도 (OffScreenSpeedScale, 플레이어 속도에 비례)
  // - 추격 후 액션까지의 시간
  // - 액션에 걸리는 시간
  // - 액션 도중 속도 감소 비율/정도
  // - 액션에 필요한 오브젝트
  //     - 발사체가 있을 경우: 발사체의 속도, 거리, 데미지
}
