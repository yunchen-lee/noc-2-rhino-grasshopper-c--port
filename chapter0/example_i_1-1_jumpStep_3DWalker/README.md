# 3D Random Walker in Grasshopper C#

[![Pasted 2025-03-29-16-46-53.png](https://github.com/yunchen-lee/noc-2-rhino-grasshopper-csharp-port/tree/main/chapter0/example_i_1-1_jumpStep_3DWalker/3D%20Random%20Walker%20in%20Grasshopper%20C%23-assets/Pasted%202025-03-29-16-46-53.png)
*Some paths created by my 3D robot ant.*

Imagine a little robot ant that move randomly in space — that’s what a random walker is! In this post, I’ll introduce how to build a random walker in Rhino and Grasshopper using C#, and also explore different versions of random walkers and how they behave.

---

This series is titled “The Nature of Code: Porting from p5.js to Rhino+Grasshopper.” Check out the source code here 👉 [noc-2-rhino-grasshopper-csharp-port](https://github.com/yunchen-lee/noc-2-rhino-grasshopper-csharp-port) and follow along as we dive into this exciting journey of creative coding!

---

### 2D Random Walker

![Pasted 2025-03-29-16-46-53 1.png](./3D%20Random%20Walker%20in%20Grasshopper%20C#-assets/Pasted%202025-03-29-16-46-53%201.png)

*Traditional 2D Random Walker in Grasshopper.*

I started with the traditional 2D random walker, ported from p5.js to C#. In Grasshopper, we write our scroipt in a C# component, and by triggering the component at a fixed time interval, we can see the walker’s path evolve over time (similar to the `draw()` function in p5.js).

![Pasted 2025-03-29-16-46-53 2.png](./3D%20Random%20Walker%20in%20Grasshopper%20C#-assets/Pasted%202025-03-29-16-46-53%202.png)

*Activate the random walker with a trigger.*

To draw the path, every step of the walker is stored as a point (using Rhino’s `Point3d` object). This gives us a continuous, organic line path that mimics a natural, wandering path.

```
/*
 * Code example for The Nature of Code (2024 Edition)
 * Daniel Shiffman
 * (https://natureofcode.com/)
 
 * Ported from p5.js by Yun-Chen Lee yclee@arch.nycu.edu.tw
 * (https://github.com/yunchen-lee/noc-2-rhino-grasshopper-csharp-port)
 */

// Grasshopper Script Instance
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using Rhino;
using Rhino.Geometry;

using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

public class Script_Instance : GH_ScriptInstance
{
  private void RunScript(bool reset, ref object a)
  {
    // Write your logic here
    if(reset){
        walker = new Walker();
    }
    else{
        walker.step();
        a = walker.pts_list;
    }
  }

  Walker walker;
  
  class Walker{
    public int px;
    public int py;
    public List<Point3d> pts_list;
    Random rnd;

    public Walker(){
        px = 0;
        py = 0;
        pts_list = new List<Point3d>();
        rnd = new Random();
    }

    public void step(){
        int choice = (int) Math.Floor(rnd.NextDouble()*4);
        if (choice == 0) px++;
        else if(choice == 1) px--;
        else if(choice == 2) py++;
        else py--;

        pts_list.Add(new Point3d(px, py, 0));
    }
  }
}
```

### A Little Adjustment: Jump-Step Walker

![Pasted 2025-03-29-16-46-53 3.png](./3D%20Random%20Walker%20in%20Grasshopper%20C#-assets/Pasted%202025-03-29-16-46-53%203.png)

A traditional 4-Step Walker creates a continuous path. Now, let’s experiment with a different rule in the `step()` function to generate unpredictable patterns. 

In this example, I implemented a Jump-Step Walker with eight possible steps. The jump steps produce a more dynamic, leap-like digital footprint. 

![Pasted 2025-03-29-16-46-53 4.png](./3D%20Random%20Walker%20in%20Grasshopper%20C#-assets/Pasted%202025-03-29-16-46-53%204.png)

Here’s a snippet of the jump-step logic:

```
public void step(){
      int choice = (int) Math.Floor(rnd.NextDouble()*8);
      if (choice == 0) px++;
      else if(choice == 1) px--;
      else if(choice == 2) py++;
      else if(choice == 3) py--;
      else if(choice == 4) px-=2;
      else if(choice == 5) px+=2;
      else if(choice == 6) py-=2;
      else py+=2;

      pts_list.Add(new Point3d(px, py, 0));
  }
```

### 3D Random Walker

To take the concept further, the `Walker` object can be extended into 3D by adding a new property for the z-coordinate (`pz`). Now, the walker is no longer limited to a flate plane — it can move up and down as well as side to side. This results in cool, three-dimensional paths in space.

![Pasted 2025-03-29-16-46-53.gif](./3D%20Random%20Walker%20in%20Grasshopper%20C#-assets/Pasted%202025-03-29-16-46-53.gif)

*3D Random Walker*

Here’s how the 3D walker works:

```
class Walker{
    public int px;
    public int py;
    public int pz;
    public List<Point3d> pts_list;
    Random rnd;

    public Walker(){
        px = 0;
        py = 0;
        pz = 0;
        pts_list = new List<Point3d>();
        rnd = new Random();
    }

    public void step(){
        int choice = (int) Math.Floor(rnd.NextDouble()*6);
        if (choice == 0) px++;
        else if(choice == 1) px--;
        else if(choice == 2) py++;
        else if(choice == 3) py--;
        else if(choice == 4) pz++;
        else pz--;

        pts_list.Add(new Point3d(px, py, pz));
    }
```

## What’s the Next Step?

- **Mimic Animal Tracks:** Try to simulate the paths of different animals for more organic patterns.

- **Unique Footprints:** Experiment with custom rules to generate one-of-a-kind footprints.

![Pasted 2025-03-29-16-57-23.png](./3D%20Random%20Walker%20in%20Grasshopper%20C#-assets/Pasted%202025-03-29-16-57-23.png)

*Sacred Space, Leonardo Ulian (2013)*

## Useful References

- hino Common API: <https://developer.rhino3d.com/api/rhinocommon/>

- The Nature of Code 2: <https://natureofcode.com/>

- Sacred Space, Leonardo Ulian (2013). [++https://www.journal-du-design.fr/art/sacred-space-leonardo-ulian-35252/++](https://www.journal-du-design.fr/art/sacred-space-leonardo-ulian-35252/)
