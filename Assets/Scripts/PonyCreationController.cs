using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PonyCreationController : MonoBehaviour
{

    public Transform PonyPrefab;

    public Transform PonyParent;

    public GameObject SafeZone;

    private Rect _safeRect;
    private bool _stopCreation = false;
    public int PonyCreated = 0;
    // Use this for initialization
    void Start()
    {
        // Собираем нормальный рект
        _safeRect = SafeZone.GetComponent<RectTransform>().rect;
        _safeRect.position = SafeZone.transform.position;
        _safeRect.position -= _safeRect.size / 2f;
        _safeRect.size *= 1.2f;

        _createPony();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Ответи понни в зону
    /// </summary>
    public void PonyCatch()
    {
        PonyCreated--;
        if (_stopCreation)
        {
            _stopCreation = false;
            _createPony();
        }
    }



    /// <summary>
    /// Создаём понни, и рекурсию заодно, выходим по достижению 200 понни
    /// </summary>
    private void _createPony()
    {
        if (PonyCreated > 199)
        {
            _stopCreation = true;
            return;
        }

        PonyCreated++;
        Vector2 randPos = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        while (_safeRect.Contains(randPos))
        {
            randPos = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        }
        Transform spawned = PoolBoss.Spawn(PonyPrefab, randPos, new Quaternion(), PonyParent);
        spawned.GetComponent<Collider2D>().enabled = true;

        StartCoroutine(_waitCoroutine(0.5f, _createPony));

    }

    public IEnumerator _waitCoroutine(float waitDelay, Action action)
    {
        yield return new WaitForSeconds(waitDelay);
        action();
    }
}
