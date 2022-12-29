using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlotView = OxyPlot.WindowsForms.PlotView;

namespace DataMiningFinal
{
    public partial class Form2 : Form
    {

        public Form2(PlotModel model)
        {
            InitializeComponent();
            
            this.plotView1.Model = model;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
