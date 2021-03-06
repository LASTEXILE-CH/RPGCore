using NUnit.Framework;
using System.IO;

namespace RPGCore.Packages.UnitTests
{
	[TestFixture(TestOf = typeof(ProjectDefinitionProperties))]
	public class ProjectDefinitionEditingShould
	{
		private static string FileName(string name)
		{
			return Path.Combine(
				TestContext.CurrentContext.Test.ClassName,
				TestContext.CurrentContext.Test.Name, name);
		}

		[SetUp]
		public void Setup()
		{
		}

		[Test, Parallelizable]
		public void ModifyName()
		{
			var createExplorer = ProjectExplorer.CreateProject(FileName("project"), new ImportPipeline());

			createExplorer.Definition.Properties.Name = "TestName1";

			createExplorer.Definition.SaveChanges();

			var loadExplorer = ProjectExplorer.Load(FileName("project"), new ImportPipeline());
			Assert.That(loadExplorer.Definition.Properties.Name, Is.EqualTo("TestName1"));

			loadExplorer.Export(new BuildPipeline()
			{
				ImportPipeline = new ImportPipeline(),
			}, FileName("export"));

			var packageImport = PackageExplorer.Load(
				Path.Combine(FileName("export"), "TestName1.bpkg"));
			Assert.That(packageImport.Definition.Properties.Name, Is.EqualTo("TestName1"));
		}

		[Test, Parallelizable]
		public void ModifyNameAndVersion()
		{
			var createExplorer = ProjectExplorer.CreateProject(FileName("project"), new ImportPipeline());

			createExplorer.Definition.Properties.Name = "TestName1";
			createExplorer.Definition.Properties.Version = "1.0.0";

			createExplorer.Definition.SaveChanges();

			var loadExplorer = ProjectExplorer.Load(FileName("project"), new ImportPipeline());
			Assert.That(loadExplorer.Definition.Properties.Name, Is.EqualTo("TestName1"));
			Assert.That(loadExplorer.Definition.Properties.Version, Is.EqualTo("1.0.0"));

			loadExplorer.Export(new BuildPipeline()
			{
				ImportPipeline = new ImportPipeline(),
			}, FileName("export"));

			var packageImport = PackageExplorer.Load(
				Path.Combine(FileName("export"), "TestName1.bpkg"));
			Assert.That(packageImport.Definition.Properties.Name, Is.EqualTo("TestName1"));
			Assert.That(packageImport.Definition.Properties.Version, Is.EqualTo("1.0.0"));
		}

		[Test, Parallelizable]
		public void ModifyVersion()
		{
			var createExplorer = ProjectExplorer.CreateProject(FileName("project"), new ImportPipeline());

			createExplorer.Definition.Properties.Version = "1.0.0";

			createExplorer.Definition.SaveChanges();

			var loadExplorer = ProjectExplorer.Load(FileName("project"), new ImportPipeline());
			Assert.That(loadExplorer.Definition.Properties.Version, Is.EqualTo("1.0.0"));

			loadExplorer.Export(new BuildPipeline()
			{
				ImportPipeline = new ImportPipeline(),
			}, FileName("export"));

			var packageImport = PackageExplorer.Load(
				Path.Combine(FileName("export"), "project.bpkg"));
			Assert.That(packageImport.Definition.Properties.Version, Is.EqualTo("1.0.0"));
		}
	}
}
