// Step 1
using System.Data.SqlClient;

namespace AdoDemo
{
    internal class Program
    {
        // Paraemeterized Queries
        static SqlConnection connection;
        static SqlCommand command;
        static void Main(string[] args)
        {
            byte ch;
            char choice = 'y';
            while (choice == 'y')
            {
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("1. Add Record");
                Console.WriteLine("2. Delete Record");
                Console.WriteLine("3. Edit Record");
                Console.WriteLine("4. List of Employees");
                Console.WriteLine("5. Search Employee by ID");
                Console.WriteLine("Enter your choice");
                ch = byte.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1: AddEmployee(); break;
                    case 2: DeleteEmployee(); break;
                    case 3: EditEmployee(); break;
                    case 4: GetEmployees(); break;
                    case 5: GetEmployeeById(); break;
                    default: Console.WriteLine("Invalid choice"); break;
                }
                Console.WriteLine("Do ypu want to repeat");
                choice = Convert.ToChar(Console.ReadLine());

            }
        
        }

        private static string GetConnectionString()
        {
            return @"data source=ANAMIKA\SQLSERVER;initial catalog=EmpDb;integrated security=true";
        }
         private static SqlConnection GetConnection()
        {
           return new SqlConnection(GetConnectionString());
        }
        private static void GetEmployees()
        {
            // Step 2
            connection = GetConnection();
        //connection.ConnectionString = "data source=ANAMIKA\\SQLSERVER;initial catalog=EmpDb;integrated security=true";

        //SqlCommand command = new SqlCommand();
        //command.CommandText = "Select * from Employee";
        //command.Connection= connection;

        // Step 3

          command = new SqlCommand("Select * from Employee", connection);

        // Step 4
        connection.Open();

        // Step 5
        SqlDataReader reader = command.ExecuteReader();

        //if(reader.HasRows)
        //{
        //    while(reader.Read())
        //    {
        //        Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2]);
        //    }
        //}
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + " ");
                }
                Console.WriteLine();
            }
        }
        else
        { Console.WriteLine("There are no records"); }

        connection.Close();
    }

    static void AddEmployee()
        {
            connection = GetConnection();
            command = new SqlCommand();
            command.CommandText = "Insert into Employee values('Deepak','Delhi',38,'Accts')";
            command.Connection = connection;

            connection.Open();
            // ExecuteNonQuery() > performs DML insert/ delete / update
            // it returns no. of records affected

            int count = command.ExecuteNonQuery();
            if(count > 0)
            {
                Console.WriteLine("Record has been added");
            }
            else
                Console.WriteLine("Some Error came");
            connection.Close();

        }

        static void DeleteEmployee()
        {
            connection = GetConnection();
            command = new SqlCommand("Delete Employee where id=3", connection);
            connection.Open();
            int count = command.ExecuteNonQuery();
            if(count>0)
            {
                Console.WriteLine("Record has been deleted");
            }
            connection.Close();
        }

        static void EditEmployee()
        {
            connection = GetConnection();
            command = new SqlCommand("update Employee set age = 26 where id=1", connection);
            connection.Open();
            int count = command.ExecuteNonQuery();
            if (count > 0)
            {
                Console.WriteLine("Record has been edited");
            }
            connection.Close();
        }

       static void GetEmployeeById()
        {
            connection = GetConnection();
            command = new SqlCommand("Select * from Employee where id=1", connection);
             connection.Open();
             SqlDataReader reader = command.ExecuteReader();

       if (reader.HasRows)
            {
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + " ");
                }           
            }
            else
            { Console.WriteLine("There is no record with this ID"); }

            connection.Close();


        }
    }
}