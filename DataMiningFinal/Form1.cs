using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.IO;
using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using ScatterPoint = OxyPlot.Series.ScatterPoint;
using ScatterSeries = OxyPlot.Series.ScatterSeries;


namespace DataMiningFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
            Console.Write("How many clusters do you want to create? ");
            int numClusters = int.Parse(Console.ReadLine());
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
            /*
            // Ask the user if they want to visualize the data
            Console.Write("Do you want to visualize the data? (y/n) ");
            string visualize = Console.ReadLine();

            if (visualize == "y")
            {
                // Ask the user which column to use for the x axis
                Console.Write("Which column do you want to use for the x axis? ");
                int xCol = int.Parse(Console.ReadLine());
                // Ask the user which column to use for the y axis
                Console.Write("Which column do you want to use for the y axis? ");
                int yCol = int.Parse(Console.ReadLine());

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
                var window = new PlotWindow { Model = plotModel };
                window.ShowDialog();
            }*/
        }

        // Normalizes the data in the specified columns
        public static double[][] Normalize(double[][] data, int[] columns)
        {
            double[][] normalizedData = data.MemberwiseClone();
            foreach (int col in columns)
            {
                double min = double.PositiveInfinity;
                double max = double.NegativeInfinity;
                for (int i = 0; i < data.Length; i++)
                {
                    min = Math.Min(min, data[i][col]);
                    max = Math.Max(max, data[i][col]);
                }
                for (int i = 0; i < data.Length; i++)
                {
                    normalizedData[i][col] = (data[i][col] - min) / (max - min);
                }
            }
            return normalizedData;
        }
    }
}

