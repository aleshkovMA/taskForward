using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTask
{
    class engineModel
    {
        //Представление исходных данных
        //Температура перегрева
        public int Tmax { get; set; }
        //Момент инерции
        public int InertMoment { get; set; }
        //Коэффициент зависимости скорости нагрева от крутящего момента
        public double HeatingTorque { get; set; }
        //Коэффициент зависимости скорости нагрева от скорости вращения коленвала
        public double HeatingRotSpeed { get; set; }
        //Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
        public double CoolingRate { get; set; }
        //Крутящий момент
        public int[] Torque { get; set; }
        //Скорость вращения коленвала
        public int[] CrankSRotSpeed { get; set; }

        public engineModel (int tmax, int inertmoment, double heatingtorque, double heatingrotspeed, double coolingrate, int[] torque, int[] cranksrotspeed)
        {
            Tmax = tmax;
            InertMoment = inertmoment;
            HeatingTorque = heatingtorque;
            HeatingRotSpeed = heatingrotspeed;
            CoolingRate = coolingrate;
            Torque = torque;
            CrankSRotSpeed = cranksrotspeed;
        }

        //Метод считающий скорость нагрева двигателя по формуле из задания
        public double heatnes(double M, double Hm, double V, double Hv)
        {
            return (M*Hm)+(Math.Pow(V,2)*Hv);
        }
        //Метод считающий скорость охлаждения двигателя по формуле из задания
        public double cooling(double C, int Tenv, double Teng)
        {
            return C*(Tenv-Teng);
        }
        //Метод считающий мощность по формуле из задания
        public double power(double M, double V)
        {
            return (M * V) / 1000;
        }
    }
}
