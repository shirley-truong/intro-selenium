﻿using System;
using OpenQA.Selenium;

namespace FluentPageObjectExample.PageObjects
{
    public abstract class PageObject
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUriString;

        protected PageObject(IWebDriver driver, string baseUriString)
        {
            _driver = driver;
            _baseUriString = baseUriString;
        }

        protected IWebDriver Driver
        {
            get { return _driver; }
        }

        protected string BaseUriString
        {
            get { return _baseUriString; }
        }

        /// <summary>
        /// Absolute path to the current page.
        /// </summary>
        protected abstract string AbsolutePath { get; }

        /// <summary>
        /// Default page query.
        /// </summary>
        protected virtual string Query
        {
            get { return ""; }
        }

        /// <summary>
        /// Checks the Driver's current AbsolutePath against the PageObject's AbsolutePath
        /// </summary>
        /// <returns>True if the Driver's current AbsolutePath matches the PageObject's AbsolutePath, false otherwise.</returns>
        public virtual bool IsCurrentPath()
        {
            var currentUri = new Uri(_driver.Url);
            return currentUri.AbsolutePath.ToLower() == AbsolutePath.ToLower();
        }

        protected void ValidatePath()
        {
            if (!IsCurrentPath())
            {
                var currentUrl = new Uri(_driver.Url);
                throw new InvalidOperationException(String.Format("Expected current absolute path {0} but was {1}", AbsolutePath,
                                                                  currentUrl.AbsolutePath));
            }
        }
    }
}