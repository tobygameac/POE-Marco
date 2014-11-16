using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using System.Diagnostics;

namespace AutoKeyPress {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      Process p = Process.GetProcessesByName("PathOfExile").FirstOrDefault();
      IntPtr pointer = p.MainWindowHandle;
      Key hotkey = Key.OemTilde;
      while (pointer != null) {
        if (Keyboard.IsKeyDown(hotkey)) {
          Console.WriteLine(hotkey);
          SetForegroundWindow(pointer);
          SendKeys.SendWait("\n/oos\n");
        }
        Thread.Sleep(100);
      }
      //sendKeystroke("[No Name] - GVIM", Keys.N, 0);
      //Application.Run(new Form1());
    }


    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    public static extern IntPtr SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point p);
    [DllImport("user32.dll")]
    static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    [Flags]
    public enum MouseEventFlags {
      LEFTDOWN = 0x00000002,
      LEFTUP = 0x00000004,
      MIDDLEDOWN = 0x00000020,
      MIDDLEUP = 0x00000040,
      MOVE = 0x00000001,
      ABSOLUTE = 0x00008000,
      RIGHTDOWN = 0x00000008,
      RIGHTUP = 0x00000010
    }

    public static void LeftClick(int x, int y) {
      Point currentMousePoint;
      GetCursorPos(out currentMousePoint);
      SetCursorPos(x, y);
      mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
      mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
      SetCursorPos(currentMousePoint.X, currentMousePoint.Y);
    }

    public static void RightClick(int x, int y) {
      Point currentMousePoint;
      GetCursorPos(out currentMousePoint);
      SetCursorPos(x, y);
      mouse_event((int)(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0);
      mouse_event((int)(MouseEventFlags.RIGHTUP), 0, 0, 0, 0);
      SetCursorPos(currentMousePoint.X, currentMousePoint.Y);
    }

    public static void sendKeystroke(String windowName, Keys k, int times) {
      const uint WM_KEYDOWN = 0x100;
      const uint WM_SYSCOMMAND = 0x018;
      const uint SC_CLOSE = 0x053;

      IntPtr WindowToFind = FindWindow(null, windowName);
      if (WindowToFind == IntPtr.Zero) {
        Console.WriteLine("Window " + windowName + " not found.");
      } else {
        Console.WriteLine("Send " + times + " times of " + k + " to " + windowName);
        SetForegroundWindow(WindowToFind);
        LeftClick(500, 500);
        for (int i = 0; i < times; i++) {
          PostMessage(WindowToFind, WM_KEYDOWN, ((IntPtr)k), IntPtr.Zero);
        }
      }
    }
  }
}
