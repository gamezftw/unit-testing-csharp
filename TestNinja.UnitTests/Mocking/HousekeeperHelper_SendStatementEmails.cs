using NUnit.Framework;

namespace TestNinja.UnitTests.Mocking
{
    public class HousekeeperHelper_SendStatementEmails
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperService _service;
        private Housekeeper _houseKeeper;
        private DateTime _statementDate = new DateTime(2020, 1, 1);
        private string _statementFileName;
        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            _service = new HousekeeperService(
                _statementGenerator.Object,
                _emailSender.Object,
                _unitOfWork.Object,
                _messageBox.Object
            );
            _houseKeeper = new Housekeeper
            {
                Email = "emal@address.com",
                FullName = "fullname",
                Oid = 1,
                StatementEmailBody = "statement"
            };
            _unitOfWork.Setup(hr => hr.Query<Housekeeper>()).Returns(new List<Housekeeper>{
                _houseKeeper
            }.AsQueryable());

            _statementFileName = "filename";
            _statementGenerator
                .Setup(sg =>
                    sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);
        }

        [Test]
        public void WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void HousekeeperEmailIsNullOrWhitespace_ShouldNotGenerateStatement(string email)
        {
            _houseKeeper.Email = email;
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void WhenCalled_SendEmail()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void StatementFileNameIsNullOrWhitespace_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementFileName = statementFileName;
            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es =>
                es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es =>
                es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ), Times.Never);
        }

        [Test]
        public void EmailSendingThrosException_MessageBoxShowIsCalled()
        {
            SendFileThrowsException();

            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(xmb => xmb.Show(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    MessageBoxButtons.OK)
                , Times.Once);

        }

        private void SendFileThrowsException()
        {
            _emailSender.Setup(
                es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )).Throws<Exception>();
        }
    }
}