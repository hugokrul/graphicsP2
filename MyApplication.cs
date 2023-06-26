using System.Diagnostics;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace INFOGR2023TemplateP2
{
    class MyApplication
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        Mesh? teapot, floor, human;                    // meshes to draw using OpenGL
        float a = 0;                            // teapot rotation angle
        readonly Stopwatch timer = new();       // timer for measuring frame duration
        Shader? shader;                         // shader to use for rendering
        Shader? postproc;                       // shader to use for post processing
        Texture? wood;                          // texture to use for rendering
        RenderTarget? target;                   // intermediate render target
        ScreenQuad? quad;                       // screen filling quad for post processing
        readonly bool useRenderTarget = true;   // required for post processing
        SceneGraphs world;
        public KeyboardState keyboard;
        public Vector3 cameraPosition = new Vector3(0, 7, -10);
        float viewAngleVert;
        Matrix4 rotateVertical;
        float speed = 0.3f;

        Vector3 cameraDirection, cameraUp, cameraRight;

        // constructor
        public MyApplication(Surface screen)
        {
            this.screen = screen;
            viewAngleVert = 0;
            rotateVertical = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);

            cameraDirection = new Vector3(0, 0, 1);
            cameraUp = new Vector3(0, 1, 0);
            cameraRight = Vector3.Cross(cameraDirection, cameraUp);
        }
        // initialize
        public void Init()
        {
            // load teapot
            teapot = new Mesh("../../../assets/teapot.obj");
            floor = new Mesh("../../../assets/floor.obj");
            human = new Mesh("../../../assets/FinalBaseMesh.obj");
            // initialize stopwatch
            timer.Reset();
            timer.Start();
            // create shaders
            shader = new Shader("../../../shaders/vs.glsl", "../../../shaders/fs.glsl");
            postproc = new Shader("../../../shaders/vs_post.glsl", "../../../shaders/fs_post.glsl");
            // load a texture
            wood = new Texture("../../../assets/wood.jpg");
            // create the render target
            if (useRenderTarget) target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();
        }

        // tick for background surface
        public void Tick()
        {
            screen.Clear(0);
            screen.Print("hello world", 2, 2, 0xffff00);

            if (keyboard.IsKeyDown(Keys.W))
            {
                cameraPosition += new Vector3(cameraDirection.X, 0, cameraDirection.Z) * speed;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                cameraPosition -= new Vector3(cameraDirection.X, 0, cameraDirection.Z) * speed;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                cameraPosition += new Vector3(cameraRight.X, 0, cameraRight.Z) * speed;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                cameraPosition -= new Vector3(cameraRight.X, 0, cameraRight.Z) * speed;

            }
            if (keyboard.IsKeyDown(Keys.Space))
            {
                cameraPosition.Y -= speed;
            }
            if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift))
            {
                cameraPosition.Y += speed;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                viewAngleVert -= speed * 0.1f;
                rotateCameraHorizontal(speed);
                rotateVertical = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), viewAngleVert);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                viewAngleVert += speed * 0.1f;
                rotateCameraHorizontal(-speed);
                rotateVertical = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), viewAngleVert);
            }
        }

        public void rotateCameraHorizontal(float rotateX)
        {
            // Calculate the rotation angle to rotate around.
            // If you rotate horizontally, you always rotate around the Y axis.
            Quaternion rotateHorizontal = new Quaternion(new Vector3(0, 1, 0) * 0.1f * rotateX);

            // Rotate both the direction and the updirection around that angle.
            cameraDirection = Vector3.Normalize(Vector3.Transform(cameraDirection, rotateHorizontal));
            cameraUp = Vector3.Normalize(Vector3.Transform(cameraUp, rotateHorizontal));
            cameraRight = Vector3.Cross(cameraDirection, cameraUp);
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            // measure frame duration
            float frameDuration = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Start();

            // prepare matrix for vertex shader
            float angle90degrees = 3;
            Matrix4 humanObjectToWorld = Matrix4.CreateScale(1f) * Matrix4.CreateTranslation(new Vector3(5, -8, -5));
            Matrix4 teapotObjectToWorld = Matrix4.CreateScale(1f) * Matrix4.CreateTranslation(new Vector3(0, -8, 0));
            Matrix4 floorObjectToWorld = Matrix4.CreateScale(4f);
            Matrix4 worldToCamera = Matrix4.CreateTranslation(cameraPosition) * rotateVertical;
            Matrix4 cameraToScreen = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), (float)screen.width/screen.height, .1f, 1000);

            // update rotation
            a += 0.001f * frameDuration;
            if (a > 2 * MathF.PI) a -= 2 * MathF.PI;

            if (useRenderTarget && target != null && quad != null)
            {
                // enable render target
                target.Bind();

                // render scene to render target
                if (shader != null && wood != null)
                {
                    human?.Render(shader, humanObjectToWorld * worldToCamera * cameraToScreen, humanObjectToWorld, wood);
                    teapot?.Render(shader, teapotObjectToWorld * worldToCamera * cameraToScreen, teapotObjectToWorld, wood);
                    floor?.Render(shader, floorObjectToWorld * worldToCamera * cameraToScreen, floorObjectToWorld, wood);

                    // world = new SceneGraphs();
                    // Node? teapotNode = new Node(teapotObjectToWorld, teapot);
                    // Node? floorNode = new Node(floorObjectToWorld, floor);
                    // world.children.Add(teapotNode);
                    // world.children.Add(floorNode);
                    // world.Render(shader, worldToCamera * cameraToScreen, Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 1), wood);
                }

                // render quad
                target.Unbind();
                if (postproc != null)
                    quad.Render(postproc, target.GetTextureID());
            }
            else
            {
                // render scene directly to the screen
                if (shader != null && wood != null)
                {
                    teapot?.Render(shader, teapotObjectToWorld * worldToCamera * cameraToScreen, teapotObjectToWorld, wood);
                    floor?.Render(shader, floorObjectToWorld * worldToCamera * cameraToScreen, floorObjectToWorld, wood);
                }
            }
        }
    }
}