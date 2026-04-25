using System.Collections;
using System.Collections.Generic;

// Code based on: https://medium.com/geekculture/how-to-create-a-simple-behaviour-tree-in-unity-c-3964c84c060e

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();
        private Dictionary<string, object> dataContext =
            new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach(Node child in children)
            {
                Attach(child);
            }
        }

        // Attach creates the edge between a node and its new child
        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            //dataContext[key] = value;
            // store on the root
            Node node = this;
            while (node.parent != null)
                node = node.parent;

            node.dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object val = null;
            if(dataContext.TryGetValue(key, out val)) return val;

            Node node = parent;
            if (node != null)
                val = node.GetData(key);
            return val;
        }

        public bool ClearData(string key)
        {
            bool cleared = false;
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }
            Node node = parent;
            if (node != null) cleared = node.ClearData(key);
            return cleared;
        }
    }
}