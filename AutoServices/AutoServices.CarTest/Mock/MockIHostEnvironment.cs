﻿using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace AutoServices.CarTest.Mock
{
    public class MockIHostEnvironment : IHostEnvironment
    {
        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
