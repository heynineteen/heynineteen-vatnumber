namespace HeyNineteen.VatNumber.Tests
{
    using System;
    using System.Collections;
    using FluentAssertions;
    using NUnit.Framework;

    public class UkVatNumberTests
    {

        [Test]
        public void Parse_NullValue_ThrowsException()
        {
            Action action = () => UkVatNumber.Parse(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Validate_NullValue_ThrowsException()
        {
            Action action = () => UkVatNumber.Validate(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestCaseSource(typeof(VatNumberCases))]
        public void Validate(string input, bool isValid, string shortFormat, string longFormat, string euCompatibleFormat, string type)
        {
            
            UkVatNumber.Validate(input).IsSuccess.Should().Be(isValid);
        }

        [TestCaseSource(typeof(VatNumberCases))]
        public void Parse(
            string input,
            bool isValid,
            string shortFormat,
            string longFormat,
            string euCompatibleFormat,
            string type)
        {
            var result = UkVatNumber.Parse(input);

            result.IsSuccess.Should().Be(isValid);

            if (isValid)
            {
                var vatNumber = result.Value;

                vatNumber.ShortFormat.Should().Be(shortFormat);
                vatNumber.LongFormat.Should().Be(longFormat);
                vatNumber.EuCompatibleFormat.Should().Be(euCompatibleFormat);
                vatNumber.VatNumberType.Name.Should().Be(type);

                Console.WriteLine($"{vatNumber.ShortFormat} - {vatNumber.CheckDigitType}");
            }
        }


    }

    class VatNumberCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            // Standard
            yield return new object[] { "123456782", true, "123456782", "GB123456782", "GB123456782", "Standard" };
            yield return new object[] { " 1  2345    6782  ", true, "123456782", "GB123456782", "GB123456782", "Standard" };
            yield return new object[] { "GB216676689", true, "216676689", "GB216676689", "GB216676689", "Standard" };
            yield return new object[] { "216676689", true, "216676689", "GB216676689", "GB216676689", "Standard" };
            yield return new object[] { "GB234413829", true, "234413829", "GB234413829", "GB234413829", "Standard" };
            yield return new object[] { "234413829", true, "234413829", "GB234413829", "GB234413829", "Standard" };
            yield return new object[] { "GB197701477", true, "197701477", "GB197701477", "GB197701477", "Standard" };
            yield return new object[] { "197701477", true, "197701477", "GB197701477", "GB197701477", "Standard" };
            yield return new object[] { "GB231609921", true, "231609921", "GB231609921", "GB231609921", "Standard" };
            yield return new object[] { "231609921", true, "231609921", "GB231609921", "GB231609921", "Standard" };
            yield return new object[] { "GB122813640", true, "122813640", "GB122813640", "GB122813640", "Standard" };
            yield return new object[] { "122813640", true, "122813640", "GB122813640", "GB122813640", "Standard" };
            yield return new object[] { "GB745064142", true, "745064142", "GB745064142", "GB745064142", "Standard" };
            yield return new object[] { "745064142", true, "745064142", "GB745064142", "GB745064142", "Standard" };
            yield return new object[] { "GB177529567", true, "177529567", "GB177529567", "GB177529567", "Standard" };
            yield return new object[] { "177529567", true, "177529567", "GB177529567", "GB177529567", "Standard" };
            yield return new object[] { "GB355837270", true, "355837270", "GB355837270", "GB355837270", "Standard" };
            yield return new object[] { "355837270", true, "355837270", "GB355837270", "GB355837270", "Standard" };
            yield return new object[] { "GB618419432", true, "618419432", "GB618419432", "GB618419432", "Standard" };
            yield return new object[] { "618419432", true, "618419432", "GB618419432", "GB618419432", "Standard" };
            yield return new object[] { "GB354724980", true, "354724980", "GB354724980", "GB354724980", "Standard" };
            yield return new object[] { "354724980", true, "354724980", "GB354724980", "GB354724980", "Standard" };
            yield return new object[] { "GB825201857", true, "825201857", "GB825201857", "GB825201857", "Standard" };
            yield return new object[] { "825201857", true, "825201857", "GB825201857", "GB825201857", "Standard" };
            yield return new object[] { "GB212365540", true, "212365540", "GB212365540", "GB212365540", "Standard" };
            yield return new object[] { "212365540", true, "212365540", "GB212365540", "GB212365540", "Standard" };
            yield return new object[] { "GB221224171", true, "221224171", "GB221224171", "GB221224171", "Standard" };
            yield return new object[] { "221224171", true, "221224171", "GB221224171", "GB221224171", "Standard" };
            yield return new object[] { "GB302296529", true, "302296529", "GB302296529", "GB302296529", "Standard" };
            yield return new object[] { "302296529", true, "302296529", "GB302296529", "GB302296529", "Standard" };
            yield return new object[] { "GB869977037", true, "869977037", "GB869977037", "GB869977037", "Standard" };
            yield return new object[] { "869977037", true, "869977037", "GB869977037", "GB869977037", "Standard" };
            yield return new object[] { "GB302160522", true, "302160522", "GB302160522", "GB302160522", "Standard" };
            yield return new object[] { "302160522", true, "302160522", "GB302160522", "GB302160522", "Standard" };
            yield return new object[] { "GB900151486", true, "900151486", "GB900151486", "GB900151486", "Standard" };
            yield return new object[] { "900151486", true, "900151486", "GB900151486", "GB900151486", "Standard" };
            yield return new object[] { "GB334180915", true, "334180915", "GB334180915", "GB334180915", "Standard" };
            yield return new object[] { "334180915", true, "334180915", "GB334180915", "GB334180915", "Standard" };
            yield return new object[] { "GB632290655", true, "632290655", "GB632290655", "GB632290655", "Standard" };
            yield return new object[] { "632290655", true, "632290655", "GB632290655", "GB632290655", "Standard" };
            yield return new object[] { "GB188327668", true, "188327668", "GB188327668", "GB188327668", "Standard" };
            yield return new object[] { "188327668", true, "188327668", "GB188327668", "GB188327668", "Standard" };
            yield return new object[] { "GB988525459", true, "988525459", "GB988525459", "GB988525459", "Standard" };
            yield return new object[] { "988525459", true, "988525459", "GB988525459", "GB988525459", "Standard" };
            yield return new object[] { "GB191688269", true, "191688269", "GB191688269", "GB191688269", "Standard" };
            yield return new object[] { "191688269", true, "191688269", "GB191688269", "GB191688269", "Standard" };
            yield return new object[] { "GB561647333", true, "561647333", "GB561647333", "GB561647333", "Standard" };
            yield return new object[] { "561647333", true, "561647333", "GB561647333", "GB561647333", "Standard" };
            yield return new object[] { "GB179530284", true, "179530284", "GB179530284", "GB179530284", "Standard" };
            yield return new object[] { "179530284", true, "179530284", "GB179530284", "GB179530284", "Standard" };
            yield return new object[] { "GB205089034", true, "205089034", "GB205089034", "GB205089034", "Standard" };
            yield return new object[] { "205089034", true, "205089034", "GB205089034", "GB205089034", "Standard" };
            yield return new object[] { "GB125281924", true, "125281924", "GB125281924", "GB125281924", "Standard" };
            yield return new object[] { "125281924", true, "125281924", "GB125281924", "GB125281924", "Standard" };
            yield return new object[] { "GB908955385", true, "908955385", "GB908955385", "GB908955385", "Standard" };
            yield return new object[] { "908955385", true, "908955385", "GB908955385", "GB908955385", "Standard" };
            yield return new object[] { "GB225799666", true, "225799666", "GB225799666", "GB225799666", "Standard" };
            yield return new object[] { "225799666", true, "225799666", "GB225799666", "GB225799666", "Standard" };
            yield return new object[] { "GB160619420", true, "160619420", "GB160619420", "GB160619420", "Standard" };
            yield return new object[] { "160619420", true, "160619420", "GB160619420", "GB160619420", "Standard" };
            yield return new object[] { "GB905181738", true, "905181738", "GB905181738", "GB905181738", "Standard" };
            yield return new object[] { "905181738", true, "905181738", "GB905181738", "GB905181738", "Standard" };
            yield return new object[] { "GB438242355", true, "438242355", "GB438242355", "GB438242355", "Standard" };
            yield return new object[] { "438242355", true, "438242355", "GB438242355", "GB438242355", "Standard" };
            yield return new object[] { "GB828234328", true, "828234328", "GB828234328", "GB828234328", "Standard" };
            yield return new object[] { "828234328", true, "828234328", "GB828234328", "GB828234328", "Standard" };
            yield return new object[] { "GB308100160", true, "308100160", "GB308100160", "GB308100160", "Standard" };
            yield return new object[] { "308100160", true, "308100160", "GB308100160", "GB308100160", "Standard" };
            yield return new object[] { "GB432502686", true, "432502686", "GB432502686", "GB432502686", "Standard" };
            yield return new object[] { "432502686", true, "432502686", "GB432502686", "GB432502686", "Standard" };
            yield return new object[] { "GB248055263", true, "248055263", "GB248055263", "GB248055263", "Standard" };
            yield return new object[] { "248055263", true, "248055263", "GB248055263", "GB248055263", "Standard" };
            yield return new object[] { "GB195861754", true, "195861754", "GB195861754", "GB195861754", "Standard" };
            yield return new object[] { "195861754", true, "195861754", "GB195861754", "GB195861754", "Standard" };
            yield return new object[] { "GB290482100", true, "290482100", "GB290482100", "GB290482100", "Standard" };
            yield return new object[] { "290482100", true, "290482100", "GB290482100", "GB290482100", "Standard" };
            yield return new object[] { "GB102500976", true, "102500976", "GB102500976", "GB102500976", "Standard" };
            yield return new object[] { "102500976", true, "102500976", "GB102500976", "GB102500976", "Standard" };
            yield return new object[] { "GB191981373", true, "191981373", "GB191981373", "GB191981373", "Standard" };
            yield return new object[] { "191981373", true, "191981373", "GB191981373", "GB191981373", "Standard" };
            yield return new object[] { "GB326301243", true, "326301243", "GB326301243", "GB326301243", "Standard" };
            yield return new object[] { "326301243", true, "326301243", "GB326301243", "GB326301243", "Standard" };
            yield return new object[] { "GB369677331", true, "369677331", "GB369677331", "GB369677331", "Standard" };
            yield return new object[] { "369677331", true, "369677331", "GB369677331", "GB369677331", "Standard" };
            yield return new object[] { "GB339424979", true, "339424979", "GB339424979", "GB339424979", "Standard" };
            yield return new object[] { "339424979", true, "339424979", "GB339424979", "GB339424979", "Standard" };
            yield return new object[] { "GB341026796", true, "341026796", "GB341026796", "GB341026796", "Standard" };
            yield return new object[] { "341026796", true, "341026796", "GB341026796", "GB341026796", "Standard" };
            yield return new object[] { "GB803344755", true, "803344755", "GB803344755", "GB803344755", "Standard" };
            yield return new object[] { "803344755", true, "803344755", "GB803344755", "GB803344755", "Standard" };
            yield return new object[] { "GB300230191", true, "300230191", "GB300230191", "GB300230191", "Standard" };
            yield return new object[] { "300230191", true, "300230191", "GB300230191", "GB300230191", "Standard" };
            yield return new object[] { "GB216077031", true, "216077031", "GB216077031", "GB216077031", "Standard" };
            yield return new object[] { "216077031", true, "216077031", "GB216077031", "GB216077031", "Standard" };

            // Branch traders
            yield return new object[] { "123456782123", true, "123456782123", "GB123456782123", "GB123456782123", "BranchTrader" };
            yield return new object[] { " 1  2345    6782  1 2 3", true, "123456782123", "GB123456782123", "GB123456782123", "BranchTrader" };
            yield return new object[] { "GB216676689123", true, "216676689123", "GB216676689123", "GB216676689123", "BranchTrader" };
            yield return new object[] { "216676689123", true, "216676689123", "GB216676689123", "GB216676689123", "BranchTrader" };
            yield return new object[] { "GB234413829123", true, "234413829123", "GB234413829123", "GB234413829123", "BranchTrader" };
            yield return new object[] { "234413829123", true, "234413829123", "GB234413829123", "GB234413829123", "BranchTrader" };
            yield return new object[] { "GB197701477123", true, "197701477123", "GB197701477123", "GB197701477123", "BranchTrader" };
            yield return new object[] { "197701477123", true, "197701477123", "GB197701477123", "GB197701477123", "BranchTrader" };
            yield return new object[] { "GB438242355123", true, "438242355123", "GB438242355123", "GB438242355123", "BranchTrader" };
            yield return new object[] { "438242355123", true, "438242355123", "GB438242355123", "GB438242355123", "BranchTrader" };

            // Government Departments
            yield return new object[] { " 0  0  1 ", true, "GD001", "GBGD001", "GB888800181", "GovernmentDepartment" };
            yield return new object[] { "001", true, "GD001", "GBGD001", "GB888800181", "GovernmentDepartment" };
            yield return new object[] { "GD001", true, "GD001", "GBGD001", "GB888800181", "GovernmentDepartment" };
            yield return new object[] { "499", true, "GD499", "GBGD499", "GB888849922", "GovernmentDepartment" };
            yield return new object[] { "GD499", true, "GD499", "GBGD499", "GB888849922", "GovernmentDepartment" };

            // Government Departments EU
            yield return new object[] { "  GB888 800    18 1   ", true, "GD001", "GBGD001", "GB888800181", "GovernmentDepartment" };
            yield return new object[] { "GB888800181", true, "GD001", "GBGD001", "GB888800181", "GovernmentDepartment" };
            yield return new object[] { "GB888849922", true, "GD499", "GBGD499", "GB888849922", "GovernmentDepartment" };

            // Health Authorities
            yield return new object[] { " 5  0  0 ", true, "HA500", "GBHA500", "GB888850063", "HealthAuthority" };
            yield return new object[] { "500", true, "HA500", "GBHA500", "GB888850063", "HealthAuthority" };
            yield return new object[] { "HA500", true, "HA500", "GBHA500", "GB888850063", "HealthAuthority" };
            yield return new object[] { "999", true, "HA999", "GBHA999", "GB888899902", "HealthAuthority" };
            yield return new object[] { "HA999", true, "HA999", "GBHA999", "GB888899902", "HealthAuthority" };

            // Health Authorities EU
            yield return new object[] { "  GB88 88500   6  3     ", true, "HA500", "GBHA500", "GB888850063", "HealthAuthority" };
            yield return new object[] { "GB888850063", true, "HA500", "GBHA500", "GB888850063", "HealthAuthority" };
            yield return new object[] { "GB888899902", true, "HA999", "GBHA999", "GB888899902", "HealthAuthority" };

            // Failures
            yield return new object[] { "", false, null, null, null, null };                // regex
            yield return new object[] { "foo", false, null, null, null, null };             // regex
            yield return new object[] { "GBfoo", false, null, null, null, null };           // regex

            // Standard - failures
            yield return new object[] { "123b56789", false, null, null, null, null };       // regex
            yield return new object[] { "GB123b56789", false, null, null, null, null };     // regex
            yield return new object[] { "123456783", false, null, null, null, null };       // check digit
            yield return new object[] { "GB123456783", false, null, null, null, null };     // check digit
            yield return new object[] { "012345620", false, null, null, null, null };       // regex
            yield return new object[] { "GB012345620", false, null, null, null, null };     // regex

            // Branch traders - failures
            yield return new object[] { "GB216b76689123", false, null, null, null, null };  // regex
            yield return new object[] { "2166b76689123", false, null, null, null, null };   // regex
            yield return new object[] { "GB216676680123", false, null, null, null, null };  // check-digit
            yield return new object[] { "216676680123", false, null, null, null, null };    // check-digit
            yield return new object[] { "GB012345620123", false, null, null, null, null };  // regex
            yield return new object[] { "012345620123", false, null, null, null, null };    // regex

            // Government Departments - failures
            yield return new object[] { "GD500", false, null, null, null, null };
            yield return new object[] { "HA001", false, null, null, null, null };

            // Government Departments EU - failures
            yield return new object[] { "GB888800182", false, null, null, null, null };
            yield return new object[] { "GB888849923", false, null, null, null, null };

            // Health Authorities - failures
            yield return new object[] { "HA499", false, null, null, null, null };
            yield return new object[] { "GD500", false, null, null, null, null };

            // Health Authorities EU - failures
            yield return new object[] { "GB888850064", false, null, null, null, null };
            yield return new object[] { "GB888899905", false, null, null, null, null };
        }
    }
}
