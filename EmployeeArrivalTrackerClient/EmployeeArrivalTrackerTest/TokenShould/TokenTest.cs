using Common.Exceptions;
using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.DbManager;
using EmployeeArrivalTrackerDomain.Application;
using EmployeeArrivalTrackerTest.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace EmployeeArrivalTrackerTest.TokenShould
{
    [TestClass]
    public class TokenTests
    {
        private const string token = "f16149b14cba46be92da20743b97a2f2";

        [TestMethod]
        public void GetTokeIfExistValid_Test()
        {
            var tokenData = TokensGenerator.GenerateTokenTable();

            var mockDbManager = new Mock<ITokenDbManager>();

            mockDbManager
                .Setup(x => x.GetToken(token))
                .Returns(tokenData);

            var manager = new TokenManager(mockDbManager.Object);

            bool sut = manager.GetTokenIfExist(token);
            Assert.IsTrue(sut);
        }

        [TestMethod]
        public void GetTokeIfExistNotValid_Test()
        {
            string notValidToken = $"{token}-23dss";
            var tokenData = TokensGenerator.GenerateTokenTable();
            var mockDbManager = new Mock<ITokenDbManager>();

            mockDbManager
                .Setup(x => x.GetToken(token))
                .Returns(tokenData);

            var manager = new TokenManager(mockDbManager.Object);
            
            bool sut = manager.GetTokenIfExist(notValidToken);
            Assert.IsFalse(sut);
        }

        [TestMethod]
        public void AddTokenDataValid_Test()
        {
            var mockDbManager = new Mock<ITokenDbManager>();
            string token = TokensGenerator.GenerateTokenModel();

            var manager = new TokenManager(mockDbManager.Object);

            bool sut = manager.AddTokenData(token);
            Assert.IsTrue(sut);
        }

        [TestMethod]
        public void AddTokenDataNotValid_Test()
        {
            var mockDbManager = new Mock<ITokenDbManager>();

            var manager = new TokenManager(mockDbManager.Object);

            bool sut = manager.AddTokenData(null);
            Assert.IsFalse(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(TokenException))]
        public void ThrowException_IfTokenIsNull_Test()
        {
            var mockDbMngr = new Mock<ITokenDbManager>();
            string token = TokensGenerator.GenerateTokenModelWithNullToken();

            var sut = new TokenManager(mockDbMngr.Object);

            sut.AddTokenData(token);
        }

        [TestMethod]
        public void AddTokenDB_Test()
        {
            var mockDbMngr = new Mock<ITokenDbManager>();
            var token = TokensGenerator.GenerateTokenTable();

            var options = TestUtilities.GetOptions(nameof(AddTokenDB_Test));

            using (var context = new EmployeeArrivalContext(options))
            {
                var dbManager = new TokenDbManager(context);
                dbManager.AddToken(token);

                var sut = context.Tokens.Count();

                Assert.AreEqual(sut, 1);
            };
        }

        [TestMethod]
        public void GetTokenDB_Test()
        {

            var mockDbMngr = new Mock<ITokenDbManager>();
            var tokenData = TokensGenerator.GenerateTokenTable();

            var options = TestUtilities.GetOptions(nameof(GetTokenDB_Test));

            using (var context = new EmployeeArrivalContext(options))
            {
                var dbManager = new TokenDbManager(context);
                dbManager.AddToken(tokenData);

                var sut = dbManager.GetToken(token);

                Assert.IsNotNull(sut);
            };
        }
    }
}
