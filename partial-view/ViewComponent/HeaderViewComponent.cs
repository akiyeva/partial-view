﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using partial_view.DataAccessLayer;
using partial_view.Models;

namespace partial_view.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    private readonly AppDbContext _dbContext;

    public HeaderViewComponent(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ViewViewComponentResult> InvokeAsync()
    {
        var basketInString = Request.Cookies["basket"];

        if (string.IsNullOrEmpty(basketInString))
        {
            return View(new List<BasketViewModel>());
        }

        var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketInString);

        var newBasketViewModels = new List<BasketViewModel>();
        foreach (var product in basketViewModels)
        {
            var existProduct = _dbContext.Products.Find(product.ProductId);

            if (existProduct is null) continue;

            newBasketViewModels.Add(new BasketViewModel()
            {
                ProductId = existProduct.Id,
                ImageUrl = existProduct.ImageUrl,
                Price = existProduct.Price,
                Name = existProduct.Name,
                Count = product.Count
            });
        }

        double totalPrice = newBasketViewModels.Sum(x => x.Price * x.Count);
        int count = newBasketViewModels.Sum(x => x.Count);
        HeaderViewModel headerViewModel = new HeaderViewModel()
        {
            BasketViewModels = basketViewModels,
            TotalPrice = totalPrice,
            Count = count
        };

        return View(headerViewModel);

    }
}