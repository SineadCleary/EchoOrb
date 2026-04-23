using BehaviourTree;
using UnityEngine;

public class HasOrb : MonoBehaviour
{
    [SerializeField] bool hasOrb = false;
    [SerializeField] Animator animator;
    public Holder targetHolder;
    public Holder lastHolder;

    private void Start()
    {
        animator.SetBool("hasOrb", hasOrb);
    }

    public void SetHasOrb(bool hasOrb)
    {
        this.hasOrb = hasOrb;
        animator.SetBool("hasOrb", hasOrb);
    }

    public bool GetHasOrb()
    {
        return hasOrb;
    }

}

public class Check_HasOrb : Node
{
    HasOrb hasOrb;

    public Check_HasOrb(HasOrb hasOrb)
    {
        this.hasOrb = hasOrb;
    }

    public override NodeState Evaluate()
    {
        if (hasOrb.GetHasOrb())
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
