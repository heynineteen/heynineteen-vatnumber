
# HeyNineteen.VatNumber

A library for validating and creating instances of Uk VAT numbers in a type safe manner.

Based on information at:

https://en.wikipedia.org/wiki/VAT_identification_number
https://discover.hubpages.com/business/Check-VAT-Numbers-UK


The library is capable of parsing string representations of the 4 types of VAT numbers used in the UK:
- standard: 9 digits (block of 3, block of 4, block of 2 – e.g. GB999 9999 73)
- branch traders: 12 digits (as for 9 digits, followed by a block of 3 digits)
- government departments: the letters GD then 3 digits from 000 to 499 (e.g. GBGD001)
- health authorities: the letters HA then 3 digits from 500 to 999 (e.g. GBHA599)

(Source: https://en.wikipedia.org/wiki/VAT_identification_number)

Once parsed, a UkVatNumber exposes the following properties

- ShortFormat e.g. `"123456783"`, `"123456783123"`, `"001"`
- LongFormat e.g. `"GB123456783"`, `"GB123456783123"`, `"GBGD001"`
- EuCompatibleFormat e.g. `"GB888800181"`
- CheckDigitType - the method used to calculate the check digits in the number. Either `Modulos97` or `Modulos9755`
- VatNumberType - either `Standard`, `BranchTrader`, `GovernmentDepartment`, `HealthAuthority`.

## Usage/Examples

### Check validity
```csharp 
var vatNumber = "123456783";
var result = UkVatNumber.Validate("123456783");

if(result.IsSuccess)
    Console.WriteLine($"'{vatNumber}' is a valid Uk Vat number.");
else
    Console.WriteLine($"'{vatNumber}' is not a valid Uk Vat number. {result.Message}");
```

### Create an instance
```csharp 
var vatNumber = "123456783";
var result = UkVatNumber.Create("123456783");

if(result.IsSuccess)
{
    Console.WriteLine($"'{vatNumber}' is a valid Uk Vat number.");

    var instance = result.Value;
    Console.WriteLIne($"Short format: {instance.ShortFormat}");
    Console.WriteLIne($"Long format: {instance.LongFormat}");
    Console.WriteLIne($"EU compatible format: {instance.EuCompatibleFormat}");
    Console.WriteLIne($"Type: {instance.VatNumberType.Name}");
    Console.WriteLIne($"Check digit type: {instance.CheckDigitType.Name}");
}
else
{
    Console.WriteLine($"'{vatNumber}' is not a valid Uk Vat number. {result.Message}");
}
```
