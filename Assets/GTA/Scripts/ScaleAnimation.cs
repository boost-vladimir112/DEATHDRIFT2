using UnityEngine;
using System.Collections;

public class ScaleAnimation : MonoBehaviour
{
    public float stretchFactor = 1.4f;
    public float animationSpeed = 8f;
    public float idleDelay = 2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(StretchLoop());
    }

    IEnumerator StretchLoop()
    {
        while (true)
        {
            Vector3 stretchedScale = new Vector3(
                originalScale.x / stretchFactor,
                originalScale.y * stretchFactor,
                originalScale.z / stretchFactor
            );

            // Вытянулся
            float t = 0f;
            while (t < 1f)
            {
                transform.localScale = Vector3.Lerp(originalScale, stretchedScale, t);
                t += Time.deltaTime * animationSpeed;
                yield return null;
            }

            // Вернулся
            t = 0f;
            while (t < 1f)
            {
                transform.localScale = Vector3.Lerp(stretchedScale, originalScale, t);
                t += Time.deltaTime * animationSpeed * 0.5f;
                yield return null;
            }

            // Пауза между циклами
            yield return new WaitForSeconds(idleDelay);
        }
    }
}
