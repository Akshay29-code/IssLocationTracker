using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = "https://api.wheretheiss.at/v1/satellites/25544";
        HttpClient client = new HttpClient();

        Console.WriteLine("==========================================");
        Console.WriteLine("   ISS Location Tracker");
        Console.WriteLine("   Press Ctrl+C to stop the program");
        Console.WriteLine("==========================================");
        Console.WriteLine();

        while (true)
        {
            try
            {
                string jsonResponse = await client.GetStringAsync(apiUrl);
                IssLocation iss = JsonSerializer.Deserialize<IssLocation>(jsonResponse)!;

                if (iss != null)
                {
                    Console.Clear();

                    Console.WriteLine("==========================================");
                    Console.WriteLine("   ISS Location Tracker");
                    Console.WriteLine("   Press Ctrl+C to stop the program");
                    Console.WriteLine("==========================================");
                    Console.WriteLine();

                    Console.WriteLine("  Current Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Console.WriteLine();
                    Console.WriteLine("  Satellite    : " + iss.Name);
                    Console.WriteLine("  Latitude     : " + iss.Latitude + "°");
                    Console.WriteLine("  Longitude    : " + iss.Longitude + "°");
                    Console.WriteLine("  Altitude     : " + iss.Altitude + " km");
                    Console.WriteLine("  Velocity     : " + iss.Velocity + " km/h");
                    Console.WriteLine("  Visibility   : " + iss.Visibility);
                    Console.WriteLine();
                    Console.WriteLine("==========================================");
                    Console.WriteLine("  Refreshing every 1 second...");
                    Console.WriteLine("==========================================");
                }
                else
                {
                    Console.WriteLine("Error: Could not read ISS data from the API.");
                }
            }
            catch (HttpRequestException)
            {
                Console.WriteLine();
                Console.WriteLine("Error: Could not connect to the API.");
                Console.WriteLine("Please check your internet connection.");
                Console.WriteLine("Trying again in 1 second...");
            }
            catch (JsonException)
            {
                Console.WriteLine();
                Console.WriteLine("Error: Could not parse the API response.");
                Console.WriteLine("The API may have changed its format.");
                Console.WriteLine("Trying again in 1 second...");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                Console.WriteLine("Trying again in 1 second...");
            }

            await Task.Delay(1000);
        }
    }
}
