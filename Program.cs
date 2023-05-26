using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace testTask
{
    class Program
    {
        //Температура окружающей среды (вводится пользователем)
        private static int Tenv;
        //Переменная необходимая для работы простейшей валидации
        private static bool TryAgain = true;
        //Текущая температура двигателя
        private static double CurrentEngineT;

        const string FILE_NAME = "engineData.json";
        public static void Main(string[] args)
        {
            var path = Path.Combine(Environment.CurrentDirectory, FILE_NAME);
            string fs = File.ReadAllText(path);
 
            engineModel engine = JsonSerializer.Deserialize<engineModel>(fs);
            
           
            Console.WriteLine("Введте температуру окружающей среды:");
            //Простейшая валидация
            while (TryAgain)
            {
                try
                {
                    //Ввод температуры окружающей среды пользователем
                    Tenv = int.Parse(Console.ReadLine());
                    CurrentEngineT = Tenv;
                    TryAgain = false;
                }
                catch
                {
                    Console.WriteLine("Температура должна быть целым числом, повторите ввод:");
                }
            }
            //Запуск тестового стенда определения перегрева
            tempTest(engine);
            //Запуск тестового стенда определения максимальной мощности
            powerTest(engine);
            Console.Read();
        }

        public static void tempTest(engineModel eng)
        {
            //Перебор массива значений скорости вращения коленвала и соответствующих значений крутящего момента
            for (int i=0; i < eng.CrankSRotSpeed.Length; i++)
            {
                double time;
                try 
                {
                    //Определение времени симуляции в точке на графике
                    time = (eng.CrankSRotSpeed[i] * eng.InertMoment) / eng.Torque[i];
                }
                catch
                {
                    time = 0;
                }
                //Определение температуры двигателя в точке на графике
                CurrentEngineT = CurrentEngineT + (eng.heatnes(eng.Torque[i], eng.HeatingTorque, eng.CrankSRotSpeed[i], eng.HeatingRotSpeed) * time) + (eng.cooling(eng.CoolingRate, Tenv, CurrentEngineT) * time);
                if (CurrentEngineT >= eng.Tmax)
                {
                    Console.WriteLine("До наступления перегрева прошло " + time + " секунд от начала симуляции");
                    break;
                }
                else if (i == eng.CrankSRotSpeed.Length-1 && CurrentEngineT < eng.Tmax)
                {
                    Console.WriteLine("При заданных условиях перегрев не наступил");
                }
            }
            
        }

        public static void powerTest(engineModel eng)
        {
            double currentPower=0;
            //Перебор массива значений скорости вращения коленвала и соответствующих значений крутящего момента
            for (int i = 0; i < eng.CrankSRotSpeed.Length; i++)
            {
                double oldPower = currentPower;
                //Определение мощности
                currentPower = eng.power(eng.Torque[i], eng.CrankSRotSpeed[i]);
                if(currentPower< oldPower)
                {
                    Console.WriteLine("Максимальная мощность двигателя составила " + oldPower + " кВт при скорости коленвала " + eng.CrankSRotSpeed[i-1] + " радиан/сек");
                    break;
                }
            }

        }
    }
}
