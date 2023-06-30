Hugo Krul:      8681929
Koen Vermeulen: 4729382

Camera:
- The camera must be interactive with keyboard and/or mouse control. It must at least support translation and rotation.
    =>  We choose to use only keyboard control. Using WASD you can (respectivly) move front, left, back, right.
        Using Space and Shift you can move up and down. And using the left arrow and right arrow you can look to the right and to the left.
        
        We did this by translating the Matrix4 worldToCamera by a Vector3 which is defined als cameraPosition.
        We then multiply this Matrix4 with a rotation Matrix4 around the y-axis (Vector3(0, 1, 0)).
        Until now you can still only move at the original XYZ axis. If you look to the left and press W, you don't move in that direction.
        We solved this using a variable called cameraDirection. If we press W, we add the X and Z value of cameraDirection to the cameraPosition.
        If you press left, this cameraDirection will be rotated to the left, and if you press right it will be rotated to the right. 
        You rotate at the same speed for the cameraDirection as for the rotation matrix.

        We also implemented the upDirection and rightDirection in case we wanted to implement looking up and down.

Scene graph:
- Your demo must show a hierarchy of objects. The scene graph must be able to hold any number of meshes, and may not put any restrictions on the maximum depth of the scene graph
    =>  We started with a SceneGraph called world. That SceneGraph will be the parent of every Node. The SceneGraph has a list of nodes called children and if the Render Method is called,
        it loops through all it's children and calling the render functions of those children. In the render function of the nodes, it checks if that node has any children,
        if it does it calls that render method creating a recursive loop. It stops when the last node doesn't have any more children.

        The position of the parent gets multiplied by the relative position of the child. That get's multiplied by the worldToCamera and that gets multiplied by cameraToScreen.
        This way you have the relavitivePositionToScreen.

        This way you only have to specify the relative position from the parent to the child so if the parent moves, the child moves too.

Shaders:
- You must provide at least one correct shader that implements the Phong shading model. This includes ambient light, diffuse reflection and glossy reflection of the point lights in the scene. To pass, you may use a single hardcoded light.
    =>  

Demonstration scene:
- All engine functionality you implement must be visible in the demo. A high quality demo will increase your grade.
    =>  If you start the application, you can see the entire scene and move around using the controls.
        You see a big guy rotating with the teapot in it's hand. the teapot has a little teapot beneath it. So the parent of the little teapot is the big teapot. The parent of the big teapot is the guy. The parent of the guy is the floor and the parent of the floor is the world.
        You can also see we implemented the light here.

Documentation:
- Describe which features you implemented. Describe the controls for your demo.
    =>  Using WASD you can move around the scene.
        Using Space and Shift you can move up and down.
        Using the right arrow and left arrow you can look to the left and to the right.

        

