SVNBuildVersioning
==================

Major and Minor are not changed.

Build in not changed on Release (Default, Prod or UAT)

Revision is changed to the SVN Last committed at revision


e.g. 2014.2.32.463


Usage: SVNBuildVersioning.exe [Project Directory] [Assembly Name] [Build Type]

Example Visual Studion Pre-build event command line:

$(SolutionDir)\Common\SVNBuildVersioning.exe $(ProjectDir) $(TargetFileName) $(ConfigurationName)
