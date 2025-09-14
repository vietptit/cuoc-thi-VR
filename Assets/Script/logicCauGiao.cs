using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class logicCauGiao : MonoBehaviour
{
    [SerializeField] private Transform cauchi;
    [SerializeField] private float duration = 1f;
    [SerializeField] private List<GameObject> lights = new List<GameObject>();
    [SerializeField]  AudioSource audioSource;

    // trạng thái: false = ở -90, true = ở +90
    private bool isAtPositive90 = false;
    private Coroutine rotateCoroutine;

    private void Start()
    {
        if (cauchi == null) cauchi = transform; // phòng trường hợp không gán trong Inspector


        float x = SignedAngle(cauchi.localEulerAngles.x);
        float dPos = Mathf.Abs(Mathf.DeltaAngle(x, 90f));
        float dNeg = Mathf.Abs(Mathf.DeltaAngle(x, -90f));
        isAtPositive90 = (dPos <= dNeg);
    }

    // Gọi method này (ví dụ từ UI hoặc button) để toggle giữa -90 và +90
    public void setRotate()
    {
        audioSource.Play();
        Debug.Log("rotate");
        float target = isAtPositive90 ? -90f : 90f;
        if (target == -90)
        {
            SetlistFalse(true);
            
        }
        else SetlistFalse(false);
        isAtPositive90 = !isAtPositive90;

        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(RotateToLocalX(target, duration));
    }

    private IEnumerator RotateToLocalX(float targetX, float dur)
    {
        Quaternion fromQ = cauchi.localRotation;

        Vector3 startEuler = cauchi.localEulerAngles;
        Vector3 targetEuler = new Vector3(targetX, startEuler.y, startEuler.z);
        Quaternion toQ = Quaternion.Euler(targetEuler);

        float t = 0f;
        while (t < dur)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / dur));
            cauchi.localRotation = Quaternion.Slerp(fromQ, toQ, alpha);
            yield return null;
        }

        cauchi.localRotation = toQ; // đảm bảo đúng góc cuối
        rotateCoroutine = null;
    }

    // trả về góc signed trong khoảng [-180,180]
    private float SignedAngle(float a)
    {
        a %= 360f;
        if (a > 180f) a -= 360f;
        return a;
    }

    void SetlistFalse(bool TF)
    {
        foreach (var hit in lights)
        {

            hit.SetActive(TF);
        }
    }
}
