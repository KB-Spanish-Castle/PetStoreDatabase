/*
Kyle Bigart
Modern Database Systems
Final Project
12/10/19

My Final Project's purpose is to be used for a Pet Store Online Website to order pets and pet supplies.
The project is using Marten to interact a Postgress database.

The User interaction in this program allows the user to commandline interface of:
Selecting All Objects from the database,
build the database of pre-defined data,
placing an order,
and will remain active in the while loop, until the user types "exit" when in the Main Menu to exit the program.

 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Marten;
namespace KBMDSFinalProject
{
    /*


    Here is what was Documented and turned in for Task 2:

    Pet Store with: products, checkout, customer, order contain customer and products, and facts for all
    those tables and operations of want to do retrieve all the products select all the customers or products in
    one product or select all the orders by id and to add an order. Three different Document Type Product
    customer and order.
    Select Products to simulate a checkout a customer add them to the order
    Postgres with Martin
    Product:
    Id
    String/VarChar(90) Name
    String/VarChar(30) ProductType
    Examples: “PetFood”, “Animal”, or “PetSupplies”
    Double Price
    QOH Integer

    Customer:
    Id
    FirstName
    LastName
    Email
    Phone (Null)
    PasswordHash
    PasswordSalt

    OrderItem:
    Id
    OrderId
    ProductId
    Quantity

    Order:
    Id
    ProductId
    CustomerId
    DateItem
    Status: Default “In Cart” if Null

    Console App To simulate the purchase have email let them pick a customer as many products as they
    want and finish and show the total. Order complete or pre shipment status.
    Once the customers are in there and comment it out.
    Show Existing orders and order and total.
    Should include the one they just did without looking into the database.
    Interactive let them pick the product and order of each and how many the user wants.


    */


    class Program
    {
        static void Main(string[] args)
        {

            string usercommand = "";
            //The user is stuck until types, "exit".
            //On Line 89 it trims and lowercase what the user types to take care spaces after the command and takes care of any caps.
            while (usercommand != "exit")
            {
                Console.Write("*MAIN MENU*\nWhat Command do you want to use?\nIf you want to exit type 'exit'\nWant to Create the new data type 'build'.\nWant To Read All data type 'readall'\nWant to place an Order type 'order'.\nThe code isn't case sensitive.\nWhat do you want to do?\n");
                usercommand = Console.ReadLine().ToLower().Trim();

                if (usercommand == "readall")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PetDB"].ConnectionString;
                    IDocumentStore store = DocumentStore.For(connectionString);
                    using (var runPetDb = store.OpenSession())
                    {

                        foreach (Customer rc in runPetDb.Query<Customer>())
                        {
                            Console.WriteLine("Customer:\nFirstName: {0} LastName: {1} Email: {2} Id#{3}", rc.FirstName, rc.LastName, rc.Email, rc.Id);
                        }

                        foreach (OrderItem roi in runPetDb.Query<OrderItem>())
                        {
                            Console.WriteLine("OrderItem:\nProduct: {0} Quantity: {1} Id#{2}", roi.Product, roi.Quantity, roi.Id);
                        }

                        foreach (Product rp in runPetDb.Query<Product>())
                        {
                            Console.WriteLine("Product:\nName: {0} Type: {1} Price: ${2} Qunatity On Hand: {3} Id#{4}", rp.Name, rp.Type, rp.Price, rp.QOH, rp.Id);
                        }

                        foreach (Order ro in runPetDb.Query<Order>())
                        {
                            Console.WriteLine("Order:\nOrderItemList: {0} DateItem: {1} Status: {2} Id#{3}", ro.OrderItemList, ro.DateItem, ro.Status, ro.Id);
                        }

                    }

                }
                /*
                    Select id in product in loop and everything they select creates order item attached to the list in order.
                */
                if (usercommand == "order")
                {
                    string usercommand2 = "";
                    while (usercommand2 != "done")
                    {
                        //Prompt Order Id, hit enter or invalid command it will show all the Products.
                        //The command "done" have to be used in order to leave this Order Menu.
                        Console.WriteLine("\n*ORDER MENU*\nYou are Ordering from Pet Store Website.\n To Finish the Order type 'done'\n Hit enter to read all or type the matching number of the Product.\nWhat command my user?\n");
                        usercommand2 = Console.ReadLine().ToLower().Trim();

                        string connectionString = ConfigurationManager.ConnectionStrings["PetDB"].ConnectionString;
                        IDocumentStore store = DocumentStore.For(connectionString);

                        //The usercommand2 input will be in the if-statement to see if usercommand2 input matched any ProductId to add the product to the newly created OderItem.
                        using (var runPetDb = store.OpenSession())
                        {
                            //Customer created in order to be used for the new OrderItem.
                            Customer c4 = new Customer { FirstName = "Abel", LastName = "Smith", Email = "AbelNot@home.com", PasswordHash = "red35deer84r5", PasswordSalt = "7905b6e4bc41a22aef2554eb4402255f" };

                            runPetDb.Store(c4);

                            foreach (Product p in runPetDb.Query<Product>())
                            {
                                Console.WriteLine("Id: {0} Name: {1}Type: {2} Price: ${3} Quantity On Hand: {4}", p.Id, p.Name, p.Type, p.Price, p.QOH);
                                //Try block blelow have an if statement is only active if the user typed a valid integer that matches the Product Id in the database.
                                try
                                {
                                    if (Int32.Parse(usercommand2) == p.Id)
                                    {

                                        Console.WriteLine("Your Selected Order is: Id: {0} Name: {1}Type: {2} Price: ${3} Quantity On Hand: {4}", p.Id, p.Name, p.Type, p.Price, p.QOH);
                                        //An new order had to be created, with the user selecting which Product Id will be placed in the Order.
                                        Order o4 = new Order { Customer = c4, Id = 4, DateItem = DateTime.Now };
                                        Console.WriteLine("How many?");

                                        OrderItem createOI = new OrderItem { Product = p, Id = 0, Quantity = 5 };
                                        o4.OrderItemList.Add(createOI);
                                        runPetDb.Store(o4);

                                    }
                                }
                                catch (Exception ex)
                                {
                                    //In this Catch included the error message and letting the user know to hit enter to continue.
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("Hit ENTER to continue!");
                                }
                            }

                        }
                        Console.ReadLine();

                        //done is the command to exit order.
                    }
                }
                //User have to type "build" it's not case sensetive so it would accept any capital letters, because the user input will be converted to lowercase on line 89.
                //All the predefined user data will be built, using the classes and userdata it will be built into Postgress Database.
                if (usercommand == "build")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["PetDB"].ConnectionString;
                    IDocumentStore store = DocumentStore.For(connectionString);

                    using (var runPetDb = store.OpenSession())
                    {
                        Customer c1 = new Customer { FirstName = "Mark", LastName = "Smith", Email = "Mark.Smith@NoEmail.gov", PasswordHash = "pass35wordab0", PasswordSalt = "79054025255fb1a26e4bc422aef54eb4" };
                        Customer c2 = new Customer { FirstName = "Geroge", LastName = "Brown", Email = "George@work.org", PasswordHash = "bla84ckbocat87", PasswordSalt = "90547054ef25222a5cf54eb4be41a26b" };
                        Customer c3 = new Customer { FirstName = "Bob", LastName = "Wilson", Email = "Wilson@home.com", PasswordHash = "red53dog85r4", PasswordSalt = "7905b1a254eb4406e4bc422aef25255f" };

                        runPetDb.Store(c1);
                        runPetDb.Store(c2);
                        runPetDb.Store(c3);

                        Product p1 = new Product { Name = "Cat Food", Type = "PetFood", Price = 24.00, QOH = 30 };
                        Product p2 = new Product { Name = "Koi", Type = "Animal", Price = 7.79, QOH = 45 };
                        Product p3 = new Product { Name = "Fish Food", Type = "PetFood", Price = 14.00, QOH = 50 };
                        Product p4 = new Product { Name = "Small Fish Net", Type = "PetSupplies", Price = 3.99, QOH = 30 };
                        Product p5 = new Product { Name = "Beta", Type = "Animal", Price = 4.59, QOH = 50 };
                        Product p6 = new Product { Name = "Dog Food", Type = "PetFood", Price = 29.99, QOH = 35 };
                        Product p7 = new Product { Name = "Hermit Crab", Type = "Animal", Price = 6.29, QOH = 45 };
                        Product p8 = new Product { Name = "5 Gallon Fish Tank", Type = "PetSupplies", Price = 34.99, QOH = 13 };

                        runPetDb.Store(p1);
                        runPetDb.Store(p2);
                        runPetDb.Store(p3);
                        runPetDb.Store(p4);
                        runPetDb.Store(p5);
                        runPetDb.Store(p6);
                        runPetDb.Store(p7);
                        runPetDb.Store(p8);

                        //DateItem format: YYYY-MM-DDThh:mm:ss
                        Order o1 = new Order { Customer = c1, Id = 1, DateItem = DateTime.Now };
                        Order o2 = new Order { Customer = c2, Id = 2, DateItem = DateTime.Now };
                        Order o3 = new Order { Customer = c3, Id = 3, DateItem = DateTime.Now };

                        runPetDb.Store(o1);
                        runPetDb.Store(o2);
                        runPetDb.Store(o3);

                        OrderItem oi1 = new OrderItem { Product = p1, Id = 1, Quantity = 5 };
                        OrderItem oi2 = new OrderItem { Product = p2, Id = 2, Quantity = 3 };
                        OrderItem oi3 = new OrderItem { Product = p3, Id = 3, Quantity = 4 };
                        OrderItem oi4 = new OrderItem { Product = p4, Id = 4, Quantity = 5 };
                        OrderItem oi5 = new OrderItem { Product = p5, Id = 5, Quantity = 4 };
                        OrderItem oi6 = new OrderItem { Product = p6, Id = 6, Quantity = 1 };
                        OrderItem oi7 = new OrderItem { Product = p7, Id = 7, Quantity = 8 };
                        OrderItem oi8 = new OrderItem { Product = p8, Id = 8, Quantity = 1 };

                        runPetDb.Store(oi1);
                        runPetDb.Store(oi2);
                        runPetDb.Store(oi3);
                        runPetDb.Store(oi4);
                        runPetDb.Store(oi5);
                        runPetDb.Store(oi6);
                        runPetDb.Store(oi7);
                        runPetDb.Store(oi8);

                        runPetDb.SaveChanges();

                    }

                }
            }
            Console.WriteLine("\nBye now!\nClose the Window or hit ENTER!");
            Console.ReadLine();

        }
    }
}
