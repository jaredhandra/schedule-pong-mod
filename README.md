# schedule-mono-example
Example template and instructions for modding Schedule I Mono version


## Requirements
- The game requires your mod to target the net48 framework, this is defined in the ExampleMod.csproj file TargetFramework property and net48 must be installed before building the library. Visual Studio will prompt you to install it if not present.
- Then install the 2 required packages, Visual Studio toolbar -> Project -> Manage NuGet Packages
  - Search for LavaGang.MelonLoader and install it
  - Search for Lib.Harmony and install it

- Now you must additionally have MelonLoader installed for the game, if it is not installed do it now.
- Opt in to the Alternate or Alternate Beta branch for Schedule I in Steam and wait for it to finish installation.
  - Then you must navigate to C:\Program Files (x86)\Steam\steamapps\common\Schedule I\Schedule I_Data\Managed
  - From here you will need two files: Assembly-CSharp.dll and UnityEngine.CoreModule.dll, copy these two files into the libs folder. (Also specified in the .csproj file)

- After these steps are done, you are ready to code your own logics. See the Template MainMod.cs file for the basic requirements for Harmony and MelonLoader.


## Code Basics

#### Melon Loader
Melon Loader requires you too fill in atleast the following fields in the build info section: Name, Author and Version.

#### Harmony Patch
In the `MainMod.cs` template there is a single Harmony Patch function that runs before (Prefix) every time the player consumes product -> logs message for demo purposes. When writing harmony patches you must specify the exact Class type and name of the function within the `[HarmonyPatch]` Square Brackets. When writing the harmony patches, you must always pass the original class instance into the function. Your function must also use the same input parameters as the original function. Returning true means that after our Prefix has executed, the code will then proceed to the original function of `ConsumeProduct(Player, ProductItemInstance)` or any other prefix function.

#### BepInEx Publicizer
Coding for Mono Backend can be trivial since private, protected, etc. functions, parameters, fields are inaccessible by default. You can get around this with 2 options: Reflection (TypeField -> access value) or BepInEx Publicizer.

To Publicize the Assembly-CSharp.dll file with BepInEx Publicizer:
- Install the package, Visual Studio toolbar -> Project -> Manage NuGet Packages
  - Search for BepInEx.AssemblyPublicizer.MSBuild and install it

- After this your .csproj file should now include the following:
```
<ItemGroup>
  <PackageReference Include="BepInEx.AssemblyPublicizer.MSBuild" Version="0.4.3" PrivateAssets="all" />
</ItemGroup>
```

- You will also need to notate in the .csproj file which assemblies you need too publicize, do this for the ScheduleOne namespace by publicizing the Assembly-CSharp. Add the `Publicize="true"` to the reference tag.
```
<Reference Include="Assembly-CSharp" Publicize="true">
  <HintPath>libs\Assembly-CSharp.dll</HintPath>
</Reference>
```

- You should now be able to access the private properties, functions, fields within the ScheduleOne namespace classes.
