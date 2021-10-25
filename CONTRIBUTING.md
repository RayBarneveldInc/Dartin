# Contributing to Dartin
The following is a set of guidelines for contributing to Dartin. These are guidelines but not rules, so be free to propose changes.

## How can I contribute?

### Reporting Bugs
When you find a bug in our software, please feel free to submit an issue on [the GitHub page](https://github.com/RayBarneveldInc/Dartin/issues). Please stick to the template provided.

### Code Contribution
Dartin is written in C# and uses the Git workflow via a scrum board in our Projects tab. If you wish to contribute code having experience in C#, C++ or C will help you a lot understanding how Dartin is built.

#### Visual Studio 2019
You can download Visual Studio 2019 [here](https://visualstudio.microsoft.com/vs/) - get the Community one if you don't have an Enterprise license.

Make sure to select the following components when installing Visual Studio:

- **.NET Desktop Development** from the Workload tab
- **NuGet Package Manager** from the Code Tools tab
- **.NET 5.0 Runtime** from the *Individual components* tab
- **C#** from the *Individual components* tab
- **Live Unit Testing** from the *Individual components* tab

The installer may have selected other options as well, but these are the most important ones.

#### Starting development

- Fork and clone the project: go to the GitHub repo page, click the fork button, copy the url from the forked repo, navigate to your project folder, open Git Bash or normal command prompt and type git clone url name and replace url with the copied URL and name with the folder name.
- Open Dartin.sln in Visual Studio 2019.
- Download NuGet Packages by selecting the solution and Right Click -> Restore NuGet Packages.

It may take a while for Visual Studio to download all packages and update all references so be patient. Once all packages are downloaded go and try building Dartin. If the build is successful then it should work, also try running the tests and see if all tests complete succesfully. If you experience issues trying to setup the development environment, you can also file an issue on the GitHub page and we will try to help.

##### Coding Style
The Dartin project uses the [C# Coding Style](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md). When working with Git you should also never push commits to your master branch when forking the project. We assume you're using Git via command-line, you can follow these instructions to create a branch and push your changes:

- `git pull`

Make sure you're on the latest version before attempting to push a change.

- `git add -A`

Add every change you made into the commit you're about to make.

- `git commit -m "Add new feature"`
Commit the added files with the description 'Add new feature'.

- `git push`
Push your changes to your repo on the new branch. You can now create an upstream pull request for the feature via GitHub. Make sure that all tests run succesfully and your code runs before creating the pull request! Also write a unit test if the feature is decently sized.
