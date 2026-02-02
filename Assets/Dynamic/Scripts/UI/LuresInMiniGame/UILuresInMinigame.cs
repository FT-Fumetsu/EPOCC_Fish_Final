using UnityEngine;

using Manager.Lure;

public class UILuresInMinigame : MonoBehaviour
{
    [SerializeField] private GameObject _luresButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckLureButton();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckLureButton()
    {
        if (!LureManager.Instance.HasAnyLure())
        {
            _luresButton.SetActive(false);
            Debug.LogWarning("Has No Lures");
            return;
        }
        _luresButton.SetActive(true);
        Debug.Log("Has Lures");
    }
}
