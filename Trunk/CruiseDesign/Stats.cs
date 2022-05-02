using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numeric;

namespace CruiseDesign
{
   class Stats
   {
      public int isTwoStage(string method)
      {
         // tree based methods - base 10
         // plot based methods   base 20
         // one stage methods    add 1
         // two stage methods    add 2
         // 100 percent          add 0
         switch (method)
         {
            case "100":
               return (10);
            case "STR":
            case "3P":
               return (11);
            case "S3P":
               return (12);
            case "PNT":
            case "FIX":
            case "FIXCNT":
               return (21);
            case "F3P":
            case "P3P":
            case "FCM":
            case "PCM":
            case "3PPNT":
               return (22);
         }
         return (0);
      }

      public double getCV(double x, double x2, int n)
      {
         double cv;
         // seg Std Deviation
         double var = (x2 - ((x * x) / n)) / (n - 1);
         double sd = Math.Sqrt(var);

         // get Mean
         double mean = x / n;

         // get CV
         cv = (sd / mean) * 100;

         return (cv);
      }
      public double getSampleError(double cv, int n)
      {
         double sampErr = 0;
         double tScore = getTScore(n);
         sampErr = Math.Round(((tScore * cv) / Math.Sqrt(Convert.ToDouble(n))),2);
         return (sampErr);
      }

      public double getTwoStageError(double cv1,double cv2, int n1, int n2)
      {
         double sampErr1 = getSampleError(cv1,n1);
         double sampErr2 = getSampleError(cv2, n2);

         double sampErr = Math.Round((Math.Sqrt(sampErr1 * sampErr1 + sampErr2 * sampErr2)),2);
         return (sampErr);
      }
      
      public double getTScore(int df)
      {
         double[] t = new double[] {12.706,4.303,3.182,2.776,2.571,2.447,2.365,2.306,2.262,2.228,
                                   2.201,2.179,2.160,2.145,2.131,2.120,2.110,2.101,2.093,2.086,
                                   2.080,2.074,2.069,2.064,2.060,2.056,2.052,2.048,2.045,2.042,2.00};

         if (df > 29)
            return (t[30]);
         else if (df < 2)
            return (0);
         else
            return (t[df-1]);

      }
   }
}
