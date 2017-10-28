#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class GlTF_Light : GlTF_Writer {
	public GlTF_ColorRGB color;
	public string type;

    public void WriteColorAndType()
    {
        IndentIn();
        Indent(); jsonWriter.Write("\"type\": \"" + type + "\""); CommaNL(); CommaNL();
        color.Write();
        IndentOut();
    }
}
#endif