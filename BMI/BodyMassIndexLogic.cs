using System;

namespace BMI
{
    public class BodyMassIndexLogic
    {
        public event Action<string> ShowResult;
        public event Action ShowFormatError;

        public BodyMassIndexLogic()
        {
        }

        private double CalculateBodyMassIndex(int size, int weight)
        {
            double sizeMeter = size / 100d;
            return Math.Round(weight / sizeMeter / sizeMeter, 1);
        }

        private String DetermineCategory(double bmi)
        {
            if (bmi < 13)
            {
                return "hochgradiges Untergewicht Grad II";
            }
            else if ((bmi >= 13) && (bmi < 16))
            {
                return "hochgradiges Untergewicht Grad I";
            }
            else if ((bmi >= 16) && (bmi < 17))
            {
                return "mäßiges Untergewicht";
            }
            else if ((bmi >= 17) && (bmi < 18.5))
            {
                return "leichtes Untergewicht";
            }
            else if ((bmi >= 18.5) && (bmi < 25))
            {
                return "Normalgewicht";
            }
            else if ((bmi >= 25) && (bmi < 30))
            {
                return "Präadipositas";
            }
            else if ((bmi >= 30) && (bmi < 35))
            {
                return "Adipositas Grad I";
            }
            else if ((bmi >= 35) && (bmi < 40))
            {
                return "Adipositas Grad II";
            }
            else
            {
                return "Adipositas Grad III";
            }
        }

        public void EvaluateData(int size, int weight)
        {
            if ((size <= 0) || (weight <= 0))
            {
                ShowFormatError();
            }
            else
            {
                double bmi = CalculateBodyMassIndex(size, weight);
                string result = $"Dein BMI ist {bmi} ({DetermineCategory(bmi)})";

                if ((bmi < 18.5) || (bmi >= 25))
                {
                    double goodBmi;
                    int goodWeight = weight;

                    // Underweight
                    if (bmi < 18.5)
                    {
                        do
                        {
                            goodWeight++;
                            goodBmi = CalculateBodyMassIndex(size, goodWeight);
                        } while (goodBmi < 18.5);
                    }

                    // Overweight
                    if (bmi >= 25)
                    {
                        do
                        {
                            goodWeight--;
                            goodBmi = CalculateBodyMassIndex(size, goodWeight);
                        } while (goodBmi >= 25);
                    }

                    result = $"{result}\nNormalgewicht beginnt bei {goodWeight} kg";
                }
                ShowResult(result);
            }
        }
    }
}
