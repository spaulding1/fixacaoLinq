using fixacaoLinq.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace fixacaoLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //pegar o caminho do arquivo a ser lido
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> lista = new List<Employee>();

            //ler o arquivo e jogar as informações numa lista de Employee
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] campos = sr.ReadLine().Split(',');
                    string nome = campos[0];
                    string email = campos[1];
                    double salario = double.Parse(campos[2], CultureInfo.InvariantCulture);
                    lista.Add(new Employee(nome,email, salario));
                }
            }

            //pegar valor de salário para comparação
            Console.Write("Enter salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine($"Email of people whose salary is more than {salary.ToString("F2", CultureInfo.InvariantCulture)}:");

            //pegar lista de e-mails de funcionário cujo valor é superior ao salary
            var emails = lista.Where(p => p.Salario > salary).OrderBy(p => p.Email).Select(p => p.Email);
            foreach (var item in emails)
            {
                Console.WriteLine(item);
            }

            //somar de salário das pessoas cujo nome começam com M
            var somaSalarios = lista.Where(p => p.Nome[0] == 'M').Sum(p => p.Salario);
            Console.Write($"Sum of salary of people whose name starts with 'M': {somaSalarios.ToString("F2", CultureInfo.InvariantCulture)}");
        }
    }
}
