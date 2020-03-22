﻿using AngularJSCore.Models;
using AngularJSCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlLinq;
using System.Linq;
using AngularJSCore.Helpers;

namespace AngularJSCore.Controllers
{
    public class DefaultController : Controller
    {
        public DefaultController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(new QueryResult());
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] QueryModel model)
        {
            var result = new QueryResult
            {
                Query = model.Query,
            };

            if (string.IsNullOrEmpty(model.Query))
            {
                ViewBag.ErrorMessage = "Please enter query.";
                return View(result);
            }

            var data = ParsingQueryHelpers.GetFilteredData(Addresses, CustomerAddresses, model.Query);

            ViewBag.ErrorMessage = "";
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Index_Backup([FromForm] QueryModel model)
        {
            var result = Addresses.Query<Address, dynamic>(model.Query).ToList();

            return View(new QueryResult
            {
                Query = model.Query,
                Addresses = result
            });
        }

        private readonly MyContext context;

        private List<Address> Addresses = new List<Address>
        {
            new Address
            {
                AddressID = 1,
                AddressLine1 = "1-AddressLine1",
                AddressLine2 = "1-AddressLine2",
                City = "1-City",
                CountryRegion = "1-CountryRegion",
                PostalCode = "1-PostalCode",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),
                StateProvince = "1-StateProvince"
            },
            new Address
            {
                AddressID = 2,
                AddressLine1 = "2-AddressLine1",
                AddressLine2 = "2-AddressLine2",
                City = "2-City",
                CountryRegion = "2-CountryRegion",
                PostalCode = "2-PostalCode",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),
                StateProvince = "2-StateProvince"
            }
        };

        private List<CustomerAddress> CustomerAddresses = new List<CustomerAddress>
        {
            new CustomerAddress
            {
                AddressID = 1,
                CustomerID = 1,
                AddressType = "Home",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            },
            new CustomerAddress
            {
                AddressID = 1,
                CustomerID = 2,
                AddressType = "Work",
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            }
        };

    }
}