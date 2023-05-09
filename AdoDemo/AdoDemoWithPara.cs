// Step 1
using System.Data.SqlClient;

namespace AdoDemoWithPara
{
    internal class Program
    {
        // Parameterized Queries
        static SqlConnection connection;
        static SqlCommand command;
        static void Main(string[] args)
        {
            CreateDatabase();
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
                    case 1:
                        {
                            Console.WriteLine("Enter Name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Age");
                            int age = byte.Parse(Console.ReadLine());
                            Console.WriteLine("ENter Address");
                            string address = Console.ReadLine();
                            Console.WriteLine("ENter Dept");
                            string dept = Console.ReadLine();

                            AddEmployee(name: name, address: address, age: age, dept: dept); break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter Id for which to dlete record");
                            int id = byte.Parse(Console.ReadLine());
                            DeleteEmployee(id: id); break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Id for which to edit record");
                            int id = byte.Parse(Console.ReadLine());
                            Console.WriteLine("ENter new Address");
                            string address = Console.ReadLine();
                            Console.WriteLine("ENter new Dept");
                            string dept = Console.ReadLine();
                            EditEmployee(id, address, dept); break;
                        }
                    case 4: GetEmployees(); break;
                    case 5:
                        {
                            Console.WriteLine("Enter Id for which to search record");
                            int id = byte.Parse(Console.ReadLine());
                            GetEmployeeById(id); break;
                        }
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
            try
            {
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                command.Dispose();
                connection.Dispose();
        } }

        static void AddEmployee(string name, string address, int age, string dept)
        {

            try
            {
                connection = GetConnection();
                command = new SqlCommand();
                command.CommandText = "Insert into Employe  (name, age, address, dept) values (@name, @age, @address,@dept)";
                command.Connection = connection;
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@dept", dept);
                command.Parameters.AddWithValue("@address", address);

                connection.Open();
                // ExecuteNonQuery() > performs DML insert/ delete / update
                // it returns no. of records affected

                int count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("Record has been added");
                }
                else
                    Console.WriteLine("Some Error came");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                command.Dispose();
                connection.Dispose();
            }
        }

        static void DeleteEmployee(int id)
        {
            try
            {
                using (connection = GetConnection())
                {
                    using (command = new SqlCommand("Delete Employee where id=@id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        connection.Open();
                        int count = command.ExecuteNonQuery();
                        if (count > 0)
                        {
                            Console.WriteLine("Record has been deleted");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        static void CreateDatabase()
        {
            try
            {
                using (connection = GetConnection())
                {
                    using (command = new SqlCommand("Create Database FirstDb", connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void EditEmployee(int id, string address, string dept)



        {
            using (connection = GetConnection())
            {
                int count;
                try
                {
                    using (command = new SqlCommand("update Employee set address =@address, dept=@dept  where id=1", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@dept", dept);
                        connection.Open();
                        count = command.ExecuteNonQuery();
                    }
                    if (count > 0)
                    {
                        Console.WriteLine("Record has been edited");
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        static void GetEmployeeById(int id)
        {
            try
            {
                using (connection = GetConnection())
                {
                    using (command = new SqlCommand("Select * from Employee where id=@id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
    }
}