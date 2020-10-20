using Practica2DIA.Core.Camiones;

namespace Practica2DIA.View {
    using Core;
    
    using System;
    
    public class ConsoleUI
    {
        public static int Menu()
        { 
            int toret = 0;
            
            Console.WriteLine("\nCargamentos");
            Console.WriteLine("1. Lista de Cargamentos");
            Console.WriteLine("2. Inserta nuevo cargamento");
            Console.WriteLine("0. Fin");

            do
            {
                Console.WriteLine("\nSelecciona (0-2): ");

                if (!int.TryParse(Console.ReadLine(), out toret))
                {
                    toret = -1;
                }

            } while (toret < 0 && toret > 2);

            return toret;
        }

        public static Cargamento PideDatos()
        {
            string idCarg;
            int tipoCam;
            string idCam;
            int tipoCont1;
            string idCont1;
            int tipoCont2;
            string idCont2;
            string check;

            Console.WriteLine("ID del cargamento: ");
            idCarg = Console.ReadLine();
            
            Console.WriteLine("Camion grande(0) o pequeno(1)? ");
            tipoCam = Int32.Parse(Console.ReadLine());
            var tCam = (Camion.TipoCamion) tipoCam;
            
            Console.WriteLine("ID del camion: ");
            idCam = Console.ReadLine();
            
            Console.WriteLine("Contenedor grande(0) o pequeno(1): ");
            tipoCont1 = Int32.Parse(Console.ReadLine());

            if (tipoCam == 1 && tipoCont1 == 0)
            {
                throw new System.ArgumentException("Un camion pequeno no puede llevar un cont. grande");    
            }
            
            var tCont1 = (Contenedor.TipoCont) tipoCont1;
            
            Console.WriteLine("ID del contenedor: ");
            idCont1 = Console.ReadLine();

            if (tipoCam == 0 && tipoCont1 == 1)
            {
                Console.WriteLine("Deseas añadir otro contenedor?(Y/N): ");
                check = Console.ReadLine();
                
                if (!check.ToLower().Equals("n"))
                {
                    Console.WriteLine("Contenedor grande(0) o pequeno(1): ");
                    tipoCont2 = Int32.Parse(Console.ReadLine());

                    if ((tipoCont1 == 1 && tipoCont2 == 0) || (tipoCont1 == 0 && tipoCont2 == 1))            
                    {
                        throw new System.ArgumentException("Un camion grande no puede llevar un cont. grande y uno pequeno");    
                    }
            
                    var tCont2 = (Contenedor.TipoCont) tipoCont2;
                    
                    Console.WriteLine("ID del contenedor: ");
                    idCont2 = Console.ReadLine();
            
                    return new Cargamento(idCarg, tCam, idCam, tCont1, idCont1, tCont2,idCont2);
                    
                }

            }
            
            return new Cargamento(idCarg, tCam, idCam, tCont1, idCont1);
            
        }

        public static void MainLoop(string[] args)
        {
            int op;
            RegistroCargamentos cargamentos = RegistroCargamentos.RecuperaXml();

            op = Menu();
            while (op != 0)
            {
                switch (op)
                {
                    case 1:
                        Console.WriteLine(cargamentos.ToString());
                        break;
                    case 2:
                        cargamentos.Add(PideDatos());
                        break;
                }

                op = Menu();
            }
            
            cargamentos.GuardaXml();
            return;
        }
    }
}