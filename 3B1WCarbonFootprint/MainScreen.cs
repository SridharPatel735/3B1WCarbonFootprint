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
            // reads file to scrape data later, saves each individual line to a string array
            string textFile = @"C:\Users\vasud\source\repos\3B1WCarbonFootprint\3B1WCarbonFootprint\infoFile.txt";
            string[] lines = File.ReadAllLines(textFile);

            // reads data to identify number of requests and responses
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

            // declares two 2d arrays to store all request and response data. 
            // The first index chooses the request block, and the second index specifies the line
            // Because all requests and responses are formatted the same, the line range is given
            string[ , ] requestArray = new string[requestSize, 11];
            string[ , ] responseArray = new string[responseSize, 9];
            int k = 0, l = 0;
            int i = 0;

            // loops through all data, sorts by request type (request or response), 
            // and adds them to their respective 2d array
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

            // initializes string variables for data parsing later
            string responseController = "", responseEndpoint = "", requestController = "", requestEndpoint = "", temp = "";

            // initalizes a list that will later be used to contain all names
            List<Username> listOfNames = new List<Username>();

            /* The outer loop iterates through each response block. For each response, the inner loop parses all requests, 
                checking for a matching endpoint and controller, two pieces of info that have a unique combination for each UES member 
                in the data. When the response and request have matching endpoints and controllers, the data is combined to form 
                the properties of a new username Object, which stores all of the relevant data across both request types. 
                This is done to all response-request pairs in the file. */
            for (int w = 0; w < responseSize; w++)
            {
                for (int s = 0; s < requestSize; s++)
                {
                    // these blocks filter out the extraneous text formatting on the Response Controllers and Endpoints
                    temp = responseArray[w, 1];
                    responseController = temp.Substring(18);
                    temp = responseArray[w, 7];
                    responseEndpoint = temp.Substring(24);

                    // these blocks filter out the extraneous text formatting on the Response Controllers and Endpoints
                    temp = requestArray[s, 2];
                    requestController = temp.Substring(12);
                    temp = requestArray[s, 5];
                    requestEndpoint = temp.Substring(10);

                    // If the newly formatted controllers and endpoints match, an object is created:
                    if ((responseController.Equals(requestController)) && (responseEndpoint.Equals(requestEndpoint)))
                    {
                        // initializes variables for all object properties (UES member statistics)
                        string address = "", username = "";
                        int meat = 0, grains = 0, dairy = 0, cell = 0, tv = 0, computer = 0, nyr = 0, car = 0, walk = 0, bus = 0;

                        // Parses UES member addresses
                        temp = requestArray[s, 0];
                        address = temp.Substring(9);

                        // Parses UES member amount of meat consumed from its line
                        temp = requestArray[s, 1];
                        temp = temp.Substring(10);
                        string b = "";

                        // This removes all non-integers from the meat quantity leaving only the number value of pounds of meat
                        for (int a = 0; a < temp.Length; a++)
                        {
                            // this creates a new string that holds all integers in the Meat consumed expression
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }
                        // turns the 'b' string into an integer value
                        if (b.Length > 0)
                            meat = int.Parse(b);

                        // Parses UES member amount of grain consumed from its line
                        temp = requestArray[s, 3];
                        temp = temp.Substring(8);
                        b = "";

                        // This removes all non-integers from the grain quantity leaving only the number value of pounds of grain
                        for (int a = 0; a < temp.Length; a++)
                        {
                            // this creates a new string that holds all integers in the Grains consumed expression
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        // turns the 'b' string into an integer value
                        if (b.Length > 0)
                            grains = int.Parse(b);

                        // Parses UES member amount of dairy consumed from its line
                        temp = requestArray[s, 6];
                        temp = temp.Substring(7);
                        b = "";

                        // This removes all non-integers from the dairy quantity leaving only the number value of pounds of dairy
                        for (int a = 0; a < temp.Length; a++)
                        {
                            // this creates a new string that holds all integers in the Dairy consumed expression 
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        // turns the 'b' string into an integer value
                        if (b.Length > 0)
                            dairy = int.Parse(b);

                        // Parses UES member amount of Cellphone use time from its line
                        temp = requestArray[s, 7];
                        temp = temp.Substring(11);
                        b = "";

                        // The next lines create a new variable with all the integer values from the Cellphone use line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            cell = int.Parse(b);

                        // Parses UES member amount of TV use time from its line
                        temp = requestArray[s, 8];
                        temp = temp.Substring(4);
                        b = "";

                        // The next lines create a new variable with all the integer values from the Television use line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            tv = int.Parse(b);

                        // Parses UES member amount of Computer use time from its line
                        temp = requestArray[s, 9];
                        temp = temp.Substring(10);
                        b = "";

                        // The next lines create a new variable with all the integer values from the PC use line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            computer = int.Parse(b);

                        // Parses UES member amount New Year's Resolution for reduced emissions
                        temp = requestArray[s, 10];
                        temp = temp.Substring(21);
                        b = "";

                        // The next lines create a new variable with all the integer values from the New Years resolution line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            nyr = int.Parse(b);

                        // Parses UES member names
                        temp = responseArray[w, 0];
                        username = temp.Substring(18);

                        // Parses UES member amount of Car use time from its line
                        temp = responseArray[w, 2];
                        temp = temp.Substring(15);
                        b = "";

                        // The next lines create a new variable with all the Car use time from the PC use line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            car = int.Parse(b);

                        // Parses UES member amount of Walking time from its line
                        temp = responseArray[w, 4];
                        temp = temp.Substring(18);
                        b = "";

                        // The next lines create a new variable with all the time Walked from the PC use line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            walk = int.Parse(b);

                        // Parses UES member amount of Public Transport time from its line
                        temp = responseArray[w, 5];
                        temp = temp.Substring(18);
                        b = "";

                        // The next lines create a new variable with all the Walking time from the PC use line
                        // They are put into a string that is then parsed into an integer for later use
                        for (int a = 0; a < temp.Length; a++)
                        {
                            if (Char.IsDigit(temp[a]))
                                b += temp[a];
                        }

                        if (b.Length > 0)
                            bus = int.Parse(b);

                        // For each set of responses and requests matched, one Username object is created, 
                        // which holds all parsed values and is stored in the listOfNames list for use in a loop
                        listOfNames.Add(new Username(meat, grains, dairy, cell, tv, computer, nyr, car, walk, bus, address, username));
                    }
                }
            }

            //Creating each column varible to hold data for each person
            string col1 = "Username:\n\n\n", col2 = "Address:\n\n\n", col3 = "Red Meat:\n\n\n", col4 = "Grains:\n\n\n", col5 = "Dairy:\n\n\n", col6 = "Cellphone:\n\n\n", col7 = "T.V.:\n\n\n", col8 = "Computer:\n\n\n", col9 = "Car:\n\n\n", col10 = "Walking:\n\n\n", col11 = "Public Transport:\n\n";

            //For loop to iterate over all of the UES members
            for (int a = 0; a < listOfNames.Count(); a++)
            {
                //Adding the proper value to the correct column
                col1 += listOfNames[a].username + "\n";
                col2 += listOfNames[a].address + "\n";
                col3 += listOfNames[a].meat + "\n";
                col4 += listOfNames[a].grains + "\n";
                col5 += listOfNames[a].dairy + "\n";
                col6 += listOfNames[a].cell + "\n";
                col7 += listOfNames[a].tv + "\n";
                col8 += listOfNames[a].computer + "\n";
                col9 += listOfNames[a].car + "\n";
                col10 += listOfNames[a].walk + "\n";
                col11 += listOfNames[a].bus + "\n";
            }
            
            //Outputting the strings into the columns for the UI
            column1.Text = col1;
            column2.Text = col2;
            column3.Text = col3;
            column4.Text = col4;
            column5.Text = col5;
            column6.Text = col6;
            column7.Text = col7;
            column8.Text = col8;
            column9.Text = col9;
            column10.Text = col10;
            column11.Text = col11;

            //Outputting the current and projected values onto the chart
            for (int a = 0; a < listOfNames.Count(); a++)
            {
                chart.Series["Current"].Points.AddXY(listOfNames[a].username, listOfNames[a].yearly);
                chart.Series["Projection"].Points.AddXY(listOfNames[a].username, listOfNames[a].output);
            }
        }
    }
}