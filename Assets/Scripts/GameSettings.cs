using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _animatorHash = Animator.StringToHash("Visible");

    private bool _isMainPanelShown;

    public void ChangePanelShown()
    {
        _animator.SetBool(_animatorHash, _isMainPanelShown = !_isMainPanelShown);
    }
}
