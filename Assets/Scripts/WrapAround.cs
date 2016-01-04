using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TextMesh))]
public class WrapAround : MonoBehaviour {

    TextMesh TheMesh;
    Renderer MeshRender;
    public float xBound;
    public string UnwrappedText;
    public float MaxWidth;
    public bool NeedsLayout = true;
    public bool ConvertNewLines = false;

    public void Start()
    {
        TheMesh = GetComponent<TextMesh>();
        MeshRender = GetComponent<MeshRenderer>();
        if (ConvertNewLines)
            UnwrappedText = UnwrappedText.Replace("\\n", System.Environment.NewLine);
    }

    string BreakPartIfNeeded(string part)
    {
        string saveText = TheMesh.text;
        TheMesh.text = part;
        xBound = MeshRender.bounds.extents.x;

        if (MeshRender.bounds.extents.x > MaxWidth)
        {
            string remaining = part;
            part = "";
            while (true)
            {
                int len;
                for (len = 2; len <= remaining.Length; len++)
                {
                    TheMesh.text = remaining.Substring(0, len);
                    if (MeshRender.bounds.extents.x > MaxWidth)
                    {
                        len--;
                        break;
                    }
                }
                if (len >= remaining.Length)
                {
                    part += remaining;
                    break;
                }
                part += remaining.Substring(0, len) + System.Environment.NewLine;
                remaining = remaining.Substring(len);
            }

            part = part.TrimEnd();
        }

        TheMesh.text = saveText;

        return part;
    }

    void Update()
    {
        if (!NeedsLayout)
            return;
        NeedsLayout = false;
        if (MaxWidth == 0)
        {
            TheMesh.text = UnwrappedText;
            return;
        }
        string builder = "";
        string text = UnwrappedText;
        TheMesh.text = "";
        string[] parts = text.Split(' ');
        for (int i = 0; i < parts.Length; i++)
        {
            string part = BreakPartIfNeeded(parts[i]);
            TheMesh.text += part + " ";
            if (MeshRender.bounds.extents.x > MaxWidth)
            {
                TheMesh.text = builder.TrimEnd() + System.Environment.NewLine + part + " ";
            }
            builder = TheMesh.text;
        }
    }
}
