// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.AddressBookParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class AddressBookParsedResult : ParsedResult
  {
    private readonly string[] names;
    private readonly string[] nicknames;
    private readonly string pronunciation;
    private readonly string[] phoneNumbers;
    private readonly string[] phoneTypes;
    private readonly string[] emails;
    private readonly string[] emailTypes;
    private readonly string instantMessenger;
    private readonly string note;
    private readonly string[] addresses;
    private readonly string[] addressTypes;
    private readonly string org;
    private readonly string birthday;
    private readonly string title;
    private readonly string[] urls;
    private readonly string[] geo;

    public AddressBookParsedResult(
      string[] names,
      string[] phoneNumbers,
      string[] phoneTypes,
      string[] emails,
      string[] emailTypes,
      string[] addresses,
      string[] addressTypes)
      : this(names, (string[]) null, (string) null, phoneNumbers, phoneTypes, emails, emailTypes, (string) null, (string) null, addresses, addressTypes, (string) null, (string) null, (string) null, (string[]) null, (string[]) null)
    {
    }

    public AddressBookParsedResult(
      string[] names,
      string[] nicknames,
      string pronunciation,
      string[] phoneNumbers,
      string[] phoneTypes,
      string[] emails,
      string[] emailTypes,
      string instantMessenger,
      string note,
      string[] addresses,
      string[] addressTypes,
      string org,
      string birthday,
      string title,
      string[] urls,
      string[] geo)
      : base(ParsedResultType.AddessBook)
    {
      this.names = names;
      this.nicknames = nicknames;
      this.pronunciation = pronunciation;
      this.phoneNumbers = phoneNumbers;
      this.phoneTypes = phoneTypes;
      this.emails = emails;
      this.emailTypes = emailTypes;
      this.instantMessenger = instantMessenger;
      this.note = note;
      this.addresses = addresses;
      this.addressTypes = addressTypes;
      this.org = org;
      this.birthday = birthday;
      this.title = title;
      this.urls = urls;
      this.geo = geo;
    }

    public string[] GetNames() => this.names;

    public string[] GetNicknames() => this.nicknames;

    public string GetPronunciation() => this.pronunciation;

    public string[] GetPhoneNumbers() => this.phoneNumbers;

    public string[] GetPhoneTypes() => this.phoneTypes;

    public string[] GetEmails() => this.emails;

    public string[] GetEmailTypes() => this.emailTypes;

    public string GetInstantMessenger() => this.instantMessenger;

    public string GetNote() => this.note;

    public string[] GetAddresses() => this.addresses;

    public string[] GetAddressTypes() => this.addressTypes;

    public string GetTitle() => this.title;

    public string GetOrg() => this.org;

    public string[] GetURLs() => this.urls;

    public string GetBirthday() => this.birthday;

    public string[] GetGeo() => this.geo;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(100);
        ParsedResult.MaybeAppend(this.names, result);
        ParsedResult.MaybeAppend(this.nicknames, result);
        ParsedResult.MaybeAppend(this.pronunciation, result);
        ParsedResult.MaybeAppend(this.title, result);
        ParsedResult.MaybeAppend(this.org, result);
        ParsedResult.MaybeAppend(this.addresses, result);
        ParsedResult.MaybeAppend(this.phoneNumbers, result);
        ParsedResult.MaybeAppend(this.emails, result);
        ParsedResult.MaybeAppend(this.instantMessenger, result);
        ParsedResult.MaybeAppend(this.urls, result);
        ParsedResult.MaybeAppend(this.birthday, result);
        ParsedResult.MaybeAppend(this.geo, result);
        ParsedResult.MaybeAppend(this.note, result);
        return result.ToString();
      }
    }
  }
}
