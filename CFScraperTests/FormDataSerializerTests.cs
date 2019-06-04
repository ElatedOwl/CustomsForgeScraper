using CFScraper.Domain.FormData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraperTests
{
    [TestClass]
    public class FormDataSerializerTests
    {
        private class SerializeTestClass
        {
            [FormData("string_test")]
            public string StringTest => "hello world";
            [FormData("int_test")]
            public int IntTest => 5;
            public string NonFormDataTest => "I should be ignored";
        }
        [TestMethod]
        public void Serialize_Maps_FormDataName()
        {
            string encodedContent = SerializedTestClass();
            Assert.IsTrue(encodedContent.Contains("string_test"));
            Assert.IsTrue(encodedContent.Contains("int_test"));
        }

        [TestMethod]
        public void Serialize_Encodes_StringValues()
        {
            var testClass = new SerializeTestClass();
            string encodedContent = SerializedTestClass(testClass);
            var testValue = System.Web.HttpUtility.UrlEncode(testClass.StringTest);

            Assert.IsTrue(encodedContent.Contains(testValue));
        }

        [TestMethod]
        public void Serialize_Encodes_IntValues()
        {
            var testClass = new SerializeTestClass();
            string encodedContent = SerializedTestClass(testClass);
            var testValue = testClass.IntTest.ToString();

            Assert.IsTrue(encodedContent.Contains(testValue));
        }

        [TestMethod]
        public void Serialize_IgnoresProperties_WithoutFormDataAttribute()
        {
            string encodedContent = SerializedTestClass();
            string ignoredPropertyName = nameof(SerializeTestClass.NonFormDataTest);
            Assert.IsFalse(encodedContent.Contains(ignoredPropertyName));
        }

        private string SerializedTestClass() => SerializedTestClass(new SerializeTestClass());

        private string SerializedTestClass(SerializeTestClass testClass)
        {
            var serializer = new FormDataSerializer();

            var formContent = serializer.Serialize(testClass);
            return formContent.ReadAsStringAsync().Result;
        }
    }
}
