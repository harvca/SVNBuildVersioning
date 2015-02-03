SVNBuildVersioning
==================
This project required TortoiseSVN to be installed

Major and Minor are not changed.

Build not changed on Release (Default, Prod or UAT)

Revision is changed to the SVN Last committed at revision

Versioning example:
2014.2.31.460 -> 2014.2.32.463


Usage: SVNBuildVersioning.exe [Project Directory] [Assembly Name] [Build Type]

Example Visual Studion Pre-build event command line:

$(SolutionDir)Common\SVNBuildVersioning.exe $(ProjectDir) $(TargetFileName) $(ConfigurationName)
