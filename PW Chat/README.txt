For source code download-------------------------------------------------------
The solution of PW-Chat contains pre and post build events that probably link
to applications you don't have on your system, GNU tools (rm cp mv) 7zip and NSIS
You must either remove them from the build events OR make sure you have all
the tools (I like a nice automated build)

For compiled download(applies to source as well)-------------------------------
You DO NOT have to use the Setup provided, I simply have it there for ease of 
use. Inside the standalone directory is all that is needed to run PW Chat
You must put the server files somewhere that can parse PHP it DOES NOT
have to be on the same server as your PW Server so long as it can talk to 
gdeliveryd. If you whine and say it doesn't work it probably is your fault
as it works for me on a different machine and on my PW Server vm just fine

Raise an issue on Google Code OR in the Ragezone forum thread ONLY if
you can't figure something out