using Moq;
using Prism.Services.Dialogs;
using PSamples.ViewModels;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new Mock<IDialogService>();
            var vm = new ViewAViewModel(mock.Object);

            mock.Setup(x => x.ShowDialog(
                It.IsAny<string>(),
                It.IsAny<IDialogParameters>(),
                It.IsAny<Action<IDialogResult>>()
                )).Callback<
                    string,
                    IDialogParameters,
                    Action<IDialogResult>
                    >
                ((viewName, p, result) =>
                {
                    Assert.AreEqual("ViewB", viewName);
                });
            vm.OKButton.Execute();
        }
    }
}