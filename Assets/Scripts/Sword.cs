using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator myAnimator;
    void Awake()
    {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        playerControls.Enable();
    }
    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }
    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }
}
