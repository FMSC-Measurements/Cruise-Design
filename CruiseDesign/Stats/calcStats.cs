using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CruiseDesign
{
   class calcStats
   {
      public long sampleSize1;
      public long sampleSize2;
      public bool isFreq = false;
      public bool isKz = false;

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
               isFreq = true;
               return (11);
            case "3P":
               isKz = true;
               return (11);
            case "S3P":
               isFreq = true;
               isKz = true;
               return (12);
            case "PNT":
            case "FIX":
            case "FIXCNT":
               return (21);
            case "F3P":
               isKz = true;
               return (22);
            case "P3P":
               isKz = true;
               return (22);
            case "FCM":
               isFreq = true;
               return (22);
            case "PCM":
               isFreq = true;
               return (22);
            case "3PPNT":
               isKz = true;
               return (22);
         }
         return (0);
      }

      public double getCV(double x, double x2, int n)
      {
         double cv;
         // seg Std Deviation
         if (n < 2 || x <= 0)
            return (0);

         double var = (x2 - ((x * x) / n)) / (n - 1);
         double sd = Math.Sqrt(var);

         // get Mean
         double mean = x / n;

         // get CV
         cv = (sd / mean) * 100;

         return (cv);
      }
      public double getPntCV(double vBars, double vBars2, double pntFac, int plotCount)
      {
         double cv;
         // seg Std Deviation
         if (plotCount < 2 || vBars <= 0)
            return (0);

         double var = (vBars2 - ((vBars * vBars) / plotCount)) / (plotCount - 1);
         double sd = Math.Sqrt(var * (pntFac * pntFac));

         // get Mean
         double mean = (vBars*pntFac) / plotCount;

         // get CV
         cv = (sd / mean) * 100;

         return (cv);
      
      }

      public double getSampleError(double cv, long n, double tScore)
      {
         double sampErr = 0;
         if (n > 0)
         {
            if(tScore <= 0)
               tScore = getTScore(n);
            sampErr = Math.Round(((tScore * cv) / Math.Sqrt(Convert.ToDouble(n))), 2);
         }
         return (sampErr);
      }
      
      //*************************************************************
      public double getSampleError(double cv, long n, double tScore, long N)
      {
         double sampErr = 0;
         if (n > 0 && n <= N)
         {
            if (tScore <= 0)
               tScore = getTScore(n);

            //sampErr = Math.Round(Math.Sqrt(((tScore*tScore * cv*cv)/Convert.ToDouble(n)) - ((tScore*tScore * cv*cv)/Convert.ToDouble(N))), 2);
            double sampErr1 = ((tScore * cv) / (Math.Sqrt(Convert.ToDouble(n))));
            double sampErr2 = Math.Sqrt((1- (Convert.ToDouble(n)/Convert.ToDouble(N))));
            sampErr = Math.Round(sampErr1 * sampErr2, 2);
         }
         return (sampErr);
      }

      public double getTwoStageError(double cv1,double cv2, long n1, long n2)
      {
         double sampErr1 = getSampleError(cv1,n1,0);
         double sampErr2 = getSampleError(cv2, n2, 0);

         double sampErr = Math.Round((Math.Sqrt(sampErr1 * sampErr1 + sampErr2 * sampErr2)),2);
         return (sampErr);
      }
      
      public double getTScore(long samp)
      {
         double[] t = new double[] {12.706,4.303,3.182,2.776,2.571,2.447,2.365,2.306,2.262,2.228,
                                   2.201,2.179,2.160,2.145,2.131,2.120,2.110,2.101,2.093,2.086,
                                   2.080,2.074,2.069,2.064,2.060,2.056,2.052,2.048,2.045,2.042,2.00};

         if (samp > 29)
            return (t[30]);
         else if (samp < 2)
            return (0);
         else
            return (t[samp-2]);

      }

      public double getStratumStats()
      {
         return (0);
      }

      public double getSaleError()
      {
         return (0);
      }

      public int getSampleSize(double Error, double CV)
      {
         if (Error == 0)
            return (3);
         double nCalc = (4 * (CV * CV)) / (Error * Error);
         int n = (int)Math.Ceiling(nCalc);
         if (n < 3) n = 3;
         return (n);
      }

      //**********************************
      public int getSampleSize(double Error, double CV, long N)
      {
         if (Error == 0 || CV <= 0)
            return (3);
         
         double nCalc = (4 * (CV * CV)) / (Error * Error + ((4 * (CV * CV))/N));

         int n = (int)Math.Ceiling(nCalc);

         if (n < 3) n = 3;
         
         return (n);
      }
      
      public void getTwoStageSampleSize(double CV, double CV2, double Error) 
      {
         if (Error > 1 || CV <= 0)
         {
            double size2 = Math.Ceiling((4 * (CV * CV2 + CV2 * CV2)) / (Error * Error));
            if (size2 < 3) size2 = 3;

            double size = Math.Ceiling((CV / CV2) * size2);
            if (size < 3) size = 3;

            sampleSize1 = (int)size;
            sampleSize2 = (int)size2;
         }
         else
         {
            sampleSize1 = 3;
            sampleSize2 = 3; ;
         }
      
      }

      public long checkTValueError (double CV, long calcSamp, double Error)
      {
         double calcError;
         // calculate error using CV and calcSamp 
         int add1 = 0;
         int cnt = 0;
         do
         {
            calcSamp += add1;
            calcError = getSampleError(CV, calcSamp, 0);
            cnt++;
            if (cnt == 1)
               add1 = 1;
            // compare to Error
         } while (calcError > Error && cnt < 10);

         return (calcSamp);
         // return sample size
      }

      public long checkTValueError(double CV, long calcSamp, double Error, long N)
      {
         double calcError;
         // calculate error using CV and calcSamp 
         int add1 = 0;
         int cnt = 0;
         do
         {
            calcSamp += add1;
            calcError = getSampleError(CV, calcSamp, 0, N);
            cnt++;
            if (cnt == 1)
               add1++;
            // compare to Error
         } while (calcError > Error && cnt < 10);

         return (calcSamp);
         // return sample size
      }

      public long checkTValueError2Stage(double CV1, double CV2, long n1, long n2, double Error)
      {
         double calcError;
         int add1 = 0;
         int cnt = 0;
         do
         {
            n1 += add1;
            calcError = getTwoStageError(CV1, CV2, n1, n2);
            cnt++;
            if (cnt == 1)
               add1++;
         } while (calcError > Error && cnt < 10);

         return (n1);

      }
   }
}
