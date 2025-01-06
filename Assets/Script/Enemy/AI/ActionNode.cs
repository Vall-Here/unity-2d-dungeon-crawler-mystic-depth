using System;

public class ActionNode : NodeBase
{
    private Func<bool> action;

    public ActionNode(Func<bool> action)
    {
        this.action = action;
    }

    public override bool Execute()
    {
        return action();
    }
}
