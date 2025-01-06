using System.Collections.Generic;

public class SelectorNode : NodeBase
{
    private List<NodeBase> childNodes;

    public SelectorNode(List<NodeBase> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override bool Execute()
    {
        foreach (var child in childNodes)
        {
            if (child.Execute())
            {
                return true; 
            }
        }
        return false; 
    }
}
