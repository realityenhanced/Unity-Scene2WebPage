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
        jsonWriter.Write(",\n");
        IndentIn();
        Indent(); jsonWriter.Write("\"positional\": {\n");
        IndentIn();
        Indent(); jsonWriter.Write("\"constantAttentuation\": " + constantAttenuation); jsonWriter.Write(",\n");
        Indent(); jsonWriter.Write("\"linearAttenuation\": " + linearAttenuation); jsonWriter.Write(",\n");
        Indent(); jsonWriter.Write("\"quadraticAttenuation\": " + quadraticAttenuation); jsonWriter.Write("\n");
        IndentOut();
        Indent(); jsonWriter.Write("}\n");
        IndentOut();

        Indent(); jsonWriter.Write("}");

    }
}
#endif