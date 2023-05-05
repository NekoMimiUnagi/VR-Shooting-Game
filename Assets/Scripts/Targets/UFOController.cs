using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : DestroyableTarget
{
    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public float stayDuration = 2f;
    public float appearDisappearDuration = 2f;

    private Vector3 moveDirection;
    private float moveSpeed;
    private float stayTimer;
    private float appearDisappearTimer;
    private bool isVisible;
    private Collider ufoCollider;
    private Renderer ufoRenderer;

    private void Start()
    {
        ufoCollider = GetComponent<Collider>();
        ufoRenderer = GetComponent<Renderer>();
        NewRandomDirection();

        float livingTime = Random.Range(livingTimeMin, livingTimeMax);
        Destroy(this.gameObject, livingTime);
    }

    private void Update()
    {
        if (isVisible)
        {
            stayTimer -= Time.deltaTime;
            appearDisappearTimer -= Time.deltaTime;

            if (stayTimer <= 0)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                NewRandomDirection();
            }

            if (appearDisappearTimer <= 0)
            {
                StartCoroutine(Disappear());
            }
        }
    }

    private void NewRandomDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        stayTimer = Random.Range(0f, stayDuration);
    }

    private IEnumerator Disappear()
    {
        isVisible = false;
        ufoCollider.enabled = false;
        float elapsedTime = 0;
        float currentDuration = appearDisappearDuration / 2f;

        while (elapsedTime < currentDuration)
        {
            elapsedTime += Time.deltaTime;
            ufoRenderer.material.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), elapsedTime / currentDuration);
            yield return null;
        }

        ufoRenderer.enabled = false;
        appearDisappearTimer = Random.Range(appearDisappearDuration, appearDisappearDuration * 2);

        yield return new WaitForSeconds(appearDisappearTimer);
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        ufoRenderer.enabled = true;
        float elapsedTime = 0;
        float currentDuration = appearDisappearDuration / 2f;

        while (elapsedTime < currentDuration)
        {
            elapsedTime += Time.deltaTime;
            ufoRenderer.material.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, elapsedTime / currentDuration);
            yield return null;
        }

        isVisible = true;
        ufoCollider.enabled = true;
        appearDisappearTimer = Random.Range(appearDisappearDuration, appearDisappearDuration * 2);
    }
}
