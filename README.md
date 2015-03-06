# hipster-technologies-api
Team #Rekt's Web Engineering project backend API.


After cloning the repo, run `whoami` in command prompt. Copy the response.

Then, run the following command in PowerShell as administrator, replacing `DOMAIN\user` with the response from `whoami`:
```
netsh http add urlacl url=http://+:8080/ user=DOMAIN\username
```
