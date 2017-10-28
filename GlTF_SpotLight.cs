#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class GlTF_SpotLight : GlTF_Light {
	public float constantAttenuation = 1f;
	public float fallOffAngle = (3.1415927f / 2.0f);
	public float fallOffExponent = 0f;
	public float linearAttenuation = 0f;
	public float quadraticAttenuation = 0f;

	public GlTF_SpotLight () { type = "spot"; }

	public override void Write()
	{
        Indent(); jsonWriter.Write("{\n");

        WriteColorAndType();

        jsonWriter.Write(",\n");
        IndentIn();
        Indent(); jsonWriter.Write("\"positional\": {\n");
        IndentIn();
        Indent(); jsonWriter.Write ("\"constantAttentuation\": "+constantAttenuation); jsonWriter.Write(",\n");
        Indent(); jsonWriter.Write("\"linearAttenuation\": " + linearAttenuation); jsonWriter.Write(",\n");
        Indent(); jsonWriter.Write("\"quadraticAttenuation\": " + quadraticAttenuation); jsonWriter.Write(",\n");
        Indent(); jsonWriter.Write("\"spot\": {\n");
        IndentIn();
        Indent(); jsonWriter.Write ("\"fallOffAngle\": "+fallOffAngle); jsonWriter.Write(",\n");
        Indent(); jsonWriter.Write ("\"fallOffExponent\": "+fallOffExponent); jsonWriter.Write("\n");
        IndentOut();
        Indent(); jsonWriter.Write("}\n");
        IndentOut();
        Indent(); jsonWriter.Write("}\n");
        IndentOut();

        Indent(); jsonWriter.Write("}");
    }
}
#endif