#version 330
 
// shader inputs
in vec4 positionWorld;              // fragment position in World Space
in vec4 normalWorld;                // fragment normal in World Space
in vec2 uv;                         // fragment uv texture coordinates

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    vec3 color;
};

uniform Light light;
uniform vec3 cameraPosition;

uniform sampler2D diffuseTexture;	// texture sampler

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
    //ambient
    vec3 ambient = light.ambient * vec3(texture(diffuseTexture, uv));

    // Diffuse 
    vec3 L = light.position - positionWorld.xyz; // vector from surface to light, unnormalized!
    float attenuation = 1.0 / dot(L, L); // distance attenuation
    float NdotL = max(0, dot(normalize(normalWorld.xyz), normalize(L))); // incoming angle attenuation
    vec3 diffuseColor = texture(diffuseTexture, uv).rgb; // texture lookup
    vec3 diffuse = light.color * attenuation * diffuseColor  * NdotL; // complete diffuse shading, A = 1.0 is opaque

    // Specular
    //vec3 specColor = texture(diffuseTexture, uv).rgb;
    vec3 V = cameraPosition - positionWorld.xyz;
    vec3 R = reflect(-normalize(L), normalize(normalWorld).xyz);
    float VdotR = pow(max(0, dot(normalize(V), normalize(R))), 20); //5.0 = shininess = n
    vec3 specular = light.specular * VdotR;

    vec3 result = (ambient + diffuse + specular);

    outputColor = vec4(result, 1.0);
}