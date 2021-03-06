# Unity Scene to Webpage exporter

(A Work In Progress)
Unity editor wizard that exports a  Unity Scene to a web page using the **glTF 2.0** format and **three.js** to load it.

Plugin based on Unity-glTF-Exporter from https://github.com/tparisi/Unity-glTF-Exporter & https://github.com/sketchfab/Unity-glTF-Exporter

## How to use it
Video @ https://youtu.be/kHxErX1yYVM
- Copy the Unity-Scene2WebPage folder to your Assets/Editor folder. (The folder location and name are important)
- Once the plugin is imported a new item should appear in the *Tools* menu. You can access the exporter by going through **Tools/Convert Scene to Web page**
- Clicking on it, will launch the browser with the page.

## CREDITS
"Low Poly Earth" model (https://www.blendswap.com/blends/view/86599) by CraigForster (https://www.blendswap.com/user/CraigForster) released under CC-0 license.

Supported Unity objects and features so far:
- Scene objects such as transforms and meshes
- PBR materials (both *Standard* and *Standard (Specular setup)* for metal/smoothness and specular/smoothness respectively). Other materials may also be exported but not with all their channels.
- Solid and skinning animation (note that custom scripts or *humanoid* skeletal animation are not exported yet).
- Lights (In Progress: Basic support available via KHR_Lights extension)

*(Note that animation is still in beta)*

Please note that custom scripts, shaders and post processes are not exported.

## Features
* [PBR Materials](#pbrmaterials)
* [Texture conversion](#texture)
* [Samples](#samples)
* [Important notes](#samples)

<a name="pbrmaterials"></a>

## PBR materials

glTF 2.0 core specification includes metal/roughness PBR material declaration. Specular/glossiness workflow is also available but kept under an extension for now.

Link to the glTF 2.0 specification: https://github.com/KhronosGroup/glTF/tree/2.0/specification/2.0

**Note:** in order to get a correct conversion with Sketchfab materials, the exporter exports bump maps (i.e greyscale textures converted to normal map inside unity) under bumpTexture instead of normalTexture.
It's a temporary workaround that will be quickly fixed.

For example,

```json
"normalTexture" : {
    "index" : 2,
    "texCoord" : 0,
    "scale" : 1
},
```

becomes


```json
"bumpTexture" : {
    "index" : 2,
    "texCoord" : 0,
    "scale" : 1
},
```

The following example describes a Metallic-Roughness material with transparency:
```json
    "materials": [
        {
            "pbrMetallicRoughness": {
                "baseColorFactor": [1, 1, 1, 1],
                "baseColorTexture" : {
                    "index" : 0,
                    "texCoord" : 0
                },
                "roughnessFactor": 1,
                "metallicFactor": 1,
                "metallicRoughnessTexture" : {
                    "index" : 1,
                    "texCoord" : 0
                }
            },
            "doubleSided": false,
            "alphaMode": "BLEND",
            "alphaCutoff": 0.5,
            "normalTexture" : {
                "index" : 2,
                "texCoord" : 0,
                "scale" : 1
            },
            "occlusionTexture" : {
                "index" : 3,
                "texCoord" : 0,
                "strength" : 0.13
            },
            "emissiveFactor": [1, 1, 1],
            "emissiveTexture" : {
                "index" : 4,
                "texCoord" : 0
            },
            "name": "metallicPlane"
        }
    ],
```

It's composed of a set of PBR textures, under `pbrMetallicRoughness`, and a set of additionnal maps.
For specular/glossiness workflow, it's still kept under an extension.

The following example describes an opaque Specular-Glossiness material:
```json
{
    "materials": [
        {
            "extensions": {
                "KHR_materials_pbrSpecularGlossiness": {
                    "diffuseFactor": [1, 1, 1, 1],
                    "diffuseTexture" : {
                        "index" : 0,
                        "texCoord" : 0
                    },
                    "glossinessFactor": 0.358,
                    "specularFactor": [0.2, 0.2, 0.2, 1],
                    "specularGlossinessTexture" : {
                        "index" : 1,
                        "texCoord" : 0
                    }               }

            },
            "doubleSided": false,
            "bumpTexture" : {
                "index" : 2,
                "texCoord" : 0
            },
            "emissiveFactor": [0, 0, 0],
            "name": "specularPlane"
        }
    ],
```

<a name="texture"></a>
## Texture conversion

glTF specification considers OpenGL flipY flag being disabled for images (see this [implementation note](https://github.com/KhronosGroup/glTF/tree/master/specification/1.0#images)).

(For more details about Flip Y flag in WebGL, see [gl.UNPACK_FLIP_Y_WEBGL parameter](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/pixelStorei)).

This flag is enabled for most software, including Unity, so textures need to be flipped along Y axis in order to match glTF specification.
The exporter applies this operation on all the exported textures.

Moreover, Unity uses smoothness and not roughness, so **alpha channel is inverted** for RGBA Metallic/Smoothness textures, also to match glTF specification.

<a name="note"></a>
## Important notes
Please note that for now, output glTF files **may not be 100% compliant** with the current state of glTF 2.0 (as mentioned above with bump maps).
