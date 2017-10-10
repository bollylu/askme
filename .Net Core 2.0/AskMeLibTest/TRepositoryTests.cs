using AskMeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace AskMeLibTest {
  [TestClass]
  public class TRepositoryTests {

    private const string REPOSITORY_TESTFOLDER_NAME = "TestFolder";
    private const string REPOSITORY_ALTERNATE_DATA = "AlternateData";
    private const string REPOSITORY_ALTERNATE_DESC = "AlternateDesc";

    [TestMethod]
    public void InstantiateRepository_MinimumParameters_DataOk() {
      using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME), Repository.RepositoryPath);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, TRepository.DEFAULT_REPOSITORY_FILENAME), Repository.RepositoryFilename);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, Repository.DataFolderName), Repository.CompleteDataFolderName);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, Repository.DescFolderName), Repository.CompleteDescFolderName);
      }
    }

    [TestMethod]
    public void ConvertRepositoryToJson_MinimumParameters_DataOk() {
      JsonString JsonRepository = null;
      using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
        JsonRepository = Repository.ToJson();
      }
      using ( TRepository Actual = new TRepository(JsonRepository) ) {
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME), Actual.RepositoryPath);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, TRepository.DEFAULT_REPOSITORY_FILENAME), Actual.RepositoryFilename);
        Assert.AreEqual(TRepository.DEFAULT_DATA_FOLDER_NAME, Actual.DataFolderName);
        Assert.AreEqual(TRepository.DEFAULT_DESC_FOLDER_NAME, Actual.DescFolderName);
      }
    }

    [TestMethod]
    public void ConvertRepositoryToJson_AdditionalParameters_DataOk() {
      JsonString JsonRepository = null;
      using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
        Repository.DataFolderName = REPOSITORY_ALTERNATE_DATA;
        Repository.DescFolderName = REPOSITORY_ALTERNATE_DESC;
        JsonRepository = Repository.ToJson();
      }
      using ( TRepository Actual = new TRepository(JsonRepository) ) {
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME), Actual.RepositoryPath);
        Assert.AreEqual(Path.Combine(TRepository.GlobalRepositoryRoot, REPOSITORY_TESTFOLDER_NAME, TRepository.DEFAULT_REPOSITORY_FILENAME), Actual.RepositoryFilename);
        Assert.AreEqual(REPOSITORY_ALTERNATE_DATA, Actual.DataFolderName);
        Assert.AreEqual(REPOSITORY_ALTERNATE_DESC, Actual.DescFolderName);
      }
    }

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public void ConvertRepositoryToJson_JsonWithError_ArgumentException() {
      JsonString JsonRepository = null;
      using ( TRepository Repository = new TRepository(REPOSITORY_TESTFOLDER_NAME) ) {
        Repository.DataFolderName = REPOSITORY_ALTERNATE_DATA;
        Repository.DescFolderName = REPOSITORY_ALTERNATE_DESC;
        JsonRepository = Repository.ToJson();
        JsonRepository.Content = JsonRepository.Content.Replace('{', '[');
      }
      TRepository Actual = new TRepository(JsonRepository);
    }
  }
}
