using System.Collections;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    [Header("Wait")]
    [SerializeField] private float _initialWait = 0;

    [Space]

    [Header("Tween Setting")]
    [SerializeField] private float _tweenTime;
    [SerializeField] private float _yStartPosition = -500;
    [SerializeField] private float _yEndPosition = 0;

    [Space]

    [Header("Load Requirments")]
    [SerializeField] private bool _onlyShowOnFirstLoad;

    [Space]

    [Header("Destroy Settings")]
    [SerializeField] private bool _destroyWithPlayer;
    [SerializeField] private float _destroyAfter;

    private RectTransform _rectTransform;

    private void OnEnable()
    {
        if(_destroyWithPlayer)
            print ("Destroy With UI - Added"); // PlayerController.OnDie += DestroyUI;

        if(_onlyShowOnFirstLoad)
        {
            if (true) // GameManager.Instance.WasFirstLoad
            {
                print("GameManager.Instance.WasFirstLoad")

                gameObject.SetActive(false);
                return;
            }
        }

        transform.localScale = Vector3.zero;

        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.position = new Vector3(_rectTransform.position.x, _yStartPosition, _rectTransform.position.z);


        StartCoroutine(nameof(InitialWait));
    }

    private IEnumerator InitialWait()
    {
        yield return Helpers.GetWait (_initialWait);

        LeanTween.moveY(_rectTransform, _yEndPosition, _tweenTime);
        LeanTween.scale(_rectTransform, Vector3.one, _tweenTime);

        if (_destroyAfter != 0)
            StartCoroutine(nameof(DestroyAfter));
        
    }

    private void OnDestroy()
    {
        if (_destroyWithPlayer)
            print ("Destroy With Player - Removed"); // PlayerController.OnDie -= DestroyUI;
    }

    private void DestroyUI() 
    {
        if (_onlyShowOnFirstLoad)
        {
            Destroy(gameObject);
            return;
        }

        LeanTween.moveY(_rectTransform, _yStartPosition, _tweenTime);
        LeanTween.scale(_rectTransform, Vector3.zero, _tweenTime).destroyOnComplete = true;
    }

    private IEnumerator DestroyAfter()
    {
        yield return Helpers.GetWait (_destroyAfter);

        DestroyUI();
    }
}
