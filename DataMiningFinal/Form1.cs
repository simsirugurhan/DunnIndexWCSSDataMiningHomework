using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Accord.IO;
using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Statistics;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using ScatterPoint = OxyPlot.Series.ScatterPoint;
using ScatterSeries = OxyPlot.Series.ScatterSeries;
namespace DataMiningFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Load the data into a double[][] array
            string[][] stringArray = File.ReadAllLines("Final-data.csv")
                .Skip(1)
                .Select(x => x.Split(','))
                .ToArray();
            double[][] array = stringArray.Select(x => x.Select(y => double.Parse(y)).ToArray()).ToArray();

            // Normalize the data
            int[] columns = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            array = Normalize(array, columns);

            // Create the KMeans model

            int numClusters = int.Parse(textBox1.Text);
            KMeans kmeans = new KMeans(numClusters);

            // Fit the model to the data
            KMeansClusterCollection clusters = kmeans.Learn(array);

            // Calculate WCSS
            double wcss = 0;
            for (int i = 0; i < array.Length; i++)
            {
                wcss += Distance.SquareEuclidean(array[i], clusters.Centroids[clusters.Decide(array[i])]);
            }

            // Calculate BCSS
            double bcss = 0;
            double[][] centroids = clusters.Centroids;
            for (int i = 0; i < centroids.Length; i++)
            {
                for (int j = i + 1; j < centroids.Length; j++)
                {
                    bcss += Distance.Euclidean(centroids[i], centroids[j]);
                }
            }
            bcss /= numClusters * numClusters;

            // Calculate Dunn index
            double dunnIndex = bcss / wcss;

            // Save the results to a file
            /*
            
            if there is no sonuc.txt it will create.

            you check solutionfile/Debug/Bin/sonuc.txt
             
             */

            using (StreamWriter writer = new StreamWriter("sonuc.txt"))
            {
                // Add the cluster labels for each data point
                writer.WriteLine("\nCluster labels:");
                for (int i = 0; i < array.Length; i++)
                {
                    writer.WriteLine($"Record: {i}: Cluster: {clusters.Decide(array[i])}");
                }

                writer.WriteLine($"WCSS: {wcss}");
                writer.WriteLine($"BCSS: {bcss}");
                writer.WriteLine($"Dunn index: {dunnIndex}");
            }

            // Ask the user if they want to visualize the data
            string visualize = textBox2.Text;

            /*
             
             if you write y and true columns index, it will visualize the data.

            just click calculate button, after writing

             */
            
            if (visualize == "y")
            {
                // Ask the user which column to use for the x axis
                int xCol = int.Parse(textBox3.Text);
                // Ask the user which column to use for the y axis
                int yCol = int.Parse(textBox4.Text);
                // Visualize the data
                var plotModel = new PlotModel();
                var series = new ScatterSeries
                {
                    MarkerType = MarkerType.Circle
                };
                for (int i = 0; i < array.Length; i++)
                {
                    series.Points.Add(new ScatterPoint(array[i][xCol], array[i][yCol], 5, clusters.Decide(array[i])));
                }
                plotModel.Series.Add(series);
                //var window = new PlotWindow { Model = plotModel };
                //var window = new PlotView { Model = plotModel};
                //window.ShowDialog();
                var myForm = new Form2(plotModel);
                myForm.Show();
            }
        }
        
        // Normalize the data in the specified columns
        static double[][] Normalize(double[][] data, int[] columns)
        {
            double[][] result = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new double[data[i].Length];
                Array.Copy(data[i], result[i], data[i].Length);
            }

            for (int c = 0; c < columns.Length; c++)
            {
                double[] col = result.GetColumn(columns[c]);
                col = col.Standardize();
                result.SetColumn(columns[c], col);
            }

            return result;
        }
    }
}
