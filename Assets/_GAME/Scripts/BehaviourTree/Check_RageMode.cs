using BehaviourTree;

public class Check_RageMode : Node
{
    private GameManager gameManager;

    public Check_RageMode(GameManager manager)
    {
        gameManager = manager;
    }

    public override NodeState Evaluate()
    {
        if (gameManager.rageMode) state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
