using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenTK.Mathematics;

namespace INFOGR2023TemplateP2
{
    public class Node
    {
        public Matrix4 objectToParent;
        public Mesh mesh;
        public List<Node> children = new List<Node>();
        public Matrix4 worldToCamera;
        public Matrix4 cameraToScreen;

        public Node(Matrix4 objectToParent, Mesh mesh)
        {
            this.objectToParent = objectToParent;
            this.mesh = mesh;

            Light light1 = new Light(new Vector3(5, 10, 5), new Vector3(255, 255, 255), new Vector3(0.25f), new Vector3(0.5f), new Vector3(1.0f));
            this.mesh.light = light1;
        }

        public void Render(Shader shader, Matrix4 position, Texture texture, Vector3 cameraPosition)
        {

            mesh.Render(shader, position * worldToCamera * cameraToScreen, position, texture, cameraPosition);

            if (children.Count > 0) {
                
                foreach (Node child in children) {
                    child.worldToCamera = worldToCamera;
                    child.cameraToScreen = cameraToScreen;
                    child.Render(shader, child.objectToParent * position, texture, cameraPosition);
                }
            }
        }
    }
    public class SceneGraphs
    {
        public List<Node> children;

        public SceneGraphs() {
            children = new List<Node>();
        }

        public void Render(Shader shader, Matrix4 worldToCamera, Matrix4 cameraToScreen, Texture texture, Vector3 cameraPosition)
        {
            foreach (Node child in children)
            {
                child.worldToCamera = worldToCamera;
                child.cameraToScreen = cameraToScreen;
                child.Render(shader, child.objectToParent, texture, cameraPosition);
            }
        }
    }
}
