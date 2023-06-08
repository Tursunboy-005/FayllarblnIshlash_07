using System.Threading.Channels;

namespace FayllarblnIshlash_07
{
    internal class Program
    {
        class User
        {
            public string FullName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
        }

        private const string filePath = "C:\\Users\\Asus\\OneDrive\\Documents\\GitHub\\FayllarblnIshlash_07\\Login.txt";
        private static List<User> users = new List<User>();

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("1. Sign up ");
                Console.WriteLine("2. Log in");
                Console.WriteLine("3. Exit ");
                Console.Write("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        SignUp();
                        break;
                    case 2:
                        LogIn();
                        break;
                    case 3:
                        DeleteUser();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void SignUp()
        {
            Console.WriteLine("Sign Up");
            Console.Write("Enter your Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter a Username: ");
            string userName = Console.ReadLine();
            Console.Write("Enter your Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter your Phone number: ");
            string phone = Console.ReadLine();

            User newUser = new User()
            {
                FullName = fullName,
                UserName = userName,
                Password = password,
                Phone = phone
            };

            users.Add(newUser);
            WriteUsersToFile();

            Console.WriteLine("User created successfully!");
        }

        static void LogIn()
        {
            Console.WriteLine("Log In");
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            User founderUser = users.Find(user => user.UserName == username && user.Password == password);

            if(founderUser != null)
            {
                Console.WriteLine("User found");
                Console.WriteLine("Full Name : " + founderUser.FullName);
                Console.WriteLine("Username: " + founderUser.UserName);
                Console.WriteLine("Phone: " + founderUser.Phone);
            }
            else
            {
                Console.WriteLine("Invalid username or password!");
            }
        }

        static void DeleteUser()
        {
            Console.WriteLine("Delete User");
            Console.Write("Enter the username of the user to delete : ");
            string username = Console.ReadLine();

            User userToDelete = users.Find(user => user.UserName == username); 
            if(userToDelete != null)
            {
                users.Remove(userToDelete);
                WriteUsersToFile();

                Console.WriteLine("User deleted successfully!");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }


        static List<User> ReadUsersFromFile()
        {
            List<User> usersList = new List<User>();

            if(File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        string[] userProperties = line.Split(',');
                        User user = new User()
                        {
                            FullName = userProperties[0],
                            UserName = userProperties[1],
                            Phone = userProperties[2],
                        };
                        usersList.Add(user);
                    }
                }
            }

            return usersList;
        }

        static void WriteUsersToFile()
        {
            using(StreamWriter sw = new StreamWriter(filePath))
            {
                foreach(User user in users)
                {
                    string line = $"{user.FullName}, {user.UserName}, {user.Password}, {user.Phone}";
                    sw.WriteLine(line);
                }
            }
        }
    }
}