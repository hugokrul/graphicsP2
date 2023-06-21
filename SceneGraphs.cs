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

        public Node(Matrix4 objectToParent, Mesh mesh)
        {
            this.objectToParent = objectToParent;
            this.mesh = mesh;
        }

        public void Render(Shader shader, Matrix4 worldToScreen, Matrix4 parentToWorld, Texture texture)
        {
            Matrix4 objectToWorld = objectToParent * parentToWorld;
            Matrix4 objectToScreen = objectToWorld * worldToScreen;

            mesh.Render(shader, objectToScreen, objectToWorld, texture);
            
        }
    }
    public class SceneGraphs
    {
        public List<Node> children;

        public SceneGraphs() {
            children = new List<Node>();
        }

        public void Render(Shader shader, Matrix4 worldToScreen, Matrix4 parentToWorld, Texture texture)
        {
            foreach (Node child in children)
            {
                child.Render(shader, worldToScreen, child.objectToParent * parentToWorld, texture);
            }
        }
    }
}
