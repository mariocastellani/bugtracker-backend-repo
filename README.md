# bug-tracking-backend-repo

## Infrastructure.Data ##
* To adds migration from **Package Manager Console**
  
  * Make sure that **`Infrastructure.Data`** is set as `Default Project` in Package Manager Console
  * Make sure that **`ApiService`** is set as the `Startup Project` of the solution
  * Write this in the console:
  
  `Add-Migration <NAME OF MIGRATION>`

  * then to impact in database:

  `Update-Database`

## IdentityService ##
* To adds migration from **Package Manager Console**
  
  * Make sure that **`IdentityService`** is set as `Default Project` in Package Manager Console
  * Make sure that **`IdentityService`** is set as the `Startup Project` of the solution
  * Write this un the console:
  
  ```
  Add-Migration <NAME OF MIGRATION> 
    -Context { ConfigurationDbContext | PersistedGrantDbContext | IdentityDataContext }
    -OutputDir Data/Migration/{ Configuration | Operational | Identity }
  ```

  * then to impact in database:

  `Update-Database -Context { ConfigurationDbContext | PersistedGrantDbContext | IdentityDataContext }`