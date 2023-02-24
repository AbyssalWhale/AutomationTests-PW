# Solution overview:
Solution consists of 3 projects. All of them use .NET 7.0 framework.    
- Core - main project with managers, utils etc that are required by objects from the 'TestsConfigurator' project;
- TestsConfigurator - project with models (POMs, APIs etc), fixtures, and reusettings. It has reference to the 'AutomationCore' project only;
- RegressionTests - project with UI and API regression tests. It has reference to the 'TestsConfigurator' project only;

Tools under the hood:
- NUnit;
- Playwright;

# Solution' projects overview:
- Core:
  - AssertAndErrorMsgs - assert and exception messages that should be used by exceptions and asserts;
  - Enums - data that can be reused by tests, POMs, and APIs during test execution;
  - Managers - tool's wrappers that are used by Tests, POMs, and APIs during test execution; 
  - Utils - additional tools that can be used at any level (POMs, API routes, tests etc);
- TestsConfigurator:
  - Fixtures - has all fixtures for all tests;
  - Models - contains all POMs and API models including API routes; 
  - RunSettings - run settings for specific branch/instance;
  - Projects - has reference to the 'Core' project;
- RegressionTests: 
  - UI -  all UI regression tests. Tests sorting: Page name.
  - TestsResults - contains tests attachment for each run. Notice: folder is added to .gitignore. 
  - Projects - has reference to the 'TestsConfigurator' project;

