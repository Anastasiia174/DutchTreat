# DutchTreat

## Test project for practicing in building a Web App with ASP.NET Core, MVC, EF Core and Angular.

## Structure
```
├──ClientApp                    # Client side source
│  ├───app                      # client components in Angular
│  │   ├───checkout             # component for checkout logic
│  │   ├───login                # component for login logic 
│  │   ├───shared               # shared data
│  │   └───shop                 # component for shop page
│  ├───assets                   # resources
│  └───environments             # angular internal source
├──Controllers                  # MVC controllers in ASP.NET Core
├──Data                         # data
│  ├───Entities                 # data classes
│  └───Migrations               # EF Core migrations
├──Models                       # models classes
├──Pages                        # view pages in Razor (not used)
├──Services                     # services (contains only mail service)
├──Views                        # views in Razor (not used)
├──wwwroot                      # static files
   ├───clientapp                # angular output files
   ├───css                      # styles  
   ├───dist                     # folder with minified files  
   ├───img                      # static images
   ├───js                       # js scripts
   └───ts                       # ts scripts
