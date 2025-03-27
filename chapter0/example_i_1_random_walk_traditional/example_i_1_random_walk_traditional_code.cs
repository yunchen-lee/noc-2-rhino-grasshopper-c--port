
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
  /* 
    Members:
      RhinoDoc RhinoDocument
      GH_Document GrasshopperDocument
      IGH_Component Component
      int Iteration

    Methods (Virtual & overridable):
      Print(string text)
      Print(string format, params object[] args)
      Reflect(object obj)
      Reflect(object obj, string method_name)
  */
  
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
