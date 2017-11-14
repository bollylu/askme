using AskMeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using BLTools.Json;

namespace AskMeLibTest {
  [TestClass]
  public class TRepositoryTests {

    private const string REPOSITORY_TESTFOLDER_NAME = "TestFolder";
    private const string REPOSITORY_ALTERNATE_DATA = "AlternateData";
    private const string REPOSITORY_ALTERNATE_DESC = "AlternateDesc";

    [TestMethod]
    public void InstantiateRepository_MinimumParameters_DataOk() {
      using ( TRepository Actual = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
        Assert.AreEqual("", Actual.Id);
        Assert.AreEqual(REPOSITORY_TESTFOLDER_NAME, Actual.Name);
        Assert.AreEqual("", Actual.Description);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME), Actual.RepositoryPath);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, TRepository.DEFAULT_REPOSITORY_FILENAME), Actual.RepositoryFilename);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, Actual.DataFolderName), Actual.CompleteDataFolderName);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, Actual.DescFolderName), Actual.CompleteDescFolderName);
      }
    }

    [TestMethod]
    public void ConvertRepositoryToJson_MinimumParameters_ReadBack_DataOk() {
      JsonObject JsonRepository = null;
      using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
        JsonRepository = Repository.ToJson() as JsonObject;
      }
      using ( TRepository Actual = new TRepository(JsonRepository) ) {
        Assert.AreEqual("", Actual.Id);
        Assert.AreEqual(REPOSITORY_TESTFOLDER_NAME, Actual.Name);
        Assert.AreEqual("", Actual.Description);
        Assert.AreEqual(TRepository.DEFAULT_DATA_FOLDER_NAME, Actual.DataFolderName);
        Assert.AreEqual(TRepository.DEFAULT_DESC_FOLDER_NAME, Actual.DescFolderName);
      }
    }

    //[TestMethod]
    //public void ConvertRepositoryToJson_AdditionalParameters_DataOk() {
    //  JsonObject JsonRepository = null;
    //  using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
    //    Repository.DataFolderName = REPOSITORY_ALTERNATE_DATA;
    //    Repository.DescFolderName = REPOSITORY_ALTERNATE_DESC;
    //    JsonRepository = Repository.ToJson();
    //  }
    //  using ( TRepository Actual = new TRepository(JsonRepository) ) {
    //    Assert.AreEqual("", Actual.Id);
    //    Assert.AreEqual(REPOSITORY_TESTFOLDER_NAME, Actual.Name);
    //    Assert.AreEqual("", Actual.Description);
    //    Assert.AreEqual(REPOSITORY_ALTERNATE_DATA, Actual.DataFolderName);
    //    Assert.AreEqual(REPOSITORY_ALTERNATE_DESC, Actual.DescFolderName);
    //  }
    //}

    //[TestMethod]
    //public void ConvertRepositoryToJson_JsonWithError_ArgumentException() {
    //  MockJsonString JsonRepository = null;
    //  using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
    //    Repository.DataFolderName = REPOSITORY_ALTERNATE_DATA;
    //    Repository.DescFolderName = REPOSITORY_ALTERNATE_DESC;
    //    JsonRepository = new MockJsonString(Repository.ToJson());
    //    JsonRepository.MessContent();
    //  }
    //  Assert.IsFalse(JsonRepository.IsValid());
    //}
  }

  //public class MockJsonString : JsonObject {
  //  public MockJsonString(JsonObject jsonContent) : base(jsonContent) { }

  //  public void MessContent() {
  //    this.
  //  }
  //}
}
