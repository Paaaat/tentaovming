using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tentaovming.models;

namespace tentaovming
{
    class Program
    {
        static string cns = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NORTHWND;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static void Main(string[] args)
        {
            //funkar git
            //Find("ALFKI");                     UPPGIFT1 (ADO)
            //ListordersByCustomers("ALFKI");    UPPGIFT4 (ENTITY FRAMEWORK)
            //IncreaseBeveragesByEightPercent(); UPPGIFT6 (ENTITY FRAMEWORK)
            //CustomersWithoutOrders();          UPPGIFT8 (ENTITY FRAMEWORK)
        }

        private static void CustomersWithoutOrders()
        {
            using (NorthwindContext cx = new NorthwindContext())
            {
                foreach (var cust in cx.Customers)
                {
                    if (cust.Orders.Count == 0)
                    {
                        Console.WriteLine(cust.CompanyName);
                    }
                }
            }
            Console.ReadLine();
        }

        private static void IncreaseBeveragesByEightPercent()
        {
            using (NorthwindContext cx = new NorthwindContext())
            {
                var cat = cx.Categories.Where(c => c.CategoryName == "Beverages").First();
                foreach (var p in cat.Products)                    
                {
                    p.UnitPrice = p.UnitPrice * 1.08M;
                }
                cx.SaveChanges();
            }
        }

        private static void ListordersByCustomers(string customer)
        {
            NorthwindContext cx = new NorthwindContext();
            var alfkiCustomer = cx.Customers.Find(customer);
            foreach (var order in alfkiCustomer.Orders)
            {
                Console.WriteLine(order.OrderID);
            }
        }

        static void Find(string customerid)
        {
            SqlConnection cn = new SqlConnection(cns);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select companyname, phone from customers where customerid=@customerid";
            cmd.Parameters.AddWithValue("@Customerid", customerid);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0));
                Console.WriteLine(rd.GetString(1));
            }
            rd.Close();
            cn.Close();
        }
    }
}
