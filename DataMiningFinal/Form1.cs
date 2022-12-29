using System;
using System.Collections.Generic;
using System.Globalization;
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

            double[][] array = LoadData("Final-data.csv", 214, 9, ',');

            // Normalize the data
            array = Normalize(array);

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
                int[] sumOfClusters = new int[numClusters];

                writer.WriteLine("\nCluster labels:");
                for (int i = 0; i < array.Length; i++)
                {
                    writer.WriteLine($"Record: {i}: Cluster: {clusters.Decide(array[i])}");
                    for (int k = 0; k < numClusters; k++)
                    {
                        if (clusters.Decide(array[i]) == k)
                        {
                            sumOfClusters[k]++;
                        }
                    }
                }

                writer.WriteLine($"WCSS: {wcss.ToString("F" + 5)}");
                writer.WriteLine($"BCSS: {bcss.ToString("F" + 5)}");
                writer.WriteLine($"Dunn index: {dunnIndex.ToString("F" + 5)}");

                for (int k = 0; k < numClusters; k++)
                {
                    writer.WriteLine($"Küme {k}: {sumOfClusters[k]}");
                }

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
        static double[][] Normalize(double[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j) // each col
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }

        static double[][] MatrixDouble(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        static double[][] LoadData(string fn, int rows, int cols, char delimit)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            double[][] result = MatrixDouble(rows, cols);
            FileStream ifs = new FileStream(fn, FileMode.Open);
            StreamReader sr = new StreamReader(ifs);
            string[] tokens = null;
            string line = null;
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                tokens = line.Split(delimit);
                for (int j = 0; j < cols; ++j)
                    result[i][j] = Convert.ToDouble(tokens[j], provider);
                ++i;
            }
            sr.Close(); ifs.Close();
            return result;
        }
    }
}
