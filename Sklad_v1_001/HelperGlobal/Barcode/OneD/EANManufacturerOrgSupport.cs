// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EANManufacturerOrgSupport
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  internal sealed class EANManufacturerOrgSupport
  {
    private readonly List<int[]> ranges;
    private readonly List<string> countryIdentifiers;

    public EANManufacturerOrgSupport()
    {
      this.ranges = new List<int[]>();
      this.countryIdentifiers = new List<string>();
    }

    internal string LookupCountryIdentifier(string productCode)
    {
      this.InitIfNeeded();
      int num1 = int.Parse(productCode.Substring(0, 3));
      int count = this.ranges.Count;
      for (int index = 0; index < count; ++index)
      {
        int[] range = this.ranges[index];
        int num2 = range[0];
        if (num1 < num2)
          return (string) null;
        int num3 = range.Length == 1 ? num2 : range[1];
        if (num1 <= num3)
          return this.countryIdentifiers[index];
      }
      return (string) null;
    }

    private void Add(int[] range, string id)
    {
      this.ranges.Add(range);
      this.countryIdentifiers.Add(id);
    }

    private void InitIfNeeded()
    {
      if (this.ranges.Count != 0)
        return;
      this.Add(new int[2]{ 0, 19 }, "US/CA");
      this.Add(new int[2]{ 30, 39 }, "US");
      this.Add(new int[2]{ 60, 139 }, "US/CA");
      this.Add(new int[2]{ 300, 379 }, "FR");
      this.Add(new int[1]{ 380 }, "BG");
      this.Add(new int[1]{ 383 }, "SI");
      this.Add(new int[1]{ 385 }, "HR");
      this.Add(new int[1]{ 387 }, "BA");
      this.Add(new int[2]{ 400, 440 }, "DE");
      this.Add(new int[2]{ 450, 459 }, "JP");
      this.Add(new int[2]{ 460, 469 }, "RU");
      this.Add(new int[1]{ 471 }, "TW");
      this.Add(new int[1]{ 474 }, "EE");
      this.Add(new int[1]{ 475 }, "LV");
      this.Add(new int[1]{ 476 }, "AZ");
      this.Add(new int[1]{ 477 }, "LT");
      this.Add(new int[1]{ 478 }, "UZ");
      this.Add(new int[1]{ 479 }, "LK");
      this.Add(new int[1]{ 480 }, "PH");
      this.Add(new int[1]{ 481 }, "BY");
      this.Add(new int[1]{ 482 }, "UA");
      this.Add(new int[1]{ 484 }, "MD");
      this.Add(new int[1]{ 485 }, "AM");
      this.Add(new int[1]{ 486 }, "GE");
      this.Add(new int[1]{ 487 }, "KZ");
      this.Add(new int[1]{ 489 }, "HK");
      this.Add(new int[2]{ 490, 499 }, "JP");
      this.Add(new int[2]{ 500, 509 }, "GB");
      this.Add(new int[1]{ 520 }, "GR");
      this.Add(new int[1]{ 528 }, "LB");
      this.Add(new int[1]{ 529 }, "CY");
      this.Add(new int[1]{ 531 }, "MK");
      this.Add(new int[1]{ 535 }, "MT");
      this.Add(new int[1]{ 539 }, "IE");
      this.Add(new int[2]{ 540, 549 }, "BE/LU");
      this.Add(new int[1]{ 560 }, "PT");
      this.Add(new int[1]{ 569 }, "IS");
      this.Add(new int[2]{ 570, 579 }, "DK");
      this.Add(new int[1]{ 590 }, "PL");
      this.Add(new int[1]{ 594 }, "RO");
      this.Add(new int[1]{ 599 }, "HU");
      this.Add(new int[2]{ 600, 601 }, "ZA");
      this.Add(new int[1]{ 603 }, "GH");
      this.Add(new int[1]{ 608 }, "BH");
      this.Add(new int[1]{ 609 }, "MU");
      this.Add(new int[1]{ 611 }, "MA");
      this.Add(new int[1]{ 613 }, "DZ");
      this.Add(new int[1]{ 616 }, "KE");
      this.Add(new int[1]{ 618 }, "CI");
      this.Add(new int[1]{ 619 }, "TN");
      this.Add(new int[1]{ 621 }, "SY");
      this.Add(new int[1]{ 622 }, "EG");
      this.Add(new int[1]{ 624 }, "LY");
      this.Add(new int[1]{ 625 }, "JO");
      this.Add(new int[1]{ 626 }, "IR");
      this.Add(new int[1]{ 627 }, "KW");
      this.Add(new int[1]{ 628 }, "SA");
      this.Add(new int[1]{ 629 }, "AE");
      this.Add(new int[2]{ 640, 649 }, "FI");
      this.Add(new int[2]{ 690, 695 }, "CN");
      this.Add(new int[2]{ 700, 709 }, "NO");
      this.Add(new int[1]{ 729 }, "IL");
      this.Add(new int[2]{ 730, 739 }, "SE");
      this.Add(new int[1]{ 740 }, "GT");
      this.Add(new int[1]{ 741 }, "SV");
      this.Add(new int[1]{ 742 }, "HN");
      this.Add(new int[1]{ 743 }, "NI");
      this.Add(new int[1]{ 744 }, "CR");
      this.Add(new int[1]{ 745 }, "PA");
      this.Add(new int[1]{ 746 }, "DO");
      this.Add(new int[1]{ 750 }, "MX");
      this.Add(new int[2]{ 754, 755 }, "CA");
      this.Add(new int[1]{ 759 }, "VE");
      this.Add(new int[2]{ 760, 769 }, "CH");
      this.Add(new int[1]{ 770 }, "CO");
      this.Add(new int[1]{ 773 }, "UY");
      this.Add(new int[1]{ 775 }, "PE");
      this.Add(new int[1]{ 777 }, "BO");
      this.Add(new int[1]{ 779 }, "AR");
      this.Add(new int[1]{ 780 }, "CL");
      this.Add(new int[1]{ 784 }, "PY");
      this.Add(new int[1]{ 785 }, "PE");
      this.Add(new int[1]{ 786 }, "EC");
      this.Add(new int[2]{ 789, 790 }, "BR");
      this.Add(new int[2]{ 800, 839 }, "IT");
      this.Add(new int[2]{ 840, 849 }, "ES");
      this.Add(new int[1]{ 850 }, "CU");
      this.Add(new int[1]{ 858 }, "SK");
      this.Add(new int[1]{ 859 }, "CZ");
      this.Add(new int[1]{ 860 }, "YU");
      this.Add(new int[1]{ 865 }, "MN");
      this.Add(new int[1]{ 867 }, "KP");
      this.Add(new int[2]{ 868, 869 }, "TR");
      this.Add(new int[2]{ 870, 879 }, "NL");
      this.Add(new int[1]{ 880 }, "KR");
      this.Add(new int[1]{ 885 }, "TH");
      this.Add(new int[1]{ 888 }, "SG");
      this.Add(new int[1]{ 890 }, "IN");
      this.Add(new int[1]{ 893 }, "VN");
      this.Add(new int[1]{ 896 }, "PK");
      this.Add(new int[1]{ 899 }, "ID");
      this.Add(new int[2]{ 900, 919 }, "AT");
      this.Add(new int[2]{ 930, 939 }, "AU");
      this.Add(new int[2]{ 940, 949 }, "AZ");
      this.Add(new int[1]{ 955 }, "MY");
      this.Add(new int[1]{ 958 }, "MO");
    }
  }
}
