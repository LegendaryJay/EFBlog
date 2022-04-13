using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using EFBlog.Models;

namespace EFBlog
{
    internal static class Program
    {
        //just to be super clear, this code was seeded from the Microsoft official example of EF due to hours
        //of troubleshooting issues. I can't find the source, but it pretty much matches your reference anyways.

        //https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/existing-database

        //if you think this is cheating, I can rewrite it or explain what each section does

        private static bool GetBlog(out Blog blog)
        {
            using var db = new BlogContext();
            var query = from b in db.Blogs
                orderby b.Name
                select b;
            Console.WriteLine($"{query.Count()} Blogs found");
            if (!query.Any())
            {
                Console.WriteLine("No Blogs exist");
                blog = null;
                return false;
            }

            foreach (var item in query)
            {
                Console.WriteLine(item.BlogId + " " + item.Name);
            }

            Console.WriteLine("Which Blog Id do you want to look at?");

            if (!int.TryParse(Console.ReadLine(), out var x))
            {
                Console.WriteLine("Invalid input");
                blog = null;
                return false;
            }

            blog = (query.FirstOrDefault(blog1 => blog1.BlogId == x) ?? null)!;
            if (blog is not null) return true;
            Console.WriteLine("Blog does not exist");
            return false;
        }

        private static void DisplayBlogs()
        {
            //this is SO COOL!!! 
            using var db = new BlogContext();
            var query = from b in db.Blogs
                orderby b.Name
                select b;
            foreach (var blog in query)
            {
                Console.WriteLine(blog.Name);
            }
        }

        private static void AddBlog()
        {
            using var db = new BlogContext();
            Console.WriteLine("whats da feelin' box name?");
            var name = Console.ReadLine();

            var blog = new Blog {Name = name};
            db.Blogs.Add(blog);
            db.SaveChanges();
        }

        private static void DisplayPosts()
        {
            if (!GetBlog(out var selectedBlog)) return;
            Console.WriteLine(selectedBlog.Name);


            Console.WriteLine($"{selectedBlog.Posts.Count} Posts");
            foreach (var post in selectedBlog.Posts)
            {
                Console.WriteLine($"{post.Blog.Name}");
                Console.WriteLine($"{post.Title}");
                Console.WriteLine($"{post.Content}");
            }
        }

        private static void AddPost()
        {
            if (!GetBlog(out var selectedBlog)) return;


            Console.WriteLine("Name your post!");
            var title = Console.ReadLine();
            Console.WriteLine("Write your content!");
            var content = Console.ReadLine();

            var newPost = new Post
            {
                Title = title,
                Content = content,
                BlogId = selectedBlog.BlogId,
            };
            using var db = new BlogContext();
            db.Posts.Add(newPost);
            db.SaveChanges();
        }

        private static void Main(string[] args)
        {
            {
                Console.WriteLine("what you want?");
                Console.WriteLine("\t1 Show evrones feelin's box");
                Console.WriteLine("\t2 Add a feelin's box");
                Console.WriteLine("\t3Lookit pacific feelin's");
                Console.WriteLine("\t4right yer feelin's");

                var selection = Console.ReadLine();
                switch (selection)
                {
                    case "1":
                        DisplayBlogs();
                        break;
                    case "2":
                        AddBlog();
                        break;
                    case "3":
                        DisplayPosts();
                        break;
                    case "4":
                        AddPost();
                        break;
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}