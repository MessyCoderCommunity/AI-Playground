# Development of Release Candididates

We use [PackageTools](https://github.com/jeffcampbellmakesgames/unity-package-tools) to create our releases. To build a release candidate:

  0. Checkout the `main` branch of this repo - this will provide a complete working project within which you can work.
  1. Update (at least) the version number in the `PackageManifestConfig` in the root of the `Assets` folder
  2. Click `Generate VersionConstants.cs` in the inspector
  3. Commit the new constants file to Git
  4. Click `Export Package Source`
  5. Commit and push the changes in your release project to GitHub

To use this release candidate in development versions of your local project
import the package using the package manager. Click the '+' in the top left
of the package manager and select 'Add package from disk...' and navigate
to the `package.json` file in `[PROJECT ROOT]/bin`.

If you need to make changes to the code create a test environmetn in the
checkout of this project. Make your changes, repeat the release candidate
process above and try within your project. Note that it is not necessary to
re-import into your downstream project, it will automaticcaly be picked up
and re-imported by Unity when switching back to that instance of the Unity
Editor.

Continue iterating until 
everything works and then issue a pull request against this project and 
start the formal release process.
