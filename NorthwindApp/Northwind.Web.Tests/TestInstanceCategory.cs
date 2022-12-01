using Northwind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Web.Tests.TestDataGenerators;

namespace Northwind.Web.Tests
{
    internal class TestInstanceCategory
    {
        public TestInstanceCategory()
        {
            var context = new NorthwindContext();
            var productGenerator = new ProductGenerator(context);
            string[] pNames =
            {
                "Chai",
                "Chang",
                "Guaraná Fantástica",
                "Sasquatch Ale",
                "Steeleye Stout",
                "Côte de Blaye",
                "Chartreuse verte",
                "Ipoh Coffee",
                "Laughing Lumberjack Lager",
                "Outback Lager",
                "Rhönbräu Klosterbier",
                "Lakkalikööri"
            };
            Products = productGenerator.Generate(12).ToList();
            int i = 0;
            foreach (var product in Products)
            {
                product.ProductName = pNames[i];
                i++;
            }
        }
        public string CategoryName = "Beverages";
        public string Description = "Soft drinks, coffees, teas, beers, and ales";
        public ICollection<Product> Products; 
    }
}
//string[] pNames =
//                { "Generic Wooden Computer",
//            "Fantastic Cotton Table",
//            "Ergonomic Fresh Pants",
//            "Sleek Frozen Soap",
//            "Licensed Plastic Bike",
//            "Rustic Frozen Computer",
//            "Fantastic Steel Fish",
//            "Rustic Granite Fish",
//            "Licensed Metal Hat",
//            "Licensed Metal Keyboard",
//            "Small Soft Bike",
//            "Handmade Granite Hat"};