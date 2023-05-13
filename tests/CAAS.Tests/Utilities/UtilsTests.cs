using CAAS.Exceptions;
using CAAS.Utilities;

namespace CAAS.Tests.Utilities
{
    public class UtilsTests
    {
        [Theory]
        [InlineData("Hello World", "SGVsbG8gV29ybGQ=")]
        public void StringAndBase64Tests(string plain, string encoded)
        {
            string encodedData = Utils.EncodeBase64(plain);
            string decodedData = Utils.DecodeBase64(encodedData);
            Assert.Equal(encodedData, encoded);
            Assert.Equal(decodedData, plain);
        }

        [Fact]
        public void GetNowTests()
        {
            string dateTime = Utils.GetNow();
            DateTime now = DateTime.Now;

            Assert.StartsWith("[", dateTime);
            Assert.EndsWith("]", dateTime);

            dateTime = dateTime.Replace("[", "");
            dateTime = dateTime.Replace("]", "");

            List<string> datePart = dateTime.Split(" ")[0].Split("-").ToList();
            List<string> timePart = dateTime.Split(" ")[1].Split(":").ToList();
            Assert.Equal(datePart[0], now.ToString("yyyy"));
            Assert.Equal(datePart[1], now.ToString("MM"));
            Assert.Equal(datePart[2], now.ToString("dd"));
            Assert.Equal(timePart[0], now.ToString("HH"));
            Assert.Equal(timePart[1], now.ToString("mm"));
        }

        [Theory]
        [InlineData("00112233445566778899AABBCCDDEEFF")]
        public void EvenHexStringAndByteArrayTests(string hex)
        {
            byte[] byteArrayData = Utils.HexStringToByteArray(hex);
            string hexData = Utils.ByteArrayToHexString(byteArrayData);
            Assert.Equal(hexData, hex);
        }

        [Theory]
        [InlineData("0112233445566778899AABBCCDDEEFF")]
        public void OddHexStringAndByteArrayTests(string hex)
        {
            byte[] byteArrayData = Utils.HexStringToByteArray(hex);
            string hexData = Utils.ByteArrayToHexString(byteArrayData);
            Assert.Equal(hexData, "0" + hex);
        }

        [Theory]
        [InlineData("Hello World", "SGVsbG8gV29ybGQ=")]
        public void ByteArrayAndBase64Tests(string plain, string encoded)
        {
            byte[] byteArrayData = Utils.StringToByteArray(plain);
            string encodedData = Utils.ByteArrayToBase64String(byteArrayData);
            Assert.Equal(encodedData, encoded);
        }

        [Theory]
        [InlineData("Hello World")]
        public void ByteArrayAndStringTests(string plain)
        {
            byte[] byteArrayData = Utils.StringToByteArray(plain);
            string stringData = Utils.ByteArrayToString(byteArrayData);
            Assert.Equal(stringData, plain);
        }

        [Theory]
        [InlineData("hex", "30303131323233333434353536363737", "0011223344556677")]
        public void TransformHexDataTests(string dataFormat, string plain, string expected)
        {
            byte[] transformedData = Utils.TransformData(dataFormat, plain);
            string transformedDataString = Utils.ByteArrayToString(transformedData);
            Assert.Equal(transformedDataString, expected);

            byte[] plainDataArray = Utils.HexStringToByteArray(plain);
            string transformedDataString2 = Utils.TransformData(dataFormat, plainDataArray);
            Assert.Equal(transformedDataString2, plain);
        }

        [Theory]
        [InlineData("base64", "MDAxMTIyMzM0NDU1NjY3Nw==", "30303131323233333434353536363737")]
        public void TransformBase64DataTests(string dataFormat, string plain, string expected)
        {
            byte[] transformedData = Utils.TransformData(dataFormat, plain);
            string transformedDataString = Utils.ByteArrayToHexString(transformedData);
            Assert.Equal(transformedDataString, expected);

            byte[] plainDataArray = Utils.Base64StringToByteArray(plain);
            string transformedDataString2 = Utils.TransformData(dataFormat, plainDataArray);
            Assert.Equal(transformedDataString2, plain);

        }

        [Theory]
        [InlineData("ascii", "0011223344556677", "30303131323233333434353536363737")]
        public void TransformASCIITextDataTests(string dataFormat, string plain, string expected)
        {

            byte[] transformedData = Utils.TransformData(dataFormat, plain);
            string transformedDataString = Utils.ByteArrayToHexString(transformedData);
            Assert.Equal(transformedDataString, expected);

            byte[] plainDataArray = Utils.StringToByteArray(plain);
            string transformedDataString2 = Utils.TransformData(dataFormat, plainDataArray);
            Assert.Equal(transformedDataString2, plain);

        }

        [Theory]
        [InlineData("utf8", "0011223344556677", "30303131323233333434353536363737")]
        public void TransformUTF8TextDataTests(string dataFormat, string plain, string expected)
        {

            byte[] transformedData = Utils.TransformData(dataFormat, plain);
            string transformedDataString = Utils.ByteArrayToHexString(transformedData);
            Assert.Equal(transformedDataString, expected);

            byte[] plainDataArray = Utils.StringToByteArray(plain);
            string transformedDataString2 = Utils.TransformData(dataFormat, plainDataArray);
            Assert.Equal(transformedDataString2, plain);

        }

        [Theory]
        [InlineData("xxx")]
        public void TransformInvalidDataTests(string invalidDataFormat)
        {
            byte[] byteArray;

            Exception ex = Assert.Throws<NotSupportedDataFormatException>(() => byteArray = Utils.TransformData(invalidDataFormat, ""));
            Assert.StartsWith("Provided data format is not supported.", ex.Message);

            string s;
            Exception ex2 = Assert.Throws<NotSupportedDataFormatException>(() => s = Utils.TransformData(invalidDataFormat, Array.Empty<byte>()));
            Assert.StartsWith("Provided data format is not supported.", ex.Message);

        }

    }
}
