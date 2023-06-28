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
    =>  

Shaders:
- You must provide at least one correct shader that implements the Phong shading model. This includes ambient light, diffuse reflection and glossy reflection of the point lights in the scene. To pass, you may use a single hardcoded light.
    =>  

Demonstration scene:
- All engine functionality you implement must be visible in the demo. A high quality demo will increase your grade.
    =>  If you start the application, you can see the entire scene and move around using the controls.


Documentation:
- Describe which features you implemented. Describe the controls for your demo.
    =>  Using WASD you can move around the scene.
        Using Space and Shift you can move up and down.
        Using the right arrow and left arrow you can look to the left and to the right.

        

