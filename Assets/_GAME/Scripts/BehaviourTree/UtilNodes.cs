using BehaviourTree;
using Mono.Cecil.Cil;
using System.Collections.Generic;

// Code based on: https://medium.com/geekculture/how-to-create-a-simple-behaviour-tree-in-unity-c-3964c84c060e

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            //bool anyChildIsRunning = false;
            foreach (Node node in children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    //default:
                    //    state = NodeState.SUCCESS;
                    //    return state;
                }
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }

    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            // if no children
            state = NodeState.FAILURE;
            return state;
        }
    }

    public class Inverter : Node
    {
        private Node child;

        public Inverter(Node child)
        {
            this.child = child;
        }

        public override NodeState Evaluate()
        {
            if (child == null)
            {
                state = NodeState.FAILURE; // fallback if no child
                return state;
            }

            switch (child.Evaluate())
            {
                case NodeState.SUCCESS:
                    state = NodeState.FAILURE;
                    break;
                case NodeState.FAILURE:
                    state = NodeState.SUCCESS;
                    break;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    break;
                default: 
                    state = NodeState.FAILURE; 
                    break;
            }

            return state;
        }
    }
}