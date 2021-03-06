// <copyright file="PexAssemblyInfo.cs">Copyright ©  2015</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("CoopCheck.WPF")]
[assembly: PexInstrumentAssembly("Csla")]
[assembly: PexInstrumentAssembly("CsvHelper")]
[assembly: PexInstrumentAssembly("GalaSoft.MvvmLight.Platform")]
[assembly: PexInstrumentAssembly("System.Data")]
[assembly: PexInstrumentAssembly("Xceed.Wpf.Toolkit")]
[assembly: PexInstrumentAssembly("LinqToExcel")]
[assembly: PexInstrumentAssembly("System.Deployment")]
[assembly: PexInstrumentAssembly("EntityFramework")]
[assembly: PexInstrumentAssembly("EntityFramework.SqlServer")]
[assembly: PexInstrumentAssembly("GalaSoft.MvvmLight.Extras")]
[assembly: PexInstrumentAssembly("Microsoft.CSharp")]
[assembly: PexInstrumentAssembly("System.Configuration")]
[assembly: PexInstrumentAssembly("CoopCheck.DAL")]
[assembly: PexInstrumentAssembly("FirstFloor.ModernUI")]
[assembly: PexInstrumentAssembly("System.Xaml")]
[assembly: PexInstrumentAssembly("System.ComponentModel.DataAnnotations")]
[assembly: PexInstrumentAssembly("WindowsBase")]
[assembly: PexInstrumentAssembly("PresentationCore")]
[assembly: PexInstrumentAssembly("PresentationFramework")]
[assembly: PexInstrumentAssembly("CoopCheck.Library")]
[assembly: PexInstrumentAssembly("CoopCheck.Repository")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("DataClean")]
[assembly: PexInstrumentAssembly("Microsoft.Practices.ServiceLocation")]
[assembly: PexInstrumentAssembly("GalaSoft.MvvmLight")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Csla")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "CsvHelper")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "GalaSoft.MvvmLight.Platform")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Data")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Xceed.Wpf.Toolkit")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "LinqToExcel")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Deployment")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "EntityFramework")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "EntityFramework.SqlServer")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "GalaSoft.MvvmLight.Extras")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Microsoft.CSharp")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Configuration")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "CoopCheck.DAL")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "FirstFloor.ModernUI")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Xaml")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.ComponentModel.DataAnnotations")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "WindowsBase")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "PresentationCore")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "PresentationFramework")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "CoopCheck.Library")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "CoopCheck.Repository")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "DataClean")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Microsoft.Practices.ServiceLocation")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "GalaSoft.MvvmLight")]

