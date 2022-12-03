using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Model;

namespace Northwind.Web.Tests
{
    [TestClass]
    public class AcceptanceTesting
    {
        HttpClient client = new() { BaseAddress = new Uri("http://localhost:5000") };


        TestInstanceCategory currentCategory = new TestInstanceCategory();

        [TestMethod]
        public async Task Create_ShouldReturnView_WithEmptyForm_And_WithLinks()
        {
            var response = await client.GetStringAsync("/categories/create");
            var document = GetDocument(response);

            var category = GetCategoryFromDocument(document);
            var links = document.Links.OfType<IHtmlAnchorElement>()
                .Select(l => l.Href)
                .Where(l => l.EndsWith("/Categories"));

            category.Should().BeEquivalentTo(new Category()
            {
                CategoryName = string.Empty,
                Description = string.Empty,
            });

            links.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnView_WithCategoryInfo_And_WithProductList_And_WithLinks_WhenIdIsValid()
        {

            var response = await client.GetStringAsync($"/categories/delete/1");
            var document = GetDocument(response);

            var category = GetCategory(document);
            var products = GetProducts(document);
            var links = document.Links.OfType<IHtmlAnchorElement>()
                .Select(l => l.Href)
                .Where(l => l.EndsWith("/Categories"));

            category.Should().BeEquivalentTo(currentCategory,
                options => options
                    .Including(c => c.CategoryName)
                    .Including(c => c.Description));

            products.Should().BeEquivalentTo(currentCategory.Products,
                options => options
                    .Including(c => c.ProductName));

            links.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnNotFoundPage_WhenIdIsInvalid()
        {
            var response = await client.GetAsync("/categories/delete/-1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnNotFoundPage_WhenIdIsNull()
        {
            var response = await client.GetAsync("/categories/delete/");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Details_ShouldReturnView_With_CategoryInfo_And_ProductList_And_Links_WhenIdIsValid()
        {

            var response = await client
                .GetStringAsync($"/categories/Details/1");

            var document = GetDocument(response);

            var category = GetCategory(document);
            var products = GetProducts(document);
            var links = document.Links.OfType<IHtmlAnchorElement>()
                .Select(l => l.Href).Where(l => l.EndsWith("/Categories"));

            category.Should().BeEquivalentTo(currentCategory,
                options => options
                    .Including(c => c.CategoryName)
                    .Including(c => c.Description));

            products.Should().BeEquivalentTo(currentCategory.Products,
                options => options
                    .Including(c => c.ProductName));

            links.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task Details_ShouldReturnNotFoundPage_WhenIdIsInvalid()
        {
            var response = await client.GetAsync("/categories/Details/-1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Details_ShouldReturnNotFoundPage_WhenIdIsNull()
        {
            var response = await client.GetAsync("/categories/Details/");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Edit_ShouldReturnView_WithFilledFieldsOfForm_And_Links_WhenIdIsValid()
        {

            var response = await client
                .GetStringAsync($"/categories/edit/1");

            var document = GetDocument(response);
            var category = GetCategoryFromDocument(document);
            var links = document.Links.OfType<IHtmlAnchorElement>()
                .Select(l => l.Href).Where(l => l.EndsWith("/Categories"));

            category.Should().BeEquivalentTo(currentCategory,
                options => options
                    .Including(c => c.CategoryName)
                    .Including(c => c.Description));


            links.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task Edit_ShouldReturnNotFoundPage_WhenIdIsInvalid()
        {
            var response = await client.GetAsync("/categories/Edit/-1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Edit_ShouldReturnNotFoundPage_WhenIdIsNull()
        {
            var response = await client.GetAsync("/categories/Edit/");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Home_ShouldReturnView_WithLinks()
        {
            var response = await client.GetStringAsync("/");
            var document = GetDocument(response);

            var links = document.Links.OfType<IHtmlAnchorElement>()
                .Select(l => l.Href).ToList();

            links.Where(l => l.EndsWith("/")).Should().NotBeEmpty();
            links.Where(l => l.Contains("Categories")).Should().NotBeEmpty();
        }

        private static IDocument GetDocument(string htmlSource)
        {
            return BrowsingContext.New(Configuration.Default)
                .OpenAsync(req => req.Content(htmlSource)).Result;
        }

        private static IEnumerable<Category> GetCategories(IDocument document)
        {
            foreach (var categoryRow in document.QuerySelectorAll("tr[data-tid|='category-row']"))
            {
                var id = categoryRow.GetAttribute("data-tid")?.Split("-").Last();
                var name = categoryRow.QuerySelector("td[data-tid='category-name']")?.Text().Trim();
                var description = categoryRow.QuerySelector("td[data-tid='category-description']")?.Text().Trim();

                yield return new Category
                {
                    CategoryId = int.Parse(id ?? "-1"),
                    CategoryName = name ?? string.Empty,
                    Description = description,
                    Picture = null
                };
            }
        }

        private static IEnumerable<Product> GetProducts(IDocument document)
        {
            foreach (var product in document.QuerySelectorAll("p[data-tid|='product-name']"))
            {
                yield return new Product
                {
                    ProductName = product.Text().Trim() ?? string.Empty
                };
            }
        }

        private static Category GetCategory(IDocument document)
        {
            return new Category
            {
                CategoryName = document.QuerySelector("dd[data-tid='category-name']")?
                    .Text().Trim() ?? string.Empty,
                Description = document.QuerySelector("dd[data-tid=\"category-description\"]")?
                    .Text().Trim(),
                Picture = null,
            };
        }

        private static Category GetCategoryFromDocument(IDocument document)
        {
            return new Category
            {
                CategoryName = document.QuerySelector("input[data-tid='category-name']")?
                    .GetAttribute("value")?.Trim() ?? string.Empty,
                Description = document.QuerySelector("input[data-tid='category-description']")?
                    .GetAttribute("value")?.Trim(),
                Picture = null
            };
        }
    }
}
