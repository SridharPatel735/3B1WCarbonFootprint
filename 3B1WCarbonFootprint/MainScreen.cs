using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _3B1WCarbonFootprint
{
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            string textFile = @"C:\Users\vasud\source\repos\3B1WCarbonFootprint\3B1WCarbonFootprint\infoFile.txt";
            string[] lines = File.ReadAllLines(textFile);
            int requestSize = 0, responseSize = 0;
            for (int w = 0; w < lines.Length; w++)
            {
                if (lines[w].Contains("Request"))
                {
                    requestSize++;
                }
                if (lines[w].Contains("Response"))
                {
                    responseSize++;
                }
            }
            string[ , ] requestArray = new string[requestSize, 11];
            string[ , ] responseArray = new string[responseSize, 9];
            int k = 0, l = 0;
            int i = 0;
            while (i < lines.Length)
            {
                if (lines[i].Contains("Request"))
                {
                    i++;
                    for (int j = 0; j < 11; j++)
                    {
                        requestArray[k, j] = lines[i];
                        i++;
                    }
                    k++;
                }
                else if (lines[i].Contains("Response"))
                {
                    i++;
                    for (int j = 0; j < 9; j++)
                    {
                        responseArray[l, j] = lines[i];
                        i++;
                    }
                    l++;
                }
                else
                {
                    i++;
                }
            }

            string responseController = "", responseEndpoint = "", requestController = "", requestEndpoint = "", temp = "";

            List<Username> listOfNames = new List<Username>();

            for (int w = 0; w < responseSize; w++)
            {
                for (int s = 0; s < requestSize; s++)
                {
                    temp = responseArray[w, 1];
                    responseController = temp.Substring(18);
                    temp = responseArray[w, 7];
                    responseEndpoint = temp.Substring(24);

                    temp = requestArray[s, 2];
                    requestController = temp.Substring(12);
                    temp = requestArray[s, 5];
                    requestEndpoint = temp.Substring(10);

                    if ((responseController.Equals(requestController)) && (responseEndpoint.Equals(requestEndpoint)))
                    {
                        string address = "", username = "";
                        int meat = 0, grains = 0, dairy = 0, cell = 0, tv = 0, computer = 0, nyr = 0, car = 0, walk = 0, bus = 0;

                        temp = requestArray[s, 0];
                        address = temp.Substring(9);
                        
                        temp = requestArray[s, 1];
                        temp = temp.Substring(10);
                        string b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            meat = int.Parse(b);

                        temp = requestArray[s, 3];
                        temp = temp.Substring(8);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            grains = int.Parse(b);

                        temp = requestArray[s, 6];
                        temp = temp.Substring(7);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            dairy = int.Parse(b);

                        temp = requestArray[s, 7];
                        temp = temp.Substring(11);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            cell = int.Parse(b);
                        
                        temp = requestArray[s, 8];
                        temp = temp.Substring(4);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            tv = int.Parse(b);

                        temp = requestArray[s, 9];
                        temp = temp.Substring(10);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            computer = int.Parse(b);

                        temp = requestArray[s, 10];
                        temp = temp.Substring(21);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            nyr = int.Parse(b);

                        temp = responseArray[w, 0];
                        username = temp.Substring(18);

                        temp = responseArray[w, 2];
                        temp = temp.Substring(15);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            car = int.Parse(b);

                        temp = responseArray[w, 4];
                        temp = temp.Substring(18);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            walk = int.Parse(b);

                        temp = responseArray[w, 5];
                        temp = temp.Substring(18);
                        b = "";

                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            bus = int.Parse(b);

                        listOfNames.Add(new Username(meat, grains, dairy, cell, tv, computer, nyr, car, walk, bus, address, username));
                    }
                }
            }
            string output = "";
            for (int a = 0; a < listOfNames.Count(); a++)
            {
                output += listOfNames[a].username + " ";
                output += listOfNames[a].nyr + " ";
                output += listOfNames[a].yearly + " ";
                output += listOfNames[a].output;
                output += "\n";
            }
            outputLabel.Text = output;
        }
    }
}
