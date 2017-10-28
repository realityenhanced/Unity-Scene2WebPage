#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class GlTF_PointLight : GlTF_Light {
	public float constantAttenuation = 1f;
	public float linearAttenuation = 0f;
	public float quadraticAttenuation = 0f;

	public GlTF_PointLight () { type = "point"; }

	public override void Write()
	{
        Indent(); jsonWriter.Write("{\n");
        WriteColorAndType();
        CommaNL();
        IndentIn();
        Indent(); jsonWriter.Write("\"positional\": [\n");
        IndentIn();
        Indent(); jsonWriter.Write("\"constantAttentuation\": " + constantAttenuation); CommaNL();
        Indent(); jsonWriter.Write("\"linearAttenuation\": " + linearAttenuation); CommaNL();
        Indent(); jsonWriter.Write("\"quadraticAttenuation\": " + quadraticAttenuation);
        Indent(); jsonWriter.Write("]\n");
        IndentOut();
        IndentOut();

        Indent(); jsonWriter.Write("}\n");

    }
}
#endif