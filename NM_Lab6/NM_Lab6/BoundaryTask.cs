using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class BoundaryTask
{
    public double alpha1;
    public double beta1;
    public double gamma1;
    public double alpha2;
    public double beta2;
    public double gamma2;
    public double a;
    public double b;
    public int pointsCount;
    public double[] nodes_X;
    public double[] nodes_U;
    public double[] nodes_V;
    public double[] nodes_W;
    public double[] nodes_alpha;
    public double[] nodes_beta;
    public double[] nodes_gamma;
    public double[] Y;
    public double[] YY;

    public BoundaryTask(string path)
    {
        using (StreamReader reader = new StreamReader("E:\\Лабы\\ЧМ\\NM_Lab6\\NM_Lab6\\" + path))
        {
            string[] line1 = reader.ReadLine().Split(' ');
            alpha1 = double.Parse(line1[0]);
            beta1 = double.Parse(line1[1]);
            gamma1 = double.Parse(line1[2]);
            alpha2 = double.Parse(line1[3]);
            beta2 = double.Parse(line1[4]);
            gamma2 = double.Parse(line1[5]);

            string[] line2 = reader.ReadLine().Split(' ');
            a = double.Parse(line2[0]);
            b = double.Parse(line2[1]);
            pointsCount = int.Parse(line2[2]) + 2;

            nodes_X = new double[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                if (i == 0)
                {
                    nodes_X[i] = a;
                }
                else if (i == pointsCount - 1)
                {
                    nodes_X[i] = b;
                }
                else
                {
                    string[] line3 = reader.ReadLine().Split(' ');
                    nodes_X[i] = double.Parse(line3[1]);
                }
            }
            nodes_U = new double[pointsCount];
            nodes_V = new double[pointsCount];
            nodes_W = new double[pointsCount];
            nodes_alpha = new double[pointsCount];
            nodes_beta = new double[pointsCount];
            nodes_gamma = new double[pointsCount];
            Y = new double[pointsCount];
            YY = new double[pointsCount];
        }
    }

    public void calculateUVW()
    {
        double u0 = alpha1;
        double v0 = -beta1;
        double w0 = gamma1;
        double u1, v1, w1;
        double k1_u, k2_u, k3_u, k4_u;
        double k1_v, k2_v, k3_v, k4_v;
        double k1_w, k2_w, k3_w, k4_w;
        double x0 = a;
        double h;

        for (int i = 0; i < pointsCount; i++)
        {
            if (i == 0)
            {
                nodes_U[i] = alpha1;
                nodes_V[i] = -beta1;
                nodes_W[i] = gamma1;
            }
            else
            {
                if (i == pointsCount - 1)
                {
                    h = b - x0;
                }
                else
                {
                    h = nodes_X[i] - x0;
                }

                k1_u = h * Px0(x0, u0, v0);
                k2_u = h * Px0(x0 + (double)1 / 3 * h, u0 + (double)1 / 3 * k1_u, v0 + (double)1 / 3 * k1_u);
                k3_u = h * Px0(x0 + (double)2 / 3 * h, u0 - (double)1 / 3 * k1_u + k2_u, v0 - (double)1 / 3 * k1_u + k2_u);
                k4_u = h * Px0(x0 + h, u0 + k1_u - k2_u + k3_u, v0 + k1_u - k2_u + k3_u);
                u1 = u0 + (double)1 / 8 * (k1_u + 3 * k2_u + 3 * k3_u + k4_u);

                k1_v = h * Qx0(x0, u0);
                k2_v = h * Qx0(x0 + (double)1 / 3 * h, u0 + (double)1 / 3 * k1_v);
                k3_v = h * Qx0(x0 + (double)2 / 3 * h, u0 - (double)1 / 3 * k1_v + k2_v);
                k4_v = h * Qx0(x0 + h, u0 + k1_v - k2_v + k3_v);
                v1 = v0 + (double)1 / 8 * (k1_v + 3 * k2_v + 3 * k3_v + k4_v);

                k1_w = h * Fx0(x0, u0);
                k2_w = h * Fx0(x0 + (double)1 / 3 * h, u0 + (double)1 / 3 * k1_w);
                k3_w = h * Fx0(x0 + (double)2 / 3 * h, u0 - (double)1 / 3 * k1_w + k2_w);
                k4_w = h * Fx0(x0 + h, u0 + k1_w - k2_w + k3_w);
                w1 = w0 + (double)1 / 8 * (k1_w + 3 * k2_w + 3 * k3_w + k4_w);

                nodes_U[i] = u1;
                nodes_V[i] = v1;
                nodes_W[i] = w1;
                u0 = u1;
                v0 = v1;
                w0 = w1;
                if (i != pointsCount)
                {
                    x0 = nodes_X[i];
                }
            }
        }
    }

    public void calculateABG()
    {
        double a0 = alpha2;
        double b0 = -beta2;
        double g0 = gamma2;
        double a1, b1, g1;
        double k1_a, k2_a, k3_a, k4_a;
        double k1_b, k2_b, k3_b, k4_b;
        double k1_g, k2_g, k3_g, k4_g;
        double x0 = b;
        double h;

        for (int i = pointsCount - 1; i >= 0; i--)
        {
            if (i == pointsCount - 1)
            {
                nodes_alpha[i] = alpha2;
                nodes_beta[i] = -beta2;
                nodes_gamma[i] = gamma2;
            }
            else
            {
                if (i == 0)
                {
                    h = x0 - a;
                }
                else
                {
                    h = x0 - nodes_X[i];
                }

                k1_a = h * Px0(x0, a0, b0);
                k2_a = h * Px0(x0 - (double)1 / 3 * h, a0 - (double)1 / 3 * k1_a, b0 - (double)1 / 3 * k1_a);
                k3_a = h * Px0(x0 - (double)2 / 3 * h, a0 + (double)1 / 3 * k1_a - k2_a, b0 + (double)1 / 3 * k1_a - k2_a);
                k4_a = h * Px0(x0 - h, a0 - k1_a + k2_a - k3_a, b0 - k1_a + k2_a - k3_a);
                a1 = a0 - (double)1 / 8 * (k1_a + 3 * k2_a + 3 * k3_a + k4_a);

                k1_b = h * Qx0(x0, a0);
                k2_b = h * Qx0(x0 - (double)1 / 3 * h, a0 - (double)1 / 3 * k1_b);
                k3_b = h * Qx0(x0 - (double)2 / 3 * h, a0 + (double)1 / 3 * k1_b - k2_b);
                k4_b = h * Qx0(x0 - h, a0 - k1_b + k2_b - k3_b);
                b1 = b0 - (double)1 / 8 * (k1_b + 3 * k2_b + 3 * k3_b + k4_b);

                k1_g = h * Fx0(x0, a0);
                k2_g = h * Fx0(x0 - (double)1 / 3 * h, a0 - (double)1 / 3 * k1_g);
                k3_g = h * Fx0(x0 - (double)2 / 3 * h, a0 + (double)1 / 3 * k1_g - k2_g);
                k4_g = h * Fx0(x0 - h, a0 - k1_g + k2_g - k3_g);
                g1 = g0 - (double)1 / 8 * (k1_g + 3 * k2_g + 3 * k3_g + k4_g);

                nodes_alpha[i] = a1;
                nodes_beta[i] = b1;
                nodes_gamma[i] = g1;
                a0 = a1;
                b0 = b1;
                g0 = g1;
                if (i != 0)
                {
                    x0 = nodes_X[i];
                }
            }
        }
    }

    public void CalculateSystem()
    {
        for (int i = 0; i < pointsCount; i++)
        {
            double[,] system = new double[2, 3];
            system[0,0] = nodes_U[i];
            system[0,1] = nodes_V[i];
            system[0,2] = nodes_W[i];
            system[1,0] = nodes_alpha[i];
            system[1,1] = nodes_beta[i];
            system[1,2] = nodes_gamma[i];

            double divide = system[0, 0];
            system[0, 0] /= divide;
            system[0, 1] /= divide;
            system[0, 2] /= divide;

            double multi = system[1,0];
            system[1, 0] -= system[0, 0] * multi;
            system[1, 1] -= system[0, 1] * multi;
            system[1, 2] -= system[0, 2] * multi;

            divide = system[1, 1];
            system[1, 0] /= divide;
            system[1, 1] /= divide;
            system[1, 2] /= divide;

            multi = system[0, 1];
            system[0, 0] -= system[1, 0] * multi;
            system[0, 1] -= system[1, 1] * multi;
            system[0, 2] -= system[1, 2] * multi;

            YY[i] = system[0, 2];
            Y[i] = system[1, 2];
        }
    }

    public void OutputToConsole()
    {
        for (int i = 0; i < pointsCount; i++)
        {
            if (i == 0)
            {
                Console.WriteLine("A) X: " + nodes_X[i] + "; Y: " + Y[i] + "; YY: " + YY[i]);
            }
            else if (i == pointsCount - 1)
            {
                Console.WriteLine("B) X: " + nodes_X[i] + "; Y: " + Y[i] + "; YY: " + YY[i]);
            }
            else
            {
                Console.WriteLine(i + ") X: " + nodes_X[i] + "; Y: " + Y[i] + "; YY: " + YY[i]);
            }
        }
    }

    public double Px0(double x, double u, double v)
    {
        return Px(x) * u + v;
    }

    public double Qx0(double x, double u)
    {
        return Qx(x) * u;
    }

    public double Fx0(double x, double u)
    {
        return Fx(x) * u;
    }

    public double Px(double x)
    {
        return x * x + 3*x + 2;
    }

    public double Qx(double x)
    {
        return 2*x * x * x + 1;
    }

    public double Fx(double x)
    {
        return 4 * x + 10;
    }
}

