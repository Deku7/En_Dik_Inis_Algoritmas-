using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static double x = 1;
        private static double y = 0;


        static double f(double x, double y)
        {
            return (x * x) + (y * y) - (2 * x * y);
        }
        static double f_gradX(double x, double y)
        {
            return (2 * x) - (2 * y);
        }
        static double f_gradY(double x, double y)
        {
            return (2 * y) - (2 * x);
        }

        static double[] g0(double x, double y)
        {
            double[] g0 = new double[2];
            g0[0] = f_gradX(x, y);
            g0[1] = f_gradY(x, y);
            return g0;
        }

        static double uzunluk(double[] g0)
        {
            double result = (g0[0] * g0[0]) + (g0[1] * g0[1]);
            return Math.Sqrt(result);
        }

        public static void yeni_nokta(double xx, double yy, double[] d0)
        {
            double a = 0.25;
            x = xx + a * d0[0];
            y = yy + a * d0[1];
        }

        static double beta_hesapla(double g0_uzunluk, double eski_g0_uzunluğu)
        {
            double result = g0_uzunluk / eski_g0_uzunluğu;
            return result * result;
        }


        public static void en_dik_inis()
        {
            int t = 0;
            double e = 0.01;
            double[] d0 = new double[2];
            while (true)
            {
                double[] g = g0(x, y);
                double g0_uzunluk = uzunluk(g);
                if (g0_uzunluk < e)
                    break;
                d0[0] = -g[0];
                d0[1] = -g[1];
                yeni_nokta(x, y, d0);
                t++;
            }
            Console.WriteLine(x + " " + y);
        }
        static void Main(string[] args)
        {
            en_dik_inis();
            eslenik_grad();
            Console.ReadLine();
        }
        static void eslenik_grad()
        {
            int t = 0;
            double e = 0.01;
            double[] d0 = new double[2];
            double[] g = g0(x, y);
            d0[0] = -g[0];
            d0[1] = -g[1];
            double g0_uzunluk = uzunluk(g);
            if (g0_uzunluk < e)
            {
                Console.WriteLine(x + " " + y);
                return;
            }
            yeni_nokta(x, y, d0);
            t += 1;
            double eski_g0_uzunluğu = g0_uzunluk;
            while (true)
            {
                g = g0(x, y);
                g0_uzunluk = uzunluk(g);
                if (g0_uzunluk < e)
                    break;
                double beta = beta_hesapla(g0_uzunluk, eski_g0_uzunluğu);
                d0[0] = beta * d0[0] - g[0];
                d0[1] = beta * d0[1] - g[1];
                yeni_nokta(x, y, d0);
                eski_g0_uzunluğu = g0_uzunluk;
                t++;

            }

            Console.WriteLine(x + " " + y);
        }
    }
}
