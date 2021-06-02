using System;

namespace blog.DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            // userRepository.Create(
            //     new User()
            //     {
            //         Email = "hovosisoyan@gmail.com",
            //         Username = "hovhannes.sisoyan",
            //         Password = "123456",
            //         FirstName = "Hovhannes",
            //         LastName = "Sisoyan",
            //         BirthDate = new DateTime(26/09/1995),
            //         Gender = true,
            //         Posts = null,
            //     }
            // );
            Console.WriteLine(userRepository.ReadAll()[0].FirstName);

            PostRepository postRepository = new PostRepository();
            // postRepository.Create(
            //     new Post()
            //     {
            //         Title = "Why learn programming?",
            //         Category = "IT",
            //         Body = "Programming helps to communicate with the computers.",
            //         UserId = 1,
            //     }
            // );
            // postRepository.Create(
            //     new Post()
            //     {
            //         Title = "Dependency injection in c#",
            //         Category = "IT",
            //         Body = "Dependency Injection (DI) is a design pattern used to implement IoC. It allows the creation of dependent objects outside of a class and provides those objects to a class through different ways. Using DI, we move the creation and binding of the dependent objects outside of the class that depends on them.",
            //         UserId = 1,
            //     }
            // );
            Console.WriteLine(postRepository.ReadAll()[0].Title);
        }
    }
}
