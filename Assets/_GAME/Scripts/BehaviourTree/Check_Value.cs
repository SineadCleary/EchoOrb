using BehaviourTree;

public class Check_Value : Node
{
    string key;

    public Check_Value(string key) 
    {
        this.key = key;
    }

    public override NodeState Evaluate()
    {
        object obj = GetData(key);
        if (obj!=null && (bool)obj)
            state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        return state;
    }
}
