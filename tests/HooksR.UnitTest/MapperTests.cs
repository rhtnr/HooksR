using AutoMapper;
using HooksR.Service;
using System;
using Xunit;

namespace HooksR.UnitTest
{
  public class MapperTests
  {
    [Fact]
    public void Test_Valid_Configuration()
    {
      MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

      configuration.AssertConfigurationIsValid();
    }
  }
}
