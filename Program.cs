using System;
using NLog.Web;
using System.IO;

namespace MediaLibrarySearch
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.scrubbed.csv";

            logger.Info("Program started");

            //string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            //logger.Info(scrubbedFile);
            //MovieFile movieFile = new MovieFile(scrubbedFile);

            MovieFile movieFile = new MovieFile(movieFilePath);

            string choice = "";
            do
            {
                // display choices to user
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("3) Find Movie by title");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                if (choice == "1")
                {
                    // Add movie
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title");
                    // input title
                    movie.title = Console.ReadLine();
                    // verify title is unique
                    if (movieFile.isUniqueTitle(movie.title)){
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        // ask user to input director
                        Console.WriteLine("Enter director");
                        // input director
                        movie.director = Console.ReadLine();
                        // ask user to input runtime
                        Console.WriteLine("Enter the runtime(h:m:s)");
                        // input runtime
                        movie.runningTime = TimeSpan.Parse(Console.ReadLine());
                        // add movie
                        movieFile.AddMovie(movie);
                    }
                }
                
                if (choice == "2")
                {
                    // Display All Movies
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }

                if (choice == "3")
                {
                    
                }
            } while (choice == "1" || choice == "2" || choice == "3");


            logger.Info("Program ended");
        }
    }
}
