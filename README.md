# hipster-technologies-api
Team #Rekt's Web Engineering project backend API.

After cloning the repo, run `whoami` in command prompt. Copy the response.

Then, run the following command in PowerShell as administrator, replacing `DOMAIN\user` with the response from `whoami`:
```
netsh http add urlacl url=http://+:8080/ user=DOMAIN\username
```

## Getting the client working

1. Make sure the submodule is upd to date
2. `cd HipsterTechnologies.API\Client`
3. `gulp build`

Then you should be able to run the project in visual studio and everything will be just peachy
