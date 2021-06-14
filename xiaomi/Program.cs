using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqo;

namespace xiaomi
{

    public class Policeman
    {
        public int OID { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public decimal salary { get; set; }

        public override string ToString()
        {
            return "ID: " + OID + ", name " + name + ", last name: " + lastName + ", salary: " + salary.ToString();
        }


    }

    class Program
    {
        private static void countSalaryAvarage(Siaqodb siaqodb)
        {

            IObjectList<Policeman> policemans = siaqodb.LoadAll<Policeman>();
            decimal sum = 0;
            int counter = 0;
            foreach(Policeman policeman in policemans)
            {
                counter++;
                sum += policeman.salary;
                
            }
            Console.WriteLine("Avarage salary: " + sum/counter);
        }
  
        private static void updatePoliceman(Siaqodb siaqodb)
        {
            Console.WriteLine("Enter id of policeman");
            int id = Convert.ToInt32(Console.ReadLine());

            var policeman = siaqodb.LoadObjectByOID<Policeman>(id);

            if(policeman != null)
            {
                Console.WriteLine("Enter name");
                policeman.name = Console.ReadLine();
                Console.WriteLine("Enter surname");
                policeman.lastName = Console.ReadLine();
                Console.WriteLine("Enter salary");
                policeman.salary = Convert.ToDecimal(Console.ReadLine());
                siaqodb.StoreObject(policeman);
            }
            else
            {
                Console.WriteLine("There is no policeman with this ID");
            }


        }


       
        private static void deletePoliceman(Siaqodb siaqodb)
        {
            Console.WriteLine("Enter id of driver to delete");

            int id = Convert.ToInt32(Console.ReadLine());
            var policeman = siaqodb.LoadObjectByOID<Policeman>(id);

            if (policeman != null)
            {
                siaqodb.Delete(policeman);
            }
            else
            {
                Console.WriteLine("There is no driver with this ID");
            }
        }
     
        

        private static void getPolicemanBySurname(Siaqodb siaqodb)
        {
            Console.WriteLine("Enter last name");
            string surname = Console.ReadLine();

            var query = from Policeman policeman in siaqodb
                        where policeman.lastName.Contains(surname)
                        select policeman;
            foreach (Policeman dri in query){
                Console.WriteLine(dri.ToString());
            }
        }
        private static void getPolicemanById(Siaqodb siaqodb)
        {
            Console.WriteLine("Enter id");
            int id = Convert.ToInt32(Console.ReadLine());

            var policeman = siaqodb.LoadObjectByOID<Policeman>(id);

            Console.WriteLine(policeman.ToString());
        }

        public static void getAllPolicemans(Siaqodb siaqodb)
        {
            IObjectList<Policeman> policemans = siaqodb.LoadAll<Policeman>();

            foreach (Policeman policeman in policemans) {
                Console.WriteLine(policeman.ToString());
            }
        }

        private static void getPolicemans(Siaqodb siaqodb)
        {
            Console.WriteLine("What you want to do:");
            Console.WriteLine("1 - get all, 2 - get by ID, 3 - get by surname, 4 - exit");
            int select = Convert.ToInt32(Console.ReadLine());

            switch (select)
            {
                case 1:
                    getAllPolicemans(siaqodb);
                    break;
                case 3:
                    getPolicemanBySurname(siaqodb);
                    break;
                case 2:
                    getPolicemanById(siaqodb);
                    break;
                case 4:
                    break;
            }
        }

        public static void createPoliceman(Siaqodb siaqodb)
        {
            Policeman policeman = new Policeman();

            Console.WriteLine("Enter name");
            policeman.name = Console.ReadLine();
            Console.WriteLine("Enter surname");
            policeman.lastName = Console.ReadLine();
            Console.WriteLine("Enter salary");
            policeman.salary = Convert.ToDecimal(Console.ReadLine());
            siaqodb.StoreObject(policeman);

        }
     
        static void Main(string[] args)
        {
            Siaqodb siaqodb = new Siaqodb("c:\\Siaqodb\\");
            int select;
            do {
                Console.WriteLine("Welcome to Police");
                Console.WriteLine("Enter value to select menu");
                Console.WriteLine("1 - create policeman , 2 - update policeman, 3 - delete policeman, 4 - get, 5 - advanced query, 6 - quit");
                select = Convert.ToInt32(Console.ReadLine());
                switch (select) {
                    case 1:
                        createPoliceman(siaqodb);
                        break;
                    case 2:
                        updatePoliceman(siaqodb);
                        break;
                    case 3:
                        deletePoliceman(siaqodb);
                        break;
                    case 4:
                        getPolicemans(siaqodb);
                        break;
                    case 5:
                        countSalaryAvarage(siaqodb);
                        break;
                    case 6:
                        break;
                }

            } while (select != 6);
        }
    } 
}
