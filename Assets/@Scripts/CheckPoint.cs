using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    #region Fields

    private ParticleSystem confetti;

    #endregion

    #region Init

    private void Start()
    {
        // 자식 오브젝트의 파티클 시스템 찾기
        confetti = GetComponentInChildren<ParticleSystem>();

        if (confetti == null)
        {
            Debug.LogError("자식 오브젝트에 파티클 시스템이 없는데용");
        }
        else
        {
            // 처음에 비활성화
            confetti.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponentInChildren<PlayerController>();

            // 체크포인트 업데이트
            if (playerController != null)
            {
                // 체크포인트 위치에 플레이어가 부활할 때 물체에 끼임 방지
                Vector3 updatedCheckPoint = transform.position + Vector3.up * 6f;

                playerController.SetCheckPoint(updatedCheckPoint);
                Debug.Log("체크포인트 설정 완료!");

                // 파티클 시스템 활성화
                if (confetti != null)
                {
                    // 활성화하기 전에 초기화를 위해 Stop 호출
                    confetti.Stop();
                    // 활성화
                    confetti.gameObject.SetActive(true);
                    // 재생
                    confetti.Play();
                }
            }
            else
            {
                Debug.LogWarning("PlayerController를 찾을 수 없음");
            }
        }
    }


    public Vector3 GetCheckPointPosition()
    {
        return transform.position;
    }

    #endregion
}
