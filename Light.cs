using OpenTK.Mathematics;

namespace INFOGR2023TemplateP2
{
    public class Light
    {
        public Vector3 position; //new Vector3(0, 5, 5)
        public Vector3 color; //new Vector3(255,255,255)
        public Vector3 ambient; //new Vector3(0.25f)
        public Vector3 diffuse; //new Vector3(0.5f)
        public Vector3 specular; //new Vector3(1.0f)

        public Light(Vector3 pos, Vector3 col, Vector3 amb, Vector3 diff, Vector3 spec) {
            position = pos;
            color = col;
            ambient = amb;
            diffuse = diff;
            specular = spec;
        }
    }
}
