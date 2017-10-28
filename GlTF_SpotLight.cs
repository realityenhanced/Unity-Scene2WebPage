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
        CommaNL();
        IndentIn();
        Indent(); jsonWriter.Write("\"positional\": [");
        IndentIn();
        Indent(); jsonWriter.Write ("\"constantAttentuation\": "+constantAttenuation); CommaNL();
        Indent(); jsonWriter.Write("\"linearAttenuation\": " + linearAttenuation); CommaNL();
        Indent(); jsonWriter.Write("\"quadraticAttenuation\": " + quadraticAttenuation);
        Indent(); jsonWriter.Write("\"spot\": [");
        IndentIn();
        Indent(); jsonWriter.Write ("\"fallOffAngle\": "+fallOffAngle);CommaNL();
        Indent(); jsonWriter.Write ("\"fallOffExponent\": "+fallOffExponent);
        Indent(); jsonWriter.Write("]"); CommaNL();
        IndentOut();
        IndentOut();
        Indent(); jsonWriter.Write("]");
        IndentOut();

        Indent(); jsonWriter.Write("}\n");
    }
}
#endif