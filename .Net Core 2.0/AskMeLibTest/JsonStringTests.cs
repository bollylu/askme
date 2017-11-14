using AskMeLib;
using BLTools.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace AskMeLibTest {
  [TestClass]
  public class JsonStringTests {

    #region --- Support --------------------------------------------
    internal class TFakeClass {
      public string TestString { get; set; }
      public int TestInt { get; set; }

      public double TestDouble { get; set; }
      public DateTime TestDateTime { get; set; }
      public bool TestBool { get; set; }

      public TFakeClass() {
        TestString = TEST_STRING;
        TestInt = TEST_INT;
        TestDouble = TEST_DOUBLE;
        TestDateTime = TEST_DATETIME;
        TestBool = TEST_BOOL;
      }

      public TFakeClass(JsonObject jsonSource) {
        TestString = jsonSource.SafeGetValueFirst<string>(nameof(TestString), DEFAULT_STRING);
        TestInt = jsonSource.SafeGetValueFirst<int>(nameof(TestInt), DEFAULT_INT);
        TestDouble = jsonSource.SafeGetValueFirst<double>(nameof(TestDouble), DEFAULT_DOUBLE);
        TestDateTime = jsonSource.SafeGetValueFirst<DateTime>(nameof(TestDateTime), DEFAULT_DATETIME);
        TestBool = jsonSource.SafeGetValueFirst<bool>(nameof(TestBool), DEFAULT_BOOL);
      }

      public JsonObject ToJson() {
        JsonObject RetVal = new JsonObject();
        RetVal.AddItem(new JsonPair(nameof(TestString), TestString));
        RetVal.AddItem(new JsonPair(nameof(TestInt), TestInt));
        RetVal.AddItem(new JsonPair(nameof(TestDouble), TestDouble));
        RetVal.AddItem(new JsonPair(nameof(TestDateTime), TestDateTime));
        RetVal.AddItem(new JsonPair(nameof(TestBool), TestBool));
        return RetVal;
      }
    }
    #endregion --- Support --------------------------------------------

    #region --- Constants --------------------------------------------
    private const string TEST_STRING = "TestContent";
    private const string TEST_STRING_JSON = "\"TestContent\"";
    private const string DEFAULT_STRING = "(default)";

    private const int TEST_INT = 98765;
    private const string TEST_INT_JSON = "98765";
    private const int DEFAULT_INT = -1;

    private const double TEST_DOUBLE = 123.456789d;
    private const string TEST_DOUBLE_JSON = "123.456789";
    private const double DEFAULT_DOUBLE = -1.0d;

    private static DateTime TEST_DATETIME = DateTime.Today;
    private static string TEST_DATETIME_JSON = DateTime.Today.ToString();
    private static DateTime DEFAULT_DATETIME = DateTime.MinValue;

    private const bool TEST_BOOL = true;
    private static string TEST_BOOL_JSON = "true";
    private const bool DEFAULT_BOOL = false;

    internal JsonObject JsonSource;
    internal TFakeClass FakeClass; 

    #endregion --- Constants --------------------------------------------

    [TestInitialize]
    public void Initialize() {
      FakeClass = new TFakeClass();
      JsonSource = FakeClass.ToJson();
    }

    [TestMethod]
    public void InstantiateJsonString_ReadContentBack_DataOk() {
      Assert.AreEqual(TEST_STRING, FakeClass.TestString);
      Assert.AreEqual(TEST_INT, FakeClass.TestInt);
    }

    #region --- Single type, valid keys --------------------------------------------
    [TestMethod]
    public void SafeGetValueString_KeyValid_DataOk() {
      string Actual = JsonSource.SafeGetValueFirst<string>(nameof(TFakeClass.TestString), DEFAULT_STRING);
      Assert.AreEqual(TEST_STRING, Actual);
    }

    [TestMethod]
    public void SafeGetValueInt_KeyValid_DataOk() {
      int Actual = JsonSource.SafeGetValueFirst<int>(nameof(TFakeClass.TestInt), DEFAULT_INT);
      Assert.AreEqual(TEST_INT, Actual);
    }

    [TestMethod]
    public void SafeGetValueDouble_KeyValid_DataOk() {
      double Actual = JsonSource.SafeGetValueFirst<double>(nameof(TFakeClass.TestDouble), DEFAULT_DOUBLE);
      Assert.AreEqual(TEST_DOUBLE, Actual);
    }

    [TestMethod]
    public void SafeGetValueDateTime_KeyValid_DataOk() {
      DateTime Actual = JsonSource.SafeGetValueFirst<DateTime>(nameof(TFakeClass.TestDateTime), DEFAULT_DATETIME);
      Assert.AreEqual(TEST_DATETIME, Actual);
    }

    [TestMethod]
    public void SafeGetValueBool_KeyValid_DataOk() {
      bool Actual = JsonSource.SafeGetValueFirst<bool>(nameof(TFakeClass.TestBool), DEFAULT_BOOL);
      Assert.AreEqual(TEST_BOOL, Actual);
    }
    #endregion --- Single type, valid keys --------------------------------------------

    #region --- Single type, invalid keys --------------------------------------------
    [TestMethod]
    public void SafeGetValueString_KeyInvalid_DataOk() {
      string Actual = JsonSource.SafeGetValueFirst<string>(nameof(TFakeClass.TestString) +"_", DEFAULT_STRING);
      Assert.AreEqual(DEFAULT_STRING, Actual);
    }

    [TestMethod]
    public void SafeGetValueInt_KeyInvalid_DataOk() {
      int Actual = JsonSource.SafeGetValueFirst<int>(nameof(TFakeClass.TestInt) + "_", DEFAULT_INT);
      Assert.AreEqual(DEFAULT_INT, Actual);
    }

    [TestMethod]
    public void SafeGetValueDouble_KeyInvalid_DataOk() {
      double Actual = JsonSource.SafeGetValueFirst<double>(nameof(TFakeClass.TestDouble) + "_", DEFAULT_DOUBLE);
      Assert.AreEqual(DEFAULT_DOUBLE, Actual);
    }

    [TestMethod]
    public void SafeGetValueDateTime_KeyInvalid_DataOk() {
      DateTime Actual = JsonSource.SafeGetValueFirst<DateTime>(nameof(TFakeClass.TestDateTime) + "_", DEFAULT_DATETIME);
      Assert.AreEqual(DEFAULT_DATETIME, Actual);
    }

    [TestMethod]
    public void SafeGetValueBool_KeyInvalid_DataOk() {
      bool Actual = JsonSource.SafeGetValueFirst<bool>(nameof(TFakeClass.TestBool) + "_", DEFAULT_BOOL);
      Assert.AreEqual(DEFAULT_BOOL, Actual);
    }
    #endregion --- Single type, invalid keys --------------------------------------------

    
  }



}
