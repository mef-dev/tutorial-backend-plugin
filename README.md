

# Creation of Backend package from template

> You have got to install an IDE with support for writing code in the C# programming language. [VisualStudio](https://visualstudio.microsoft.com/vs/) was used to write this guide.

> Also is required the [Dotnet CLI](https://docs.microsoft.com/ru-ru/dotnet/core/tools/dotnet)

## Repository cloning

Let's start the development with the GIT repository cloning **[tutorial-backend-plugin](https://github.com/mef-dev/tutorial-backend-plugin)**.
```
git clone https://github.com/mef-dev/tutorial-backend-plugin.git
```

## Content overview

After cloning and opening the solution, the content will be like that:

|![Project content](/Images/dev_guides/create_backend_plugin/1.png)|
| :--: |

The unique identifier whithin the platform is the assembly name as well as the entity name within the particular alias. When creating your own project, rename the project to change assemply name and modify the name of the entity as you want.

## Build

To build the package you can use the command below:
```
dotnet publish -o bin\Deploy --force
```
After successful build operation, you have got to archive the content of `bin\Deploy` folder into ZIP file. 

##  Registration package

Let's start registration process with the package creation page, it should be done the only once

|![Creating page](/Images/dev_guides/create_ui_plugin/2.png)|
| :--: |

> *Note. This functionality is available to users with Developer Admin and Developer roles*

The package creation page you can find under the `Plugins` menu. After clicking the Add button, we get to the plugin creation page. In this case, we need to specify the details of the new plugin below:

- **ALIAS** - the name of the subject area of the package, which is used to combine packages by functional purpose (logical group). Should be noted that package names cannot intersect within one ALIAS name. 
- **NAME** - the package name. 

After entering this data, we will pass to a choice of package type. At this time the platform supports 4 main package types - the purpose of each type and the differences between them are written in the help (green block). Now we are interested in the `Service` type - a package oriented to handle the requests from UI components without specific tenant configuration, so we choose it.

After the selection, our `Backend` block was activated to user input. It contains only one field `PluginMefName`. This is the project assembly name. Enter the name and click **Save**.
When all fields are filed correctly, you will be directed to the package configuration page.

> *Note. Futher, you can navigate to it via the `Configure` sub-menu in the selected row menu, located in the last column of table on the page* ***Plugins***
 
##  Uploading version of package

The updated or initial package version is uploaded from the **Backend** block. When you click on the Upload button, you have got to select the ZIP archive with **collected package content from the folder** witn name: `bin\Deploy`.

|![Creating page](/Images/dev_guides/create_backend_plugin/2.png)|
| :--: |

It is not allowed to upload has already uploaded version - so it is important to manage the package versioning and do changes of the assembly version in the project properties.
After uploading, you have got to switch to the uploaded version and click **Save**
 
##  Package Dry run

You can make dry run of the package with any API tools. In this guide, the *Postman* will be used.

You should be authorized to send HTTP requests - usually, the authorization scheme with the user token is used, but we will use `Basic Auth` for dry runs. You can create the required login-password pair under the [SETTINGS\CREDENTIALS](https://preview.mef.dev/console/settings/credentials) section of your profile on the platform, accessed by clicking on the user icon in the upper right corner. After clicking on the `ADD` button you will be able to set up the user login and password with the Basic Auth authorization type.

### Basic health check
From a technical perspective, the platform provides you the endpoint for the package's version check within the platform:
```
https://preview.mef.dev/api/v1/<alias>/plugins/<PluginMefName>/version.json?detaillevel=detailed
```

|![detaillevel=detailed](/Images/dev_guides/create_backend_plugin/3.png)|
| :--: |

In case of a similar result, your package was uploaded on the platform successfully.

### Standard Package requests
Sending the standard requests to the package will be demonstrated by the GET requests examples.

To send requests you have got to use the URL path below:
```
https://preview.mef.dev/api/v1/<alias>/<EntityName>
```
Design-wise, you can develop and add any parameters, headers, and fields within the request, but you should handle them with the input and output models of the package. To operate with the platform with specific tasks, you should use ServiceProvider from the context
 
|![detaillevel=detailed](/Images/dev_guides/create_backend_plugin/4.png)|
| :--: |

### Specific Action requests

To implement specific logic which should outstanding of standard HTTP methods, you have got to use the Action implementation over the POST method. An example you can find with the URL path below:
```
https://preview.mef.dev/api/v1/<alias>/<EntityName>/<ActionName>.json
```
|![detaillevel=detailed](/Images/dev_guides/create_backend_plugin/5.png)|
| :--: |

> The implementation of these requests also is available by the Angular application package example [tutorial-ui-plugin](https://github.com/mef-dev/tutorial-ui-plugin).
