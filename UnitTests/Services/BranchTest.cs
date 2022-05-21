using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace UnitTests.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BranchTest : BeforeAfterTestAttribute
    {
        private readonly BranchManager _branchManager;
        private readonly Mock<IBranchDal> _mockBranchDal = new Mock<IBranchDal>();

        public BranchTest()
        {
            _branchManager = new BranchManager(_mockBranchDal.Object);
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            Debug.WriteLine(methodUnderTest.Name);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            Debug.WriteLine(methodUnderTest.Name);
        }

        [Fact]
        public void AddTest()
        {
            // Arange
            Branch newBranch = new Branch()
            {
                Name = "Deneme",
                Status = true
            };
            bool expected = true;
            // Act
            bool result = _branchManager.Add(newBranch).Success;
            // Assert
            Assert.Equal(expected, result);
        }
    }
}
