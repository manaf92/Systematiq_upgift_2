using System.Data.SqlClient;
using Systematiq_upgift_2;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=systematiq_test_data;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
// ha alla deltagare
Console.WriteLine("Tävlingar\n");

using (SqlConnection connection = new(connectionString))
{
    connection.Open();
    string selectTävingar = "select * from tävling";
    SqlCommand command1 = new (selectTävingar, connection);
    SqlDataReader data1 = command1.ExecuteReader();

    List<Tävling> tävlings = new();
    while (data1.Read())
    {
        Tävling t = new()
        {
            Id = (int)data1[0],
            Namn = (string)data1[1]
        };
        tävlings.Add(t);
    }
    data1.Close();
    tävlings.ForEach(t =>
    {
        Console.WriteLine("\n"+t.Id + "\t" + t.Namn+"\n");

        string selectDeltagarna = "select * from deltagares where TävlingId = " + t.Id;
        SqlCommand command2 = new(selectDeltagarna, connection);
        SqlDataReader data2 = command2.ExecuteReader();

        while (data2.Read())
        {
            Console.WriteLine("\t\t" + data2[0] + "\t" + data2[1]);
        }
        data2.Close();
    });
}
