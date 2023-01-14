using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3B1WCarbonFootprint
{
    class Username
    {
        public int meat, grains, dairy, cell, tv, computer, nyr, car, walk, bus;
        public string address, username;
        public double output, yearly;

        public Username(int meat, int grains, int dairy, int cell, int tv, int computer, int nyr, int car, int walk, int bus, string address, string username)
        {
            this.meat = meat;
            this.grains = grains;
            this.dairy = dairy;
            this.cell = cell;
            this.tv = tv;
            this.computer = computer;
            this.nyr = nyr;
            this.car = car;
            this.walk = walk;
            this.bus = bus;
            this.address = address;
            this.username = username;
            calcEmissions(this.bus, this.car, this.meat, this.dairy, this.grains, this.cell, this.computer, this.tv, this.nyr);
        }

        public double calcEmissions(int busHrs, int carHrs, int meatLbs, int dairyLbs, int grainLbs, int cellHrs, int pcHrs, int tvHrs, int nyr)
        {
            double[] emissionTypes = new double[9];
            double yearlyEmissions = 0;

            for (int i = 0; i < emissionTypes.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        emissionTypes[i] = busHrs * 4.3;
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 1:
                        emissionTypes[i] = (carHrs * 6.5);
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 2:
                        emissionTypes[i] = 0;
                        break;
                    case 3:
                        emissionTypes[i] = (meatLbs * 0.454 * 8.0);
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 4:
                        emissionTypes[i] = (dairyLbs * 0.454 * 6.3);
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 5:
                        emissionTypes[i] = (grainLbs * 0.454 * 3.7);
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 6:
                        emissionTypes[i] = (cellHrs * 3.6);
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 7:
                        emissionTypes[i] = (pcHrs * 4.2);
                        yearlyEmissions += emissionTypes[i];
                        break;
                    case 8:
                        emissionTypes[i] = (tvHrs * 6.8);
                        yearlyEmissions += emissionTypes[i];
                        break;
                }
            }
            yearly = yearlyEmissions;
            output = yearlyEmissions * ((1 - 0.01 * nyr));
            return output;
        }
    }
}
