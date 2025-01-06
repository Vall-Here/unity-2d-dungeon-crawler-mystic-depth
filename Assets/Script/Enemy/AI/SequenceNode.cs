using System.Collections.Generic;

public class SequenceNode : NodeBase
{
    private List<NodeBase> childNodes;

    public SequenceNode(List<NodeBase> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override bool Execute()
    {
        foreach (var child in childNodes)
        {
            if (!child.Execute())
            {
                return false; // Gagal menjalankan anak node
            }
        }
        return true; // Semua anak berhasil
    }
}
