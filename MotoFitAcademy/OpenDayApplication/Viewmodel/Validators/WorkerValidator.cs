using System.Text.RegularExpressions;
using OpenDayApplication.Model;

namespace OpenDayApplication.Viewmodel.Validators
{
  public static class WorkerValidator
  {
    public static bool Perform(Worker worker)
    {
      return IsNameValid(worker.Name) && IsSurnameValid(worker.Surname);
    }

    private static bool IsSurnameValid(string surname)
    {
      return IsStringValid(surname);
    }

    private static bool IsStringValid(string workerData)
    {
      return !(string.IsNullOrEmpty(workerData) || string.IsNullOrWhiteSpace(workerData)) && Regex.IsMatch(workerData, @"^[a-zA-Z]+$");
    }

    private static bool IsNameValid(string name)
    {
      return IsStringValid(name);
    }
  }
}
