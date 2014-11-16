using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoKeyPress {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
      this.KeyPreview = true;
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
    }
    private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
      Console.WriteLine(e.KeyCode.ToString());
    }
  }



}
