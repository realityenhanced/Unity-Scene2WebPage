#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class GlTF_AmbientLight : GlTF_Light {
    public GlTF_AmbientLight() { type = "ambient"; }
    public override void Write()
	{
        Indent(); jsonWriter.Write("{\n");
        WriteColorAndType();
        jsonWriter.Write("\n");
        Indent(); jsonWriter.Write("}");
    }
}
#endif