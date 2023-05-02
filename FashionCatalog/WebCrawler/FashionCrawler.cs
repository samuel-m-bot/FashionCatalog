using FashionCatalogue.Models;
using HtmlAgilityPack;
using Microsoft.Data.SqlClient;

namespace FashionCatalogue.WebCrawler
{
    public class FashionCrawler
    {
        private static async Task crawlWebsites(Website website)
        {
            List<Product> products = new List<Product>();
            var httpClient = new HttpClient();
            foreach (var category in website.Catagories)
            {
                for (int i = 0; i < website.PageNumber.Length; i++)
                {
                    var ur = "";
                    string[] elements = category.Value.Split(',');

                    if (elements[8] != "" && elements[9] != "" && website.PageNumber[i] != "")
                    {
                        string page = website.PageNumber[i].Replace(elements[8], elements[8] + elements[9]);
                        ur = $"{website.Url}/{category.Key}" + page;
                    }
                    else
                    {
                        ur = $"{website.Url}/{category.Key}" + website.PageNumber[i];
                    }
                    var html = await httpClient.GetStringAsync(ur);
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    var divs = htmlDocument.DocumentNode.Descendants(elements[11])
                       .Where(node => node.GetAttributeValue("class", "").Equals(elements[0])).ToList();
                    foreach (var div in divs)
                    {
                        string priceStr = "";
                        var price = div.Descendants(elements[2]).ToList();
                        foreach (var item in price)
                        {
                            if (item.GetAttributeValue("class", "") == elements[7])
                            {
                                priceStr = item.InnerText.Replace("\n", "").Replace("\r", "");
                                break;
                            }
                            else if (item.GetAttributeValue("class", "") == elements[6])
                            {
                                priceStr = item.InnerText.Replace("\n", "").Replace("\r", "");
                            }
                        }
                        var test = div.Descendants(elements[1]).Where(node => node.GetAttributeValue("class", "").Contains(elements[12])).ToList();

                        var product = new Product
                        {
                            Name = div.Descendants(elements[1]).Where(node => node.GetAttributeValue("class", "").Contains(elements[12])).FirstOrDefault().InnerText.Replace("\n", "").Replace("\r", "").TrimStart().Replace(".", "").TrimEnd(),
                            SiteURL = div.Descendants("a").LastOrDefault().ChildAttributes("href").FirstOrDefault().Value,
                            ImageURL = div.Descendants(elements[3]).FirstOrDefault().ChildAttributes(elements[10]).FirstOrDefault().Value,
                            Sex = elements[4],
                            Category = elements[5]
                        };
                        if (!product.SiteURL.Contains(website.Url))
                        {
                            product.SiteURL = website.Url + product.SiteURL;
                        }
                        double result;
                        bool isDouble = Double.TryParse(priceStr, out result);
                        if (!isDouble)
                        {
                            string cleanedString = "";
                            foreach (char c in priceStr)
                            {
                                if (Char.IsDigit(c) || c == '.' || c == '-')
                                {
                                    cleanedString += c;
                                }
                            }
                            Double.TryParse(cleanedString, out result);
                            product.Price = (float)result;
                        }
                        string subcat = "";
                        string na = product.Name;
                        if (na.Contains("Cargo") && product.Category == "trousers")
                        {
                            subcat = "cargo trousers";
                        }
                        else if (na.Contains("Chino") && product.Category == "trousers")
                        {
                            subcat = "Chino";
                        }
                        else if (na.Contains("Joggers") && product.Category == "trousers")
                        {
                            subcat = "Joggers";
                        }
                        else if (na.Contains("Leggings") && product.Category == "trousers")
                        {
                            subcat = "Leggings";
                        }
                        else if (na.Contains("Dress") || product.Category == "dresses")
                        {
                            int catindex = 0;
                            string[] cattarr = product.Name.Split();
                            for (int x = 0; x < cattarr.Length; x++)
                            {
                                switch (cattarr[x])
                                {
                                    case "Maxi":
                                        subcat = "Maxi";
                                        break;
                                    case "Midaxi":
                                        subcat = "Midaxi";
                                        break;
                                    case "Midi":
                                        subcat = "Midi";
                                        break;
                                    case "Mini":
                                        subcat = "Mini";
                                        break;
                                    case "Bodycon":
                                        subcat = "Bodycon";
                                        break;
                                    case "Skater":
                                        subcat = "Skater";
                                        break;
                                    case "Shirt":
                                        subcat = "Shirt";
                                        break;
                                    case "Jumpsuit":
                                        subcat = "Jumpsuit";
                                        break;
                                    case "Blazer":
                                        subcat = "Blazer";
                                        break;
                                    case "Jumper":
                                        subcat = "Jumper";
                                        break;
                                    case "Wrap":
                                        subcat = "Wrap";
                                        break;
                                    case "Smock":
                                        subcat = "Smock";
                                        break;
                                    case "T-Shirt":
                                        subcat = "T-Shirt";
                                        break;
                                    case "Cami":
                                        subcat = "Cami";
                                        break;
                                    case "Slip":
                                        subcat = "Slip";
                                        break;
                                    default: break;
                                }
                            }
                        }
                        product.SubCategory = subcat;
                        products.Add(product);
                    }
                    // Connection string for connecting to SQL Server
                    string connectionString = "Data Source=LAPTOP-G5SQI013\\SQLEXPRESS;Initial Catalog=productsdb;Integrated Security=True;"; // Replace <server_name> and <database_name> with your actual server and database names

                    // SQL query to insert a product into the 'products_table'
                    string checkQuery = "SELECT COUNT(*) FROM products_table WHERE Name = @Name AND Category = @Category AND Store = @Store;";

                    // SQL query to insert a product into the 'products_table'
                    string insertQuery = "INSERT INTO products_table (Store, Name, Category, SubCategory, Price, SiteURL, ImageURL, Sex) VALUES (@Store, @Name, @Category, @SubCategory, FORMAT(@Price, 'N2'), @SiteURL,@ImageURL, @Sex)";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {

                            connection.Open();
                        }
                        catch (Exception ex)
                        {
                            // Handle any exceptions that may occur
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        foreach (Product product in products)
                        {
                            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                            {
                                // Set parameter value for the product ID to check if it already exists
                                checkCommand.Parameters.AddWithValue("@Name", product.Name);
                                checkCommand.Parameters.AddWithValue("@Category", product.Category);
                                checkCommand.Parameters.AddWithValue("@Store", website.Name);

                                int count = (int)checkCommand.ExecuteScalar(); // Execute the query and get the result as an integer

                                if (count == 0)
                                {
                                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                    {
                                        // Set parameter values using the properties of the 'Product' object
                                        insertCommand.Parameters.AddWithValue("@Id", product.Id);
                                        insertCommand.Parameters.AddWithValue("@Store", website.Name);
                                        insertCommand.Parameters.AddWithValue("@Name", product.Name);
                                        insertCommand.Parameters.AddWithValue("@Category", product.Category);
                                        insertCommand.Parameters.AddWithValue("@SubCategory", product.SubCategory);
                                        insertCommand.Parameters.AddWithValue("@Price", product.Price);
                                        insertCommand.Parameters.AddWithValue("@SiteURL", product.SiteURL);
                                        insertCommand.Parameters.AddWithValue("@ImageURL", product.ImageURL);
                                        insertCommand.Parameters.AddWithValue("@Sex", product.Sex);

                                        insertCommand.ExecuteNonQuery(); // Execute the query to insert the product into the table
                                    }
                                }
                            }

                        }
                        connection.Close();
                    }
                }
                Console.WriteLine("Finished");
            }

        }
    }
}

