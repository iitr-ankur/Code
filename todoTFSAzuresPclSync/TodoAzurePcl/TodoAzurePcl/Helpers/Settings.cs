// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace TodoAzurePcl.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string NameKey = "name_key";
    private const string PhoneKey = "phone_key";
    private const string IdKey = "Id_key";
    private const string IsRegisteredKey = "isRegistered_key";
    private static readonly string StringDefault = string.Empty;
    private static readonly bool IsRegisteredDefault = false;

    #endregion


    public static string Name
    {
        get
        {
            return AppSettings.GetValueOrDefault(NameKey, StringDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(NameKey, value);
        }
    }

    public static string Phone
    {
        get
        {
            return AppSettings.GetValueOrDefault(PhoneKey, StringDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(PhoneKey, value);
        }
    }

    public static string Id
    {
        get
        {
            return AppSettings.GetValueOrDefault(IdKey, StringDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(IdKey, value);
        }
    }

    public static bool IsRegistered
    {
        get
        {
            return AppSettings.GetValueOrDefault(IsRegisteredKey, IsRegisteredDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(IsRegisteredKey, value);
        }
    }

  }
}