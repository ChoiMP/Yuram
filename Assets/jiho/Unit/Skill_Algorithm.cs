using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Algorithm : MonoBehaviour
{
    [Header("적용 안해줘도 됨")] 
    [SerializeField] protected Unit apply_Unit;

    [SerializeField] protected LayerMask attackLayer;

    public List<Status> skill_Tartget;//해당 스킬 범위에 있는 오브젝트들
    Collider2D[] overlapColider;


    //===============기즈모

    [Header("Skill Range Gizmo Settings")]
    [SerializeField] private Color gizmoColor = Color.red; // 기즈모 색상
    [SerializeField] private bool showGizmo = false; // 기즈모 표시 여부 (초기에는 false로 설정)

    private enum SkillType { None, Straight, Circle } // 스킬 유형
    private SkillType currentSkillType = SkillType.None; // 현재 스킬 유형
    private Vector2 gizmoSize; // 기즈모 크기
    private float gizmoRadius; // 기즈모 반경 (원형 스킬에 사용)
    private Vector3 gizmoPosition; // 기즈모 위치


    /*
     감지하는 형태의 스킬들 모음
   
     */
    // ============================제라스 q   ======================================
    public List<Status> Straight_Skill(Skill skillObject)
    {
        overlapColider = Physics2D.OverlapBoxAll(transform.position, skillObject.skillRange, 0);

        Colider_to_Status(overlapColider);

         // 기즈모 설정 (범위를 그리기 위해)
         showGizmo = true; // 기즈모 표시를 활성화
        currentSkillType = SkillType.Straight; // 현재 스킬 유형을 직선 스킬로 설정
        gizmoSize = skillObject.skillRange; // 스킬 범위를 기즈모 크기로 설정
        gizmoPosition = transform.position; // 기즈모 위치를 현재 위치로 설정


        return skill_Tartget;
    }
    // ============================제라스 q   ======================================


    //=========================알리스타q=============================
    public List<Status> Circle_Skill(Skill skillObject)
    {
        overlapColider = Physics2D.OverlapCircleAll(transform.position, skillObject.skillRange.x, attackLayer);

        Colider_to_Status(overlapColider);

        // 기즈모 설정 (범위를 그리기 위해)
        showGizmo = true; // 기즈모 표시를 활성화
        currentSkillType = SkillType.Circle; // 현재 스킬 유형을 원형 스킬로 설정
        gizmoRadius = skillObject.skillRange.x; // 원형 스킬의 반경을 설정
        gizmoPosition = transform.position; // 기즈모 위치를 현재 위치로 설정

        return skill_Tartget;
    }
    //=========================알리스타q=============================

    //=====================스몰더 W===================================
    public void Straight_Projectile_Skill(Skill skillObject)
    {
        StartCoroutine(Straight_Projectile_Skill_corutine(skillObject));
    }

    IEnumerator Straight_Projectile_Skill_corutine(Skill skillObject)
    {
        float t = 0;

        Vector2 dir= skillObject.skill_Tartget[0].transform.position - skillObject.transform.position;

      // Vector2 dir = Vector2.left;
        while (t<10)
        {
            skillObject.transform.Translate(dir * skillObject.speed * Time.deltaTime);
            print("fireball이동중");
            if (t<3)
            {
                t += Time.deltaTime;
                yield return null;
            }
            else
            {
                skillObject.gameObject.SetActive(false);
                break;
            }
        }

    }
    //=====================스몰더 W===================================

    //===================힐===============================
    public void HealUnit(Status unit , Skill skill)
    {
        unit.RetrunHP += unit.curDamege * skill.skillDamegeRatio;
    }

    //===================힐===============================

    void Colider_to_Status(Collider2D [] collider)//OverlapCircleAll의 콜라이더형을 Status형식으로 바꿈
    {
        skill_Tartget.Clear();
        for (int i = 0; i < collider.Length; i++)
        {
            skill_Tartget.Add(collider[i].GetComponent<Status>());
        }
    }



    private void OnDrawGizmos()
    {
        if (!showGizmo) return; // 기즈모 표시 설정이 꺼져 있으면 리턴

        Gizmos.color = gizmoColor; // 기즈모 색상 설정

        // 현재 스킬 유형에 따라 기즈모를 그린다
        if (currentSkillType == SkillType.Straight)
        {
            // 직선 스킬 범위 (OverlapBox와 유사한 모양)
            Gizmos.DrawWireCube(gizmoPosition, gizmoSize);
        }
        else if (currentSkillType == SkillType.Circle)
        {
            // 원형 스킬 범위 (OverlapCircle과 유사한 모양)
            Gizmos.DrawWireSphere(gizmoPosition, gizmoRadius);
        }
    }

    // 스킬 범위 그리기를 종료하기 위한 메서드
    public void HideSkillRange()
    {
        showGizmo = false; // 기즈모 표시를 비활성화
    }
}
