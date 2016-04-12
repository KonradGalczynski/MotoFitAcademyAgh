using NUnit.Framework;
using OpenDayApplication.Model;
using OpenDayApplication.Viewmodel.Validators;

namespace MotoFitAcademyTests
{
  [TestFixture]
  public class WorkerValidatorTests
  {
    [TestCase(null)]
    [TestCase("")]
    [TestCase("21456")]
    [TestCase("#$%^&")]
    public void ShouldValidateWorkerWithIncorrectNameToFalse(string name)
    {
      var worker = new Worker() {ID = 0, Name = name, Surname = "ValidSurname"};
      Assert.That(WorkerValidator.Perform(worker), Is.False);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("21456")]
    [TestCase("#$%^&")]
    public void ShouldValidateWorkerWithIncorrectSurnameToFalse(string surname)
    {
      var worker = new Worker() { ID = 0, Name = "ValidName", Surname = surname};
      Assert.That(WorkerValidator.Perform(worker), Is.False);
    }

    [Test]
    public void ShouldValidateWorkerWithCorrectDataToTrue()
    {
      var worker = new Worker() { ID = 0, Name = "ValidName", Surname = "ValidSurname"};
      Assert.That(WorkerValidator.Perform(worker), Is.True);
    }
  }
}
